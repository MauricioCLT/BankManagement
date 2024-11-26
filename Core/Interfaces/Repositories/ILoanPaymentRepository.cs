using Core.DTOs.Payment;

namespace Core.Interfaces.Repositories;

public interface ILoanPaymentRepository
{
    public Task<PayInstallmentsResponseDTO> PayInstallments(PayInstallmentsRequestDTO request);
}
