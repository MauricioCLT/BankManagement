using Core.DTOs.ApproveLoan;
using Core.DTOs.RequestLoan;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanRequestRepository
{
    public Task<RequestLoanResponseDTO> CreateRequestLoan(RequestLoanDTO requestLoan);
    public Task<LoanRequest> GetLoanRequestById(int loanRequestId);
    public Task<LoanRequest> UpdateLoanRequest(LoanRequest loanRequest);
}
