using Core.DTOs.Payment;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanDetailedRepository
{
    public Task<LoanRequest> GetLoanDetailsInclude(int loanRequestId);
}
