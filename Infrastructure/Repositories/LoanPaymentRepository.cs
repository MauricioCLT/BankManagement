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

    public async Task<LoanRequest> GetLoanRequestByIdWithDetails(int loanRequestId)
    {
        return await _context.LoanRequests
            .Include(x => x.ApprovedLoan)
            .ThenInclude(x => x.Installments)
            .FirstOrDefaultAsync(x => x.Id == loanRequestId);
    }

    public async Task<List<Installment>> GetPendingInstallments(int approvedLoanId, List<int> installmentIds)
    {
        return await _context.Installments
            .Where(x => installmentIds.Contains(x.Id) && x.ApprovedLoanId == approvedLoanId && x.Status != "Complete")
            .ToListAsync();
    }

    public void UpdateInstallment(Installment installment)
    {
        _context.Installments.Update(installment);
    }

    public async Task AddInstallmentPayment(InstallmentPayment installmentPayment)
    {
        await _context.InstallmentPayments.AddAsync(installmentPayment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
