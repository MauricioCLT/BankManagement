using Core.DTOs.Installment;

namespace Core.Interfaces.Services;

public interface IInstallmentService
{
    public Task<List<InstallmentResponseDTO>> GetInstallmentsByFilter(int loanId, string filter);
    public Task<List<OverdueInstallmentDTO>> GetOverdueInstallments();
}
