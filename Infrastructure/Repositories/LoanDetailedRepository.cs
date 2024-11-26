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
                    .ThenInclude(x => x.InstallmentPayments)
                .FirstOrDefaultAsync(x => x.Id == loanRequestId);
        }
        /*
        public async Task<PaymentDetailResponseDTO> GetLoanDetails(int loanRequestId)
        {
            
            //var loanRequest = await _context.LoanRequests
            //    .Include(x => x.Customer)
            //    .Include(x => x.TermInterestRate)
            //    .Include(x => x.ApprovedLoan)
            //    .ThenInclude(x => x.Installments)
            //    .ThenInclude(x => x.InstallmentPayments)
            //    .FirstOrDefaultAsync(x => x.Id == loanRequestId);

            //if (loanRequest == null)
            //    throw new Exception("Loan request not found.");

            //var totalAmount = loanRequest.Amount + loanRequest.Amount * (decimal)loanRequest.TermInterestRate.Interest / 100;
            //var revenue = totalAmount - loanRequest.Amount;

            //var completePayments = loanRequest.ApprovedLoan.Installments
            //    .Sum(x => x.InstallmentPayments.Count(p => p.Status == "Completed"));

            //var uncompletePayments = loanRequest.ApprovedLoan.Installments
            //    .Sum(x => x.InstallmentPayments.Count(p => p.Status == "Pending"));

            //var nextDueDate = loanRequest.ApprovedLoan.Installments
            //    .Where(x => x.Status == "Pending")
            //    .OrderBy(x => x.DueDate)
            //    .FirstOrDefault()?.DueDate;

            //return new PaymentDetailResponseDTO
            //{
            //    CustomerId = loanRequest.CustomerId,
            //    CustomerName = $"{loanRequest.Customer.FirstName} {loanRequest.Customer.LastName}",
            //    ApprovedDate = loanRequest.RequestDate,
            //    RequestedAmount = loanRequest.Amount,
            //    TotalAmount = totalAmount,
            //    Revenue = revenue,
            //    Months = loanRequest.Months,
            //    LoanType = loanRequest.LoanType,
            //    InterestRate = loanRequest.TermInterestRate.Interest,
            //    CompletePayments = completePayments,
            //    UncompletePayments = uncompletePayments,
            //    NextDueDate = nextDueDate,
            //    PaymentStatus = nextDueDate == null ? "All payments completed" : "Pending payments"
            //};
        }
        */
    }
}