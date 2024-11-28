using Core.DTOs.ApproveLoan;
using Core.DTOs.Customer;
using Core.DTOs.Installment;
using Core.DTOs.RequestLoan;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class BankMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoanRequest, RequestLoanResponseDTO>()
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.LoanRequestId, src => src.Id)
            .Map(dest => dest.RequestDate, src => src.RequestDate.ToShortDateString())
            .Map(dest => dest.Status, src => src.Status);

        config.NewConfig<RequestLoanDTO, LoanRequest>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.Amount, src => src.AmountRequest)
            .Map(dest => dest.RequestDate, src => DateTime.UtcNow);

        config.NewConfig<ApproveLoanDTO, ApprovedLoan>()
            .Map(dest => dest.LoanRequestId, src => src.LoanRequestId)
            .Map(dest => dest.Status, src => "Approved")
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow);

        config.NewConfig<RejectLoanDTO, ApprovedLoan>()
            .Map(dest => dest.LoanRequestId, src => src.LoanRequestId)
            .Map(dest => dest.Status, src => "Rejected")
            .Map(dest => dest.RejectionReason, src => src.RejectedReason)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow);

        config.NewConfig<LoanRequest, ApproveLoanResponseDTO>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.RequestedAmount, src => src.Amount)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.InterestRate, src => src.TermInterestRate.Interest)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow.ToShortDateString());

        config.NewConfig<LoanRequest, ApprovedLoan>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.LoanRequestId, src => src.Id)
            .Map(dest => dest.RequestedAmount, src => src.Amount)
            .Map(dest => dest.InterestRate, src => src.TermInterestRate.Interest)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow)
            .Map(dest => dest.Status, src => "Approved")
            .Ignore(dest => dest.Customer)
            .Ignore(dest => dest.LoanRequest)
            .Ignore(dest => dest.Installments)
            .Ignore(dest => dest.Id);

        config.NewConfig<ApprovedLoan, RejectLoanResponseDTO>()
            .Map(dest => dest.LoanRequestId, src => src.Id)
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.RequestedAmount, src => src.RequestedAmount)
            .Map(dest => dest.RejectReason, src => src.RejectionReason);

        config.NewConfig<Installment, OverdueInstallmentDTO>()
            .Map(dest => dest.Customer,
                 src => new DetailedCustomerDTO
                 {
                     Id = src.ApprovedLoan.Customer.Id,
                     Name = $"{src.ApprovedLoan.Customer.FirstName} {src.ApprovedLoan.Customer.LastName}"
                 })
            .Map(dest => dest.DueDate, src => src.DueDate.ToShortDateString())
            .Map(dest => dest.DaysLate, src => $"La cuota tiene {Math.Max(0, (DateTime.UtcNow.Date - src.DueDate.Date).Days)} días de atraso.")
            .Map(dest => dest.AmountPending, src => src.TotalAmount);

        config.NewConfig<Installment, InstallmentResponseDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.DueDate, src => src.DueDate.ToShortDateString())
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.TotalAmount, src => src.TotalAmount);
    }
}
