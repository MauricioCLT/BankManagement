using Core.DTOs.Payment;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanDetailedRepository
{
    // public Task<PaymentDetailResponseDTO> GetLoanDetails(int loanRequestId);
    public Task<LoanRequest> GetLoanDetailsInclude(int loanRequestId);
}
