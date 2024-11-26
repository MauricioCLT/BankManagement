using Core.DTOs.ApproveLoan;
using Core.DTOs.RequestLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApproveLoanRepository : IApproveLoanRepository
{
    private readonly AplicationDbContext _context;

    public ApproveLoanRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveApprovedLoan(ApprovedLoan approvedLoan, List<Installment> installments)
    {
        _context.ApprovedLoans.Add(approvedLoan);
        await _context.SaveChangesAsync();

        foreach (var installment in installments)
        {
            installment.ApprovedLoanId = approvedLoan.Id;
        }

        _context.Installments.AddRange(installments);
        await _context.SaveChangesAsync();
    }

    public async Task SaveRejectedLoan(ApprovedLoan rejectedLoan)
    {
        _context.ApprovedLoans.Add(rejectedLoan);
        await _context.SaveChangesAsync();
    }
}
