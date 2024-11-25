using Core.Entities;

namespace Core.DTOs.RequestLoan;

public class RequestLoanDTO
{
    public int CustomerId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public ushort Months { get; set; }
    public decimal AmountRequest { get; set; }
}
