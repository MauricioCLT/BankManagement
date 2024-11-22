using Core.DTOs.RequestLoan;
using Core.DTOs.SimulateLoan;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services;

public class BankService : IBankService
{
    private readonly IRequestLoanRepository _requestLoanRepository;

    public BankService(IRequestLoanRepository requestLoanRepository)
    {
        _requestLoanRepository = requestLoanRepository;
    }

    public async Task<RequestLoanResponse> CreateRequestLoan(RequestLoan requestLoan)
    {
        if (requestLoan.AmountRequest <= 0)
            throw new Exception("El monto deber ser mayor a 0.");

        if (requestLoan.Months <= 0)
            throw new Exception("El plazo debe ser mayor a 0.");

        return await _requestLoanRepository.CreateRequestLoan(requestLoan);
    }
}
