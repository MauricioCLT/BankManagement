namespace Core.DTOs.SimulateLoan;

public class LoanSimulateResponse
{
    public float InterestRate { get; set; }
    public decimal MonthyPayment { get; set; }
    public decimal TotalPayment { get; set; }
}
