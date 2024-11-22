using Core.Interfaces.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class BankController : BaseApiController
{
    private readonly AplicationDbContext _context;
    private readonly IBankService _bankService;

    public BankController(AplicationDbContext context, IBankService bankService)
    {
        _context = context;
        _bankService = bankService;
    }

    [HttpPost("Request-Loan")]
    public async Task<IActionResult> CreateRequestLoan([FromBody] RequestLoan requestLoan)
    {
        return Ok(await _bankService.CreateRequestLoan(requestLoan));
    }
}
