using Core.DTOs.Payment;
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

    public async Task<PayInstallmentsResponse> PayInstallments(PayInstallmentsRequest payInstallmentsRequest)
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
                   && x.Status != "Paid").ToListAsync();

        if (installments.Count != payInstallmentsRequest.InstallmentIds.Count)
            throw new Exception("Some installments are already paid or not part of the loan.");

        foreach (var installment in installments)
        {
            installment.Status = "Paid";
            _context.Installments.Update(installment);
        }

        await _context.SaveChangesAsync();

        var remainingInstallmentsCount = loanRequest.ApprovedLoan.Installments.Count(x => x.Status != "Paid");

        return new PayInstallmentsResponse
        {
            LoanRequestId = loanRequest.Id,
            PaidInstallmentsCount = installments.Count,
            RemainingInstallmentsCount = remainingInstallmentsCount,
            StatusMessage = remainingInstallmentsCount == 0 ? "All installments paid" : "Some installments still pending"
        };
    }
}
