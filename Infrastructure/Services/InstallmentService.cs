using Core.DTOs.Installment;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;
public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;

    public InstallmentService(IInstallmentRepository installmentRepository)
    {
        _installmentRepository = installmentRepository;
    }

    public async Task<List<InstallmentResponseDTO>> GetInstallmentsByFilter(int loanId, string filter)
    {
        List<Installment> installments = filter switch
        {
            "Paid" => await _installmentRepository.GetPaidInstallmentsByLoanId(loanId),
            "Complete" => await _installmentRepository.GetPaidInstallmentsByLoanId(loanId),
            "Pending" => await _installmentRepository.GetPendingInstallmentsByLoanId(loanId),
            _ => await _installmentRepository.GetAllInstallmentsByLoanId(loanId),
        };

        return installments.Select(x => new InstallmentResponseDTO
        {
            Id = x.Id,
            DueDate = x.DueDate,
            Status = x.Status,
            TotalAmount = x.TotalAmount
        }).ToList();
    }

    public async Task<List<OverdueInstallmentDTO>> GetOverdueInstallments()
    {
        var installmentOverdue = await _installmentRepository.GetOverdueInstallments();
        return installmentOverdue.Adapt<List<OverdueInstallmentDTO>>();
    }
}
