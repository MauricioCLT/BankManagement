namespace Core.Entities;

public class InstallmentPayment
{
    public int Id { get; set; }
    public int InstallmentId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public string Status { get; set; } = null!;

    public Installment Installment { get; set; } = null!;
}
