using Core.DTOs.ApproveLoan;
using Core.DTOs.RequestLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LoanRequestRepository : ILoanRequestRepository
{
    private readonly AplicationDbContext _context;

    public LoanRequestRepository(AplicationDbContext context)
    {
        _context = context;
    }
    public async Task<LoanRequest> GetLoanRequestById(int loanRequestId)
    {
        return await _context.LoanRequests
            .Include(x => x.Customer)
            .Include(x => x.TermInterestRate)
            .FirstOrDefaultAsync(x => x.Id == loanRequestId);
    }

    public async Task<RequestLoanResponseDTO> CreateRequestLoan(LoanRequest loanRequest)
    {
        _context.LoanRequests.Add(loanRequest);
        await _context.SaveChangesAsync();
        return loanRequest.Adapt<RequestLoanResponseDTO>();
    }

    public async Task<LoanRequest> UpdateLoanRequest(LoanRequest loanRequest)
    {
        _context.LoanRequests.Update(loanRequest);
        await _context.SaveChangesAsync();

        return loanRequest;
    }

    public async Task<TermInterestRate> GetByMonths(ushort months)
    {
        return await _context.TermInterestRates.FirstOrDefaultAsync(x => x.Months == months);
    }
}
