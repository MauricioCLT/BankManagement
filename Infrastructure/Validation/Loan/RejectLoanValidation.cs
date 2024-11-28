using Core.DTOs.ApproveLoan;
using FluentValidation;

namespace Infrastructure.Validation.Loan;

public class RejectLoanValidation : AbstractValidator<RejectLoanDTO>
{
    public RejectLoanValidation()
    {
        RuleFor(x => x.LoanRequestId).GreaterThan(0);
        RuleFor(x => x.RejectedReason).NotEmpty().NotNull().WithMessage("Debe especificar una razón de rechazo.");
    }
}
