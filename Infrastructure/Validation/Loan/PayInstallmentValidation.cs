using Core.DTOs.Payment;
using FluentValidation;

namespace Infrastructure.Validation.Loan;

public class PayInstallmentValidation : AbstractValidator<PayInstallmentsRequestDTO>
{
    public PayInstallmentValidation()
    {
        RuleFor(x => x.LoanRequestId).GreaterThan(0);
        RuleFor(x => x.InstallmentIds).NotEmpty().WithMessage("Debe especificar que cuotas va a pagar");
    }
}
