namespace Core.DTOs.Payment;

public class PayInstallmentsRequest
{
    public int LoanRequestId { get; set; }
    public List<int> InstallmentIds { get; set; } = [];
}
