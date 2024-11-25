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

    public async Task<PayInstallmentsResponse> PayInstallments(PayInstallmentsRequest request)
    {
        var loanRequest = await _context.LoanRequests
                .Include(lr => lr.ApprovedLoan)
                .ThenInclude(al => al.Installments)
                .FirstOrDefaultAsync(lr => lr.Id == request.LoanRequestId);

        if (loanRequest == null)
            throw new Exception("Loan request not found.");

        // Validar cuotas disponibles
        var installments = await _context.Installments
            .Where(i => request.InstallmentIds.Contains(i.Id) && i.ApprovedLoanId == loanRequest.ApprovedLoan.Id && i.Status != "Paid")
            .ToListAsync();

        if (installments.Count != request.InstallmentIds.Count)
            throw new Exception("Some installments are already paid or not part of the loan.");

        foreach (var installment in installments)
        {
            installment.Status = "Paid";
            _context.Installments.Update(installment);
        }

        await _context.SaveChangesAsync();

        var remainingInstallmentsCount = loanRequest.ApprovedLoan.Installments.Count(i => i.Status != "Paid");

        return new PayInstallmentsResponse
        {
            LoanRequestId = loanRequest.Id,
            PaidInstallmentsCount = installments.Count,
            RemainingInstallmentsCount = remainingInstallmentsCount,
            StatusMessage = remainingInstallmentsCount == 0 ? "All installments paid" : "Some installments still pending"
        };
    }
}
