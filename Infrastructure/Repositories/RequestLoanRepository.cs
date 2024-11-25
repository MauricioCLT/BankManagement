using Core.DTOs.RequestLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RequestLoanRepository : IRequestLoanRepository
{
    private readonly AplicationDbContext _context;

    public RequestLoanRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RequestLoanResponse> CreateRequestLoan(RequestLoanDTO requestLoanDTO)
    {
        var termInterstRate = await _context.TermInterestRates.FirstOrDefaultAsync(x => x.Months == requestLoanDTO.Months);

        if (termInterstRate == null)
            throw new Exception("El plazo ingresado no es valido!");

        var loanRequest = new LoanRequest
        {
            CustomerId = requestLoanDTO.CustomerId,
            LoanType = requestLoanDTO.LoanType,
            Months = requestLoanDTO.Months,
            Amount = requestLoanDTO.AmountRequest,
            TermInterestRateId = termInterstRate.Id,
            RequestDate = DateTime.UtcNow,
        };

        _context.LoanRequests.Add(loanRequest);
        await _context.SaveChangesAsync();

        return loanRequest.Adapt<RequestLoanResponse>();
    }
}
