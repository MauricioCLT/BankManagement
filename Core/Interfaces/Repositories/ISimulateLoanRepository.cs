using Core.DTOs.SimulateLoan;

namespace Core.Interfaces.Repositories;

public interface ISimulateLoanRepository
{
    public Task<LoanSimulateResponse> SimulateLoan(decimal amount, ushort months);
}
