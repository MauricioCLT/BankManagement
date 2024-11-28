namespace Core.DTOs.ApproveLoan;

public class RejectLoanDTO
{
    public int LoanRequestId { get; set; }
    public string RejectedReason { get; set; } = string.Empty;
}
