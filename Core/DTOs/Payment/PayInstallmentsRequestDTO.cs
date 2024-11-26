namespace Core.DTOs.Payment;

public class PayInstallmentsRequestDTO
{
    public int LoanRequestId { get; set; }
    public List<int> InstallmentIds { get; set; } = [];
}
