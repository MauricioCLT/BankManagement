namespace Core.DTOs.ApproveLoan;

public class RejectLoanResponse
{
    public int CustomerId { get; set; }
    public string RejectReason { get; set; } = string.Empty;
}
