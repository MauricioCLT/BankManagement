public class RequestLoan
{
    public int CustomerId { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public ushort Months { get; set; }
    public decimal AmountRequest { get; set; }
    public string Status { get; set; } = string.Empty;
}
