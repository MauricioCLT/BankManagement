namespace Core.Entities;

public class ApprovedLoan
{
    public int Id { get; set; }
    public decimal RequestedAmount { get; set; }
    public float InterestRate { get; set; }
    public ushort Months { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public string RejectionReason { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? ApprovalDate { get; set; }

    public int LoanRequestId { get; set; }
    public LoanRequest LoanRequest { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<Installment> Installments { get; set; } = [];
}
