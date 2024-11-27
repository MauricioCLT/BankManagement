namespace Core.Request;

public class CreateLoanRequest
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public ushort Months { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } = "Pending";

    public int CustomerId { get; set; }
}
