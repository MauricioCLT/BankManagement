using Core.DTOs.SimulateLoan;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class SimulateLoanController : BaseApiController
{
    private readonly ISimulateLoanService _loanService;

    public SimulateLoanController(ISimulateLoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost("Simulate")]
    public async Task<IActionResult> SimulateCredit([FromBody] LoanSimulate loanSimulate)
    {       
        return Ok(await _loanService.SimulateCredit(loanSimulate));
    }
}
