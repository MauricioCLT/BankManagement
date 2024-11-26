using Core.DTOs.ApproveLoan;
using Core.DTOs.RequestLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Mapster;
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

    public async Task<RequestLoanResponseDTO> CreateRequestLoan(RequestLoanDTO requestLoanDTO)
    {
        var termInterestRate = await _context.TermInterestRates
            .FirstOrDefaultAsync(x => x.Months == requestLoanDTO.Months);

        if (termInterestRate == null)
            throw new Exception($"No se encontró una tasa de interés para el plazo de {requestLoanDTO.Months} meses.");

        var loanRequest = requestLoanDTO.Adapt<LoanRequest>();
        loanRequest.TermInterestRateId = termInterestRate.Id;
        loanRequest.RequestDate = DateTime.UtcNow;

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
}
