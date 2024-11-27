using Core.DTOs.Payment;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoanPaymentRepository
{
    Task<LoanRequest> GetLoanRequestByIdWithDetails(int loanRequestId);
    Task<List<Installment>> GetPendingInstallments(int approvedLoanId, List<int> installmentIds);
    void UpdateInstallment(Installment installment);
    Task AddInstallmentPayment(InstallmentPayment installmentPayment);
    Task SaveChangesAsync();
}
