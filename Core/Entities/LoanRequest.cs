namespace Core.Entities;

public class LoanRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int TermInterestRateId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;

    public Customer Customer { get; set; } = null!;
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public TermInterestRate TermInterestRate { get; set; } = null!;
}
