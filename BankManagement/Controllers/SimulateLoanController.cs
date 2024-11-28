using Core.DTOs.SimulateLoan;
using Core.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class SimulateLoanController : BaseApiController
{
    private readonly ISimulateLoanService _loanService;
    private readonly IValidator<LoanSimulateDTO> _loanSimulateValidator;

    public SimulateLoanController(
        ISimulateLoanService loanService,
        IValidator<LoanSimulateDTO> loanSimulateValidator)
    {
        _loanService = loanService;
        _loanSimulateValidator = loanSimulateValidator;
    }

    [HttpPost("Simulate")]
    public async Task<IActionResult> SimulateCredit([FromBody] LoanSimulateDTO loanSimulate)
    {       
        var result = await _loanSimulateValidator.ValidateAsync(loanSimulate);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        return Ok(await _loanService.SimulateCredit(loanSimulate));
    }
}
