using Core.DTOs.Payment;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DetailedPaymentRepository : IApproveLoanDetailRepository
    {
        private readonly AplicationDbContext _context;

        public DetailedPaymentRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentDetailResponse> GetLoanDetails(int loanRequestId)
        {
            var loanRequest = await _context.LoanRequests
                .Include(lr => lr.Customer)
                .Include(lr => lr.TermInterestRate)
                .Include(lr => lr.ApprovedLoan)
                .ThenInclude(al => al.Installments)
                .ThenInclude(i => i.InstallmentPayments)
                .FirstOrDefaultAsync(lr => lr.Id == loanRequestId);

            if (loanRequest == null)
                throw new Exception("Loan request not found.");

            var totalAmount = loanRequest.Amount + loanRequest.Amount * (decimal)loanRequest.TermInterestRate.Interest / 100;
            var revenue = totalAmount - loanRequest.Amount;
            var completePayments = loanRequest.ApprovedLoan.Installments
                .Sum(i => i.InstallmentPayments.Count(p => p.Status == "Completed"));
            var uncompletePayments = loanRequest.ApprovedLoan.Installments
                .Sum(i => i.InstallmentPayments.Count(p => p.Status == "Pending"));

            var nextDueDate = loanRequest.ApprovedLoan.Installments
                .Where(i => i.Status == "Pending")
                .OrderBy(i => i.DueDate)
                .FirstOrDefault()?.DueDate;

            return new PaymentDetailResponse
            {
                CustomerId = loanRequest.CustomerId,
                CustomerName = $"{loanRequest.Customer.FirstName} {loanRequest.Customer.LastName}",
                ApprovedDate = loanRequest.RequestDate,
                RequestedAmount = loanRequest.Amount,
                TotalAmount = totalAmount,
                Revenue = revenue,
                Months = loanRequest.Months,
                LoanType = loanRequest.LoanType,
                InterestRate = loanRequest.TermInterestRate.Interest,
                CompletePayments = completePayments,
                UncompletePayments = uncompletePayments,
                NextDueDate = nextDueDate,
                PaymentStatus = nextDueDate == null ? "All payments completed" : "Pending payments"
            };
        }
    }
}