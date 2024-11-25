namespace Core.DTOs.ApproveLoan;

public class RejectLoanDTO
{
    public int LoanRequestId { get; set; }
    public int CustomerId { get; set; }
    public string RejectedReason { get; set; }
}
