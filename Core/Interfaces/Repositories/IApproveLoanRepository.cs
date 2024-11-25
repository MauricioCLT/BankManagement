using Core.DTOs.ApproveLoan;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IApproveLoanRepository
{
    public Task<ApproveLoanResponse> ApproveLoanRequest(ApproveLoanDTO approveLoanDTO);
    public Task<RejectLoanResponse> RejectLoanRequest(RejectLoanDTO rejectLoanDTO);
}
