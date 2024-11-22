using Core.DTOs.SimulateLoan;
using FluentValidation;

namespace Infrastructure.Validation.Loan;

public class LoanSimulateValidation : AbstractValidator<LoanSimulate>
{
    public LoanSimulateValidation()
    {
        RuleFor(x => x.Amount)
            .ExclusiveBetween(5000, 100000000);
    }
}
