using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IInstallmentRepository
{
    public Task<List<Installment>> GetAllInstallmentsByLoanId(int loanId);
    public Task<List<Installment>> GetPaidInstallmentsByLoanId(int loanId);
    public Task<List<Installment>> GetPendingInstallmentsByLoanId(int loanId);
    public Task<List<Installment>> GetOverdueInstallments();
}
