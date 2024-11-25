namespace Core.Entities;

public class Installment
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public DateTime DueDate { get; set; } // Fecha Limite
    public string Status { get; set; } = string.Empty;

    public int ApprovedLoanId { get; set; }
    public ApprovedLoan ApprovedLoan { get; set; } = null!;
    public List<InstallmentPayment> InstallmentPayments { get; set; } = [];
}
