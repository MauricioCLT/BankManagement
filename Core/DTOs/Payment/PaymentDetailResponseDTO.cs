namespace Core.DTOs.Payment;

public class PaymentDetailResponseDTO
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty; // Nombre completo
    public DateTime ApprovedDate { get; set; }
    public decimal RequestedAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Revenue { get; set; }
    public ushort Months { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public float InterestRate { get; set; }
    public int CompletePayments { get; set; }
    public int UncompletePayments { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
}