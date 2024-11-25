using Core.DTOs.Payment;

namespace Core.Interfaces.Repositories;

public interface IApproveLoanDetailRepository
{
    public Task<PaymentDetailResponse> GetLoanDetails(int loanRequestId);
}
