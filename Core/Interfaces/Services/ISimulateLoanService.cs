using Core.DTOs.RequestLoan;
using Core.DTOs.SimulateLoan;

namespace Core.Interfaces.Services;

/* 
2. Simulador de Cuota
Implementar una funcionalidad que permita al cliente simular el importe mensual de un préstamo basado en los siguientes datos de entrada:

Monto: Importe que desea solicitar.
Plazo: Número de meses.
*/

public interface ISimulateLoanService
{
    public Task<LoanSimulateResponse> SimulateCredit(LoanSimulate loanSimulate);
}
