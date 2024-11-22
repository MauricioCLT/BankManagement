using Core.DTOs.SimulateLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SimulateLoanRepository : ISimulateLoanRepository
{
    private readonly AplicationDbContext _context;

    public SimulateLoanRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LoanSimulateResponse> SimulateLoan(decimal amount, ushort months)
    {
        var termInterestRate = await _context.TermInterestRates.FirstOrDefaultAsync(x => x.Months == months);

        if (termInterestRate == null)
            throw new Exception("No se encontró una tasa de interés para el plazo especifico");

        float monthyRate = (termInterestRate.Interest / 12 / 100);
        decimal monthyPayment = CalculateMonthyPayment(amount, monthyRate, months);

        return new LoanSimulateResponse
        {
            InterestRate = termInterestRate.Interest,
            MonthyPayment = monthyPayment,
            TotalPayment = monthyPayment * months
        };
    }

    private decimal CalculateMonthyPayment(decimal principal, float monthyRate, ushort months)
    {
        if (monthyRate == 0)
            return principal / months;

        double numerator = (double)principal * monthyRate;
        double denominator = 1 - Math.Pow(1 + monthyRate, -months);

        return (decimal)(numerator / denominator);
    }
}
