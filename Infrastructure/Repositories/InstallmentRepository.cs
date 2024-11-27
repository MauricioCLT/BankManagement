using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class InstallmentRepository : IInstallmentRepository
{
    private readonly AplicationDbContext _context;

    public InstallmentRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Installment>> GetAllInstallmentsByLoanId(int loanId)
    {
        return await _context.Installments
            .Where(x => x.ApprovedLoanId == loanId)
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<Installment>> GetOverdueInstallments()
    {
        return await _context.Installments
            .Include(x => x.ApprovedLoan)
                .ThenInclude(x => x.Customer)
            .Where(x => x.Status == "Pending" && x.DueDate < DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<List<Installment>> GetPaidInstallmentsByLoanId(int loanId)
    {
        return await _context.Installments
            .Where(x => x.ApprovedLoanId == loanId && x.Status == "Complete" || x.Status == "Paid")
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<Installment>> GetPendingInstallmentsByLoanId(int loanId)
    {
        return await _context.Installments
            .Where(x => x.ApprovedLoanId == loanId && x.Status == "Pending")
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }
}
