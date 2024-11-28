using Core.DTOs.RequestLoan;
using FluentValidation;

namespace Infrastructure.Validation.Loan;

public class RequestLoanValidation : AbstractValidator<RequestLoanDTO>
{
    private static readonly IReadOnlyList<string> ValidTypes = new List<string>()
    {
        "Personal",
        "Hipotecario",
        "Automotriz"
    }.AsReadOnly();

    public RequestLoanValidation()
    {
        RuleFor(x => x.AmountRequest)
            .ExclusiveBetween(5000, 500000000).WithMessage("El monto debe tener un rango de 5.000 a 500.000.000");

        RuleFor(x => x.LoanType)
            .NotEmpty()
            .Must(type => ValidTypes.Contains(type))
            .WithMessage(type => $"El tipo de prestamo no es valido. Los tipos validos son: {string.Join(", ", ValidTypes)}.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("El id no puede ser negativo.");
    }
}
