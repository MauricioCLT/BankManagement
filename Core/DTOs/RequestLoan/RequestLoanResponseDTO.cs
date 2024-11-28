namespace Core.DTOs.RequestLoan;

public class RequestLoanResponseDTO
{
    public int CustomerId { get; set; }
    public int LoanRequestId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public ushort Months { get; set; }
    public decimal Amount { get; set; }
    public string RequestDate { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
}
