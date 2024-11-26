using Core.DTOs.ApproveLoan;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IApproveLoanRepository
{
    public Task SaveApprovedLoan (ApprovedLoan approvedLoan, List<Installment> installments);
    public Task SaveRejectedLoan(ApprovedLoan rejectedLoan);
}
