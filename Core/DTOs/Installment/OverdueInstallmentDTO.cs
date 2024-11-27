using Core.DTOs.Customer;

namespace Core.DTOs.Installment;

public class OverdueInstallmentDTO
{
    public DetailedCustomerDTO Customer { get; set; } = null!;
    public string DueDate { get; set; } = string.Empty;
    public string DaysLate { get; set; } = string.Empty;
    public decimal AmountPending { get; set; }
}
