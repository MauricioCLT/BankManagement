using Core.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AplicationDbContext : DbContext
{
    public DbSet<ApprovedLoan> ApprovedLoans { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Installment> Installments { get; set; }
    public DbSet<InstallmentPayment> InstallmentPayments { get; set; }
    public DbSet<LoanRequest> LoanRequests { get; set; }
    public DbSet<TermInterestRate> TermInterestRates { get; set; }

    public AplicationDbContext() {}

    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApprovedLoanConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new InstallmentConfiguration());
        modelBuilder.ApplyConfiguration(new InstallmentPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new LoanRequestConfiguration());
        modelBuilder.ApplyConfiguration(new TermInterestRateConfiguration());
    }
}
