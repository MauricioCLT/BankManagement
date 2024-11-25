using Core.DTOs.Payment;

namespace Core.Interfaces.Repositories;

public interface ILoanPaymentRepository
{
    public Task<PayInstallmentsResponse> PayInstallments(PayInstallmentsRequest request);
}
