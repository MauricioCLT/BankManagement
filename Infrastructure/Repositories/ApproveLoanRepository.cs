using Core.DTOs.ApproveLoan;
using Core.DTOs.RequestLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ApproveLoanRepository : IApproveLoanRepository
{
    private readonly AplicationDbContext _context;

    public ApproveLoanRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApproveLoanResponse> ApproveLoanRequest(ApproveLoanDTO approveLoanDTO)
    {
        var entity = await _context.LoanRequests
            .Include(x => x.Customer)
            .Include(x => x.TermInterestRate)
            .FirstOrDefaultAsync(x => x.Id == approveLoanDTO.LoanRequestId);

        if (entity == null)
            throw new Exception("No se encontro el usuario");

        var approveLoan = approveLoanDTO.Adapt<ApprovedLoan>();
        approveLoan.CustomerId = entity.Customer.Id;
        approveLoan.ApprovalDate = DateTime.UtcNow;
        approveLoan.InterestRate = entity.TermInterestRate.Interest;
        approveLoan.LoanType = entity.LoanType;
        approveLoan.Status = entity.Status;
        approveLoan.Months = entity.Months;
        approveLoan.RequestedAmount = entity.Amount;
        entity.Status = "Approved";

        _context.ApprovedLoans.Add(approveLoan);
        await _context.SaveChangesAsync();
            
        return approveLoan.Adapt<ApproveLoanResponse>();
    }

    public async Task<RejectLoanResponse> RejectLoanRequest(RejectLoanDTO rejectLoanDTO)
    {
        // Buscar la entidad LoanRequest existente
        var entity = await _context.LoanRequests
            .Include(x => x.Customer)
            .Include(x => x.TermInterestRate)
            .FirstOrDefaultAsync(x => x.Id == rejectLoanDTO.LoanRequestId);

        if (entity == null)
            throw new Exception("No se encontró la solicitud de préstamo.");

        // Actualizar el estado de LoanRequest
        entity.Status = "Rejected";

        // Crear una nueva instancia de ApprovedLoan para registrar el rechazo
        var rejectedLoan = new ApprovedLoan
        {
            LoanRequestId = entity.Id,
            CustomerId = entity.CustomerId,
            LoanType = entity.LoanType,
            Months = entity.Months,
            RequestedAmount = entity.Amount,
            InterestRate = entity.TermInterestRate.Interest,
            Status = "Rejected",
            ApprovalDate = DateTime.UtcNow, // Puede ser la fecha de rechazo también
            RejectionReason = rejectLoanDTO.RejectedReason // Razón del rechazo
        };

        _context.ApprovedLoans.Add(rejectedLoan);
        await _context.SaveChangesAsync();

        return rejectedLoan.Adapt<RejectLoanResponse>();
    }
}
