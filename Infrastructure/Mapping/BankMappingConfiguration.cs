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
        // LoanRequest -> RequestLoanResponse
        config.NewConfig<LoanRequest, RequestLoanResponseDTO>()
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.LoanRequestId, src => src.Id)
            .Map(dest => dest.RequestDate, src => src.RequestDate.ToShortDateString())
            .Map(dest => dest.Status, src => src.Status);

        // RequestLoanDTO -> LoanRequest
        config.NewConfig<RequestLoanDTO, LoanRequest>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.Amount, src => src.AmountRequest)
            .Map(dest => dest.RequestDate, src => DateTime.UtcNow);

        // ApproveLoanDTO -> ApprovedLoan
        config.NewConfig<ApproveLoanDTO, ApprovedLoan>()
            .Map(dest => dest.LoanRequestId, src => src.LoanRequestId)
            .Map(dest => dest.Status, src => "Approved")
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow);

        // RejectLoanDTO -> ApprovedLoan
        config.NewConfig<RejectLoanDTO, ApprovedLoan>()
            .Map(dest => dest.LoanRequestId, src => src.LoanRequestId)
            .Map(dest => dest.Status, src => "Rejected")
            .Map(dest => dest.RejectionReason, src => src.RejectedReason)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow);

        // LoanRequest -> ApproveLoanResponse
        config.NewConfig<LoanRequest, ApproveLoanResponseDTO>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.RequestedAmount, src => src.Amount)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.InterestRate, src => src.TermInterestRate.Interest)
            .Map(dest => dest.ApprovalDate, src => DateTime.UtcNow.ToShortDateString());

        TypeAdapterConfig<LoanRequest, ApprovedLoan>.NewConfig()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.LoanRequestId, src => src.Id)
            .Map(dest => dest.RequestedAmount, src => src.Amount)
            .Map(dest => dest.InterestRate, src => src.TermInterestRate.Interest)
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Ignore(dest => dest.ApprovalDate)
            .Ignore(dest => dest.Status);

        // ApprovedLoan -> RejectLoanResponse
        config.NewConfig<ApprovedLoan, RejectLoanResponseDTO>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
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
    }
}
