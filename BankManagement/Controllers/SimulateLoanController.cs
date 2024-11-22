using Core.DTOs.SimulateLoan;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class SimulateLoanController : BaseApiController
{
    private readonly ILoanService _loanService;

    public SimulateLoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost("Simulate-Credit")]
    public async Task<IActionResult> SimulateCredit([FromBody] LoanSimulate loanSimulate)
    {
        var simulate = await _loanService.SimulateCredit(loanSimulate);

        if (simulate == null)
            throw new Exception("");
        
        return Ok(simulate);
    }
}
