using Core.DTOs.ApproveLoan;
using FluentValidation;

namespace Infrastructure.Validation.Loan;

public class ApproveLoanValidation : AbstractValidator<ApproveLoanDTO>
{
    public ApproveLoanValidation()
    {
        RuleFor(x => x.LoanRequestId).GreaterThan(0);
    }
}
