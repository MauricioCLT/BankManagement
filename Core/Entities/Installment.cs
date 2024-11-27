namespace Core.Entities;

public class Installment
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public int? InstallmentPaymentId { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public int ApprovedLoanId { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public InstallmentPayment? InstallmentPayment { get; set; } = null!;
}
