namespace Core.Entities;

public class LoanRequest
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public ushort Months { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } = "Pending";

    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int TermInterestRateId { get; set; }
    public TermInterestRate TermInterestRate { get; set; } = null!;
}
