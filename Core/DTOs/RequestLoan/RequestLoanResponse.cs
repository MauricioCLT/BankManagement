namespace Core.DTOs.RequestLoan;

public class RequestLoanResponse
{
    public int CustomerId { get; set; }
    public int LoanRequestId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public ushort Months { get; set; }
    public decimal Amount { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } = "Pending";
}
