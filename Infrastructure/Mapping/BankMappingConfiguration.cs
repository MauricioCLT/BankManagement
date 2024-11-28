using Core.DTOs.ApproveLoan;
using Core.DTOs.Customer;
using Core.DTOs.Installment;
using Core.DTOs.Payment;
using Core.DTOs.RequestLoan;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class BankMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoanRequest, PaymentDetailResponseDTO>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.CustomerName, src => $"{src.Customer.FirstName} {src.Customer.LastName}")
            .Map(dest => dest.ApprovedDate, src => src.RequestDate.ToShortDateString())
            .Map(dest => dest.RequestedAmount, src => src.Amount)
            .Map(dest => dest.TotalAmount, src => src.Amount + src.Amount * (decimal)src.TermInterestRate.Interest / 100)
            .Map(dest => dest.Revenue, src => (src.Amount * (decimal)src.TermInterestRate.Interest / 100))
            .Map(dest => dest.Months, src => src.Months)
            .Map(dest => dest.LoanType, src => src.LoanType)
            .Map(dest => dest.InterestRate, src => src.TermInterestRate.Interest)
            .Map(dest => dest.CompletePayments, src => src.ApprovedLoan.Installments.Count(x => x.Status == "Complete" || x.Status == "Paid"))
            .Map(dest => dest.UncompletePayments, src => src.ApprovedLoan.Installments.Count(x => x.Status == "Pending"))
            .Map(dest => dest.NextDueDate, src => GetNextDueDate(src.ApprovedLoan))
            .Map(dest => dest.PaymentStatus, src => src.ApprovedLoan.Installments.Any(x => x.Status == "Pending") ? "Pending payments" : "All payments completed");

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

    public static string GetNextDueDate(ApprovedLoan approvedLoan)
    {
        var nextInstallment = approvedLoan?.Installments
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.DueDate)
            .FirstOrDefault();

        return nextInstallment?.DueDate.ToShortDateString() ?? "No hay pagos pendientes";
    }
}
