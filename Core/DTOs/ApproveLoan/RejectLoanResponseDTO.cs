namespace Core.DTOs.ApproveLoan;

public class RejectLoanResponseDTO
{
    public int CustomerId { get; set; }
    public string RejectReason { get; set; } = string.Empty;
}
