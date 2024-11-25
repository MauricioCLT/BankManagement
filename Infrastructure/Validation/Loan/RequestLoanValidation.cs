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
            .ExclusiveBetween(10000, 10000000);

        RuleFor(x => x.LoanType)
            .NotEmpty()
            .Must(type => ValidTypes.Contains(type))
            .WithMessage(type => $"El tipo de prestamo {type} no es valido. Los tipos validos son: {string.Join(", ", ValidTypes)}.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0);
    }
}
