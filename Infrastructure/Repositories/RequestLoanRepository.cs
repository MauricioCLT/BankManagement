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

    public async Task<RequestLoanResponse> CreateRequestLoan(RequestLoan requestLoan)
    {
        var termInterstRate = await _context.TermInterestRates.FirstOrDefaultAsync(x => x.Months == requestLoan.Months);

        if (termInterstRate == null)
            throw new Exception("El plazo ingresado no es valido!");

        var loanRequest = new LoanRequest
        {
            CustomerId = requestLoan.CustomerId,
            LoanType = requestLoan.LoanType,
            // Months = requestLoan.Months,
            Amount = requestLoan.AmountRequest,
            Status = "Pending",
            TermInterestRateId = termInterstRate.Id
        };

        // var loanRequestAdap = requestLoan.Adapt<LoanRequest>();

        _context.LoanRequests.Add(loanRequest);
        await _context.SaveChangesAsync();

        var requestLoanResponse = new RequestLoanResponse
        {
            CustomerId = loanRequest.CustomerId,
            LoanType = loanRequest.LoanType,
            Amount = loanRequest.Amount,
            // Months = loanRequest,
            Status = requestLoan.Status
        };

        return requestLoanResponse;
    }
}
