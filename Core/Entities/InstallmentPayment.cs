namespace Core.Entities;

public class InstallmentPayment
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal PaymentAmount { get; set; }
    public string Status { get; set; } = null!;

    public List<Installment> Installments { get; set; } = null!;
}
