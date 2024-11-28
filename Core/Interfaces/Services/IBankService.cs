using Core.DTOs.ApproveLoan;
using Core.DTOs.Payment;
using Core.DTOs.RequestLoan;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface IBankService
{
    public Task<RequestLoanResponseDTO> CreateRequestLoan(RequestLoanDTO requestLoan);
    public Task<ApproveLoanResponseDTO> ApproveLoanRequest(ApproveLoanDTO approveLoanDTO);
    public Task<RejectLoanResponseDTO> RejectLoanRequest(RejectLoanDTO rejectLoanDTO);
    public Task<PaymentDetailResponseDTO> GetLoanDetails(int loanRequestId);
    public Task<PayInstallmentsResponseDTO> PayInstallments(PayInstallmentsRequestDTO request);


    public List<Installment> CalculateInstallments(decimal amount, float interestRate, ushort months);
}
