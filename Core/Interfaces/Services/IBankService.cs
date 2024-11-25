using Core.DTOs.ApproveLoan;
using Core.DTOs.Payment;
using Core.DTOs.RequestLoan;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface IBankService
{
    public Task<RequestLoanResponse> CreateRequestLoan(RequestLoanDTO requestLoan);
    public Task<ApproveLoanResponse> ApproveLoanRequest(ApproveLoanDTO approveLoanDTO);
    public Task<RejectLoanResponse> RejectLoanRequest(RejectLoanDTO rejectLoanDTO);
    public Task<PaymentDetailResponse> GetLoanDetails(int loanRequestId);
    public Task<PayInstallmentsResponse> PayInstallments(PayInstallmentsRequest request);
}
