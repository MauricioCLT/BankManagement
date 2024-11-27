using Core.DTOs.Payment;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LoanPaymentRepository : ILoanPaymentRepository
{
    private readonly AplicationDbContext _context;

    public LoanPaymentRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PayInstallmentsResponseDTO> PayInstallments(PayInstallmentsRequestDTO payInstallmentsRequest)
    {
        var loanRequest = await _context.LoanRequests
                .Include(x => x.ApprovedLoan)
                .ThenInclude(x => x.Installments)
                .FirstOrDefaultAsync(x => x.Id == payInstallmentsRequest.LoanRequestId);

        if (loanRequest == null)
            throw new Exception("Loan request not found.");

        // Validar cuotas disponibles
        var installments = await _context.Installments
            .Where(x => payInstallmentsRequest.InstallmentIds.Contains(x.Id)
                   && x.ApprovedLoanId == loanRequest.ApprovedLoan.Id
                   && x.Status != "Complete").ToListAsync();

        if (installments.Count != payInstallmentsRequest.InstallmentIds.Count)
            throw new Exception("Some installments are already paid or not part of the loan.");

        foreach (var installment in installments)
        {
            installment.Status = "Complete";
            _context.Installments.Update(installment);

            var installmentPayment = new InstallmentPayment
            {
                InstallmentId = installment.Id,
                PaymentDate = DateTime.UtcNow,
                Status = "Paid",
                PaymentAmount = installment.PrincipalAmount + installment.InterestAmount,
            };
            await _context.InstallmentPayments.AddAsync(installmentPayment);
        }

        await _context.SaveChangesAsync();

        var remainingInstallmentsCount = loanRequest.ApprovedLoan.Installments.Count(x => x.Status != "Complete");

        return new PayInstallmentsResponseDTO
        {
            LoanRequestId = loanRequest.Id,
            PaidInstallments = installments.Count,
            RemainingInstallments = remainingInstallmentsCount,
            StatusMessage = remainingInstallmentsCount == 0 ? "Todas las cuotas fueron pagadas." : "Queda(n) alguna(s) cuota(s) pendiente(s)."
        };
    }
}
