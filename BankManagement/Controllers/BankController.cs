using Core.DTOs.ApproveLoan;
using Core.DTOs.Payment;
using Core.DTOs.RequestLoan;
using Core.Interfaces.Services;
using FluentValidation;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers;

public class BankController : BaseApiController
{
    private readonly IBankService _bankService;
    private readonly IValidator<RequestLoanDTO> _validateRequestLoanDTO;
    private readonly IValidator<ApproveLoanDTO> _validateApproveLoanDTO;
    private readonly IValidator<RejectLoanDTO> _validateRejectLoanDTO;
    private readonly IValidator<PayInstallmentsRequestDTO> _validatePayInstallmentDTO;

    public BankController(
        IBankService bankService,
        IValidator<RequestLoanDTO> validateRequestLoanDTO,
        IValidator<ApproveLoanDTO> validateApproveLoanDTO,
        IValidator<RejectLoanDTO> validateRejectLoanDTO,
        IValidator<PayInstallmentsRequestDTO> validatePayInstallmentDTO)
    {
        _bankService = bankService;
        _validateRequestLoanDTO = validateRequestLoanDTO;
        _validateApproveLoanDTO = validateApproveLoanDTO;
        _validateRejectLoanDTO = validateRejectLoanDTO;
        _validatePayInstallmentDTO = validatePayInstallmentDTO;
    }

    [HttpPost("Request-Loan")]
    public async Task<IActionResult> CreateRequestLoan([FromBody] RequestLoanDTO requestLoan)
    {
        var result = await _validateRequestLoanDTO.ValidateAsync(requestLoan);
        if (!result.IsValid) 
        {
            var errors = result.Errors.Select(x => new
            {
                x.PropertyName,
                x.ErrorMessage
            });
            return BadRequest(errors);
        }

        return Ok(await _bankService.CreateRequestLoan(requestLoan));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Approve-Loan")]
    public async Task<IActionResult> ApproveLoan([FromBody] ApproveLoanDTO approveLoanDTO)
    {
        var result = await _validateApproveLoanDTO.ValidateAsync(approveLoanDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        return Ok(await _bankService.ApproveLoanRequest(approveLoanDTO));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Reject-Loan")]
    public async Task<IActionResult> RejectLoan([FromBody] RejectLoanDTO rejectLoanDTO)
    {
        var result = await _validateRejectLoanDTO.ValidateAsync(rejectLoanDTO);
        if (!result.IsValid) 
            return BadRequest(result.Errors);

        return Ok(await _bankService.RejectLoanRequest(rejectLoanDTO));
    }

    [HttpGet("{loanRequestId}/Details")]
    public async Task<IActionResult> GetLoanDetails([FromRoute] int loanRequestId)
    {
        return Ok(await _bankService.GetLoanDetails(loanRequestId));
    }

    [HttpPost("Pay-Installment")]
    public async Task<IActionResult> PayInstallment([FromBody] PayInstallmentsRequestDTO request)
    {
        var result = await _validatePayInstallmentDTO.ValidateAsync(request);
        if (!result.IsValid) 
            return BadRequest(result.Errors);

        return Ok(await _bankService.PayInstallments(request));
    }
}
