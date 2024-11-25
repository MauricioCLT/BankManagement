namespace Core.DTOs.ApproveLoan;

public class ApproveLoanResponse
{
    public int CustomerId { get; set; }
    public DateTime ApprovalDate { get; set; }
    public decimal RequestedAmount { get; set; }
    public ushort Months { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public float InterestRate { get; set; }
}
