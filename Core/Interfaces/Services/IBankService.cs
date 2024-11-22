using Core.DTOs.RequestLoan;

namespace Core.Interfaces.Services;

public interface IBankService
{
    public Task<RequestLoanResponse> CreateRequestLoan(RequestLoan requestLoan);
}
