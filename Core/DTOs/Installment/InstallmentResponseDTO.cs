namespace Core.DTOs.Installment;

public class InstallmentResponseDTO
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string DueDate { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
