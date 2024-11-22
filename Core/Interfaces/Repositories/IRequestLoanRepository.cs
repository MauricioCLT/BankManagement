using Core.DTOs.RequestLoan;

namespace Core.Interfaces.Repositories;

public interface IRequestLoanRepository
{
    public Task<RequestLoanResponse> CreateRequestLoan(RequestLoan requestLoan);
}
