using Core.DTOs.RequestLoan;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanRequestRepository
{
    public Task<RequestLoanResponseDTO> CreateRequestLoan(LoanRequest loanRequest);
    public Task<LoanRequest> GetLoanRequestById(int loanRequestId);
    public Task<LoanRequest> UpdateLoanRequest(LoanRequest loanRequest);
    public Task<TermInterestRate> GetByMonths(ushort months);
}
