using Core.DTOs.Payment;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoanDetailedRepository : ILoanDetailedRepository
    {
        private readonly AplicationDbContext _context;

        public LoanDetailedRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRequest> GetLoanDetailsInclude(int loanRequestId)
        {
            return await _context.LoanRequests
                .Include(x => x.Customer)
                .Include(x => x.TermInterestRate)
                .Include(x => x.ApprovedLoan)
                    .ThenInclude(x => x.Installments)
                    .ThenInclude(x => x.InstallmentPayment)
                .FirstOrDefaultAsync(x => x.Id == loanRequestId);
        }
    }
}