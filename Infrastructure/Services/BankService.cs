﻿using Mapster;
using Core.Interfaces.Services;
using Core.Interfaces.Repositories;
using Core.Entities;
using Core.DTOs.RequestLoan;
using Core.DTOs.Payment;
using Core.DTOs.ApproveLoan;

namespace Infrastructure.Services;

public class BankService : IBankService
{
    private readonly ILoanRequestRepository _loanRequestRepository;
    private readonly IApproveLoanRepository _approveLoanRepository;
    private readonly ILoanDetailedRepository _approveLoanDetailRepository;
    private readonly ILoanPaymentRepository _loanPaymentRepository;

    public BankService(
        ILoanRequestRepository loanRequestRepository,
        IApproveLoanRepository approveLoanRepository,
        ILoanDetailedRepository approveLoanDetailRepository,
        ILoanPaymentRepository loanPaymentRepository
        )
    {
        _loanRequestRepository = loanRequestRepository;
        _approveLoanRepository = approveLoanRepository;
        _approveLoanDetailRepository = approveLoanDetailRepository;
        _loanPaymentRepository = loanPaymentRepository;
    }

    public async Task<ApproveLoanResponseDTO> ApproveLoanRequest(ApproveLoanDTO approveLoanDTO)
    {
        var loanRequest = await _loanRequestRepository.GetLoanRequestById(approveLoanDTO.LoanRequestId);
        if (loanRequest == null)
            throw new Exception("No se encontró la solicitud de préstamo.");

        if (loanRequest.Status == "Approved" || loanRequest.Status == "Rejected")
            throw new Exception("La solicitud ya fue procesada anteriormente.");

        loanRequest.Status = "Approved";

        await _loanRequestRepository.UpdateLoanRequest(loanRequest);

        var approveLoan = loanRequest.Adapt<ApprovedLoan>();

        //var approveLoan = new ApprovedLoan
        //{
        //    CustomerId = loanRequest.CustomerId,
        //    LoanRequestId = loanRequest.Id,
        //    RequestedAmount = loanRequest.Amount,
        //    InterestRate = loanRequest.TermInterestRate.Interest,
        //    Months = loanRequest.Months,
        //    LoanType = loanRequest.LoanType,
        //    Status = "Approved",
        //    ApprovalDate = DateTime.UtcNow
        //};

        var installments = CalculateInstallments(
            approveLoan.RequestedAmount, approveLoan.InterestRate, approveLoan.Months);
        await _approveLoanRepository.SaveApprovedLoan(approveLoan, installments);

        return loanRequest.Adapt<ApproveLoanResponseDTO>();
    }

    public async Task<RejectLoanResponseDTO> RejectLoanRequest(RejectLoanDTO rejectLoanDTO)
    {
        var loanRequest = await _loanRequestRepository.GetLoanRequestById(rejectLoanDTO.LoanRequestId);
        if (loanRequest == null)
            throw new Exception("No se encontro la solicitud del préstamo.");

        if (loanRequest.Status == "Approved" || loanRequest.Status == "Rejected")
            throw new Exception("La solicitud ya fue procesada anteriormente.");

        if (string.IsNullOrWhiteSpace(rejectLoanDTO.RejectedReason))
            throw new Exception("Debe proporcionar una razón para el rechazo");

        loanRequest.Status = "Rejected";
        await _loanRequestRepository.UpdateLoanRequest(loanRequest);

        var rejectedLoan = new ApprovedLoan
        {
            LoanRequestId = loanRequest.Id,
            CustomerId = loanRequest.CustomerId,
            LoanType = loanRequest.LoanType,
            Months = loanRequest.Months,
            RequestedAmount = loanRequest.Amount,
            InterestRate = loanRequest.TermInterestRate.Interest,
            Status = "Rejected",
            ApprovalDate = DateTime.UtcNow,
            RejectionReason = rejectLoanDTO.RejectedReason
        };

        await _approveLoanRepository.SaveRejectedLoan(rejectedLoan);

        return rejectedLoan.Adapt<RejectLoanResponseDTO>();
    }

    public async Task<RequestLoanResponseDTO> CreateRequestLoan(RequestLoanDTO requestLoan)
    {
        if (requestLoan.AmountRequest <= 0)
            throw new Exception("El monto deber ser mayor a 0.");

        if (requestLoan.Months <= 0)
            throw new Exception("El plazo debe ser mayor a 0.");

        return await _loanRequestRepository.CreateRequestLoan(requestLoan);
    }

