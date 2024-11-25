using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ApprovedLoanConfiguration : IEntityTypeConfiguration<ApprovedLoan>
{
    public void Configure(EntityTypeBuilder<ApprovedLoan> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.ApprovalDate)
              .IsRequired();

        entity.Property(x => x.RequestedAmount)
              .IsRequired();

        entity.Property(x => x.InterestRate)
              .IsRequired();

        entity.Property(x => x.LoanType)
              .IsRequired();

        entity.HasOne(x => x.LoanRequest)
              .WithOne(x => x.ApprovedLoan)
              .HasForeignKey<ApprovedLoan>(x => x.LoanRequestId);

        entity.HasOne(x => x.Customer)
              .WithMany(x => x.ApprovedLoans)
              .HasForeignKey(x => x.CustomerId);

        entity.HasMany(x => x.Installments)
              .WithOne(x => x.ApprovedLoan)
              .HasForeignKey(x => x.ApprovedLoanId);
    }
}
