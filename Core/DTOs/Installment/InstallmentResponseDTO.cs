namespace Core.DTOs.Installment;

public class InstallmentResponseDTO
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
