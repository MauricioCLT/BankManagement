namespace Core.DTOs.Payment;

public class PayInstallmentsResponseDTO
{
    public int LoanRequestId { get; set; }
    public int PaidInstallmentsCount { get; set; }
    public int RemainingInstallmentsCount { get; set; }
    public string StatusMessage { get; set; } = string.Empty;
}
