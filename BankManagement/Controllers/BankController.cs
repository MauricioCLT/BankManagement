using Core.DTOs.ApproveLoan;
using Core.DTOs.Payment;
using Core.DTOs.RequestLoan;
using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> CreateRequestLoan([FromBody] RequestLoanDTO requestLoan)
    {
        return Ok(await _bankService.CreateRequestLoan(requestLoan));
    }

    // [Authorize(Roles = "Admin")]
    [HttpPost("Approve-Loan")]
    public async Task<IActionResult> ApproveLoan([FromBody] ApproveLoanDTO approveLoanDTO)
    {
        return Ok(await _bankService.ApproveLoanRequest(approveLoanDTO));
    }

    // [Authorize(Roles = "Admin")]
    [HttpPost("Reject-Loan")]
    public async Task<IActionResult> RejectLoan([FromBody] RejectLoanDTO rejectLoanDTO)
    {
        return Ok(await _bankService.RejectLoanRequest(rejectLoanDTO));
    }

    [HttpGet("{loanRequestId}/Details")]
    public async Task<IActionResult> GetLoanDetails([FromRoute] int loanRequestId)
    {
        return Ok(await _bankService.GetLoanDetails(loanRequestId));
    }

    [HttpPost("{loanRequestId}/Pay-Installment")]
    public async Task<IActionResult> PayInstallment(PayInstallmentsRequestDTO request)
    {
        return Ok(await _bankService.PayInstallments(request));
    }
}
