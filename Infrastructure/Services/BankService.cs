using Core.DTOs.ApproveLoan;
using Core.DTOs.Payment;
using Core.DTOs.RequestLoan;
using Core.DTOs.SimulateLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class BankService : IBankService
{
    private readonly IRequestLoanRepository _requestLoanRepository;
    private readonly IApproveLoanRepository _approveLoanRepository;
    private readonly IApproveLoanDetailRepository _approveLoanDetailRepository;
    private readonly ILoanPaymentRepository _loanPaymentRepository;

    public BankService(
        IRequestLoanRepository requestLoanRepository,
        IApproveLoanRepository approveLoanRepository,
        IApproveLoanDetailRepository approveLoanDetailRepository,
        ILoanPaymentRepository loanPaymentRepository
        )
    {
        _requestLoanRepository = requestLoanRepository;
        _approveLoanRepository = approveLoanRepository;
        _approveLoanDetailRepository = approveLoanDetailRepository;
        _loanPaymentRepository = loanPaymentRepository;
    }

    public async Task<ApproveLoanResponse> ApproveLoanRequest(ApproveLoanDTO approveLoanDTO)
    {
        if (approveLoanDTO == null)
            throw new Exception("");

        return await _approveLoanRepository.ApproveLoanRequest(approveLoanDTO);
    }

    public async Task<RequestLoanResponse> CreateRequestLoan(RequestLoanDTO requestLoan)
    {
        if (requestLoan.AmountRequest <= 0)
            throw new Exception("El monto deber ser mayor a 0.");

        if (requestLoan.Months <= 0)
            throw new Exception("El plazo debe ser mayor a 0.");

        return await _requestLoanRepository.CreateRequestLoan(requestLoan);
    }

    public async Task<PaymentDetailResponse> GetLoanDetails(int loanRequestId)
    {
        if (loanRequestId <= 0)
            throw new Exception("El id del prestamo no puede ser menor a 0 ni 0");

        return await _approveLoanDetailRepository.GetLoanDetails(loanRequestId);
    }

    public async Task<PayInstallmentsResponse> PayInstallments(PayInstallmentsRequest request)
    {
        if (request == null)
            throw new Exception("");

        return await _loanPaymentRepository.PayInstallments(request);
    }

    public async Task<RejectLoanResponse> RejectLoanRequest(RejectLoanDTO rejectLoanDTO)
    {
        if (rejectLoanDTO == null)
            throw new Exception("No se Encontró el usuario");

        return await _approveLoanRepository.RejectLoanRequest(rejectLoanDTO);
    }
}