    public async Task<PaymentDetailResponseDTO> GetLoanDetails(int loanRequestId)
    {
        if (loanRequestId <= 0)
            throw new Exception("El id del prestamo no puede ser menor a 0 ni tampoco ser 0");

        var loanRequest = await _approveLoanDetailRepository.GetLoanDetailsInclude(loanRequestId);

        if (loanRequest == null)
            throw new Exception("No se encontró el préstamo solicitado");

        var totalAmount = loanRequest.Amount + loanRequest.Amount * (decimal)loanRequest.TermInterestRate.Interest / 100;
        var revenue = totalAmount - loanRequest.Amount;

        var completePayments = loanRequest.ApprovedLoan.Installments
            .Count(x => x.Status == "Complete" || x.Status == "Paid");

        var uncompletePayments = loanRequest.ApprovedLoan.Installments
            .Count(x => x.Status == "Pending");

        var nextDueDate = loanRequest.ApprovedLoan?.Installments
            .Where(x => x.Status == "Pending")
            .OrderBy(x => x.DueDate)
            .FirstOrDefault()?.DueDate
            .ToShortDateString();

        return new PaymentDetailResponseDTO
        {
            CustomerId = loanRequest.CustomerId,
            CustomerName = $"{loanRequest.Customer.FirstName} {loanRequest.Customer.LastName}",
            ApprovedDate = loanRequest.RequestDate.ToShortDateString(),
            RequestedAmount = loanRequest.Amount,
            TotalAmount = totalAmount,
            Revenue = revenue,
            Months = loanRequest.Months,
            LoanType = loanRequest.LoanType,
            InterestRate = loanRequest.TermInterestRate.Interest,
            CompletePayments = completePayments,
            UncompletePayments = uncompletePayments,
            NextDueDate = nextDueDate ?? "No hay pagos pendientes",
            PaymentStatus = nextDueDate == null ? "All payments completed" : "Pending payments"
        };
    }

    public async Task<PayInstallmentsResponseDTO> PayInstallments(PayInstallmentsRequestDTO request)
    {
        if (request == null || !request.InstallmentIds.Any())
            throw new Exception("No se especificaron cuotas para pagar.");

        var loanRequest = await _loanPaymentRepository.GetLoanRequestByIdWithDetails(request.LoanRequestId);
        if (loanRequest == null)
            throw new Exception("No se encontró la solicitud de préstamo.");

        var installments = await _loanPaymentRepository.GetPendingInstallments(loanRequest.ApprovedLoan.Id, request.InstallmentIds);
        if (installments.Count != request.InstallmentIds.Count)
            throw new Exception("Algunas cuotas ya fueron pagadas o no pertenecen al préstamo.");

        var installmentPayment = new InstallmentPayment
        {
            Installments = installments,
            PaymentDate = DateTime.UtcNow,
            Status = "Paid",
        };

        foreach (var installment in installments)
        {
            installment.Status = "Complete";
            _loanPaymentRepository.UpdateInstallment(installment);
            installmentPayment.Installments.Add(installment);
            installmentPayment.PaymentAmount += installment.PrincipalAmount + installment.InterestAmount;
        }

        await _loanPaymentRepository.AddInstallmentPayment(installmentPayment);


        await _loanPaymentRepository.SaveChangesAsync();

        var remainingInstallmentsCount = loanRequest.ApprovedLoan.Installments.Count(x => x.Status == "Pending");

        return new PayInstallmentsResponseDTO
        {
            LoanRequestId = loanRequest.Id,
            PaidInstallments = installments.Count,
            RemainingInstallments = remainingInstallmentsCount,
            StatusMessage = remainingInstallmentsCount == 0 ? "Todas las cuotas fueron pagadas." : "Quedan cuotas pendientes."
        };
    }

    public List<Installment> CalculateInstallments(decimal amount, float interestRate, ushort months)
    {
        var installments = new List<Installment>();
        decimal principalAmount = amount / months;
        decimal interestAmount = (amount * (decimal)interestRate) / 100 / months;

        for (ushort i = 1; i <= months; i++)
        {
            var firstDayOfMonth = new DateTime(
                DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc).AddMonths(i);

            installments.Add(new Installment
            {
                TotalAmount = principalAmount + interestAmount,
                PrincipalAmount = principalAmount,
                InterestAmount = interestAmount,
                DueDate = firstDayOfMonth,
                Status = "Pending"
            });
        }

        return installments;
    }
}
