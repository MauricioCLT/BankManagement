namespace Core.DTOs.Payment;

public class PayInstallmentsResponseDTO
{
    public int LoanRequestId { get; set; }
    public int PaidInstallments { get; set; }
    public int RemainingInstallments { get; set; }
    public string StatusMessage { get; set; } = string.Empty;
}
