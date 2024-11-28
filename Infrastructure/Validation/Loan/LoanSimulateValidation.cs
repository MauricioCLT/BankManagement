using Core.DTOs.SimulateLoan;
using FluentValidation;

namespace Infrastructure.Validation.Loan;

public class LoanSimulateValidation : AbstractValidator<LoanSimulateDTO>
{
    public LoanSimulateValidation()
    {
        RuleFor(x => x.Amount)
            .ExclusiveBetween(5000, 100000000).WithMessage("El monto debe tener un rango de 5.000 a 500.000.000");
    }
}
