using Core.DTOs.SimulateLoan;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class SimulateLoanService : ILoanService
{ 
    private readonly ISimulateLoanRepository _simulateLoanRepository;

    public SimulateLoanService(ISimulateLoanRepository simulateLoanRepository)
    {
        _simulateLoanRepository = simulateLoanRepository;
    }

    public async Task<LoanSimulateResponse> SimulateCredit(LoanSimulate loanSimulate)
    {
        if (loanSimulate.Amount <= 0)
            throw new Exception("El Monto debe ser Mayor a Cero");

        if (loanSimulate.Months <= 0)
            throw new Exception("La cantidad de meses debe ser mayor a Cero");

        return await _simulateLoanRepository.SimulateLoan(loanSimulate.Amount, loanSimulate.Months);
    }
}
