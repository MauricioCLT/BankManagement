using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class FilterLoansController : BaseApiController
{
    private readonly IInstallmentService _installmentService;

    public FilterLoansController(IInstallmentService installmentService)
    {
        _installmentService = installmentService;
    }

    [HttpGet("{loanId}/installments")]
    public async Task<IActionResult> GetInstallments(int loanId, [FromQuery] string filter = "all")
    {
        return Ok(await _installmentService.GetInstallmentsByFilter(loanId, filter));
    }

    [HttpGet("Overdue")]
    public async Task<IActionResult> GetOverdueInstallment()
    {
        return Ok(await _installmentService.GetOverdueInstallments());
    }
}
