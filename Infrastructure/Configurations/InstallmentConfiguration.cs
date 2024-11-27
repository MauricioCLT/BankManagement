using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.TotalAmount)
            .IsRequired();

        entity.Property(x => x.PrincipalAmount)
            .IsRequired();

        entity.Property(x => x.InterestAmount)
            .IsRequired();

        entity.Property(x => x.DueDate)
            .IsRequired();

        entity.Property(x => x.Status)
            .IsRequired();

        entity.HasOne(x => x.ApprovedLoan)
              .WithMany(x => x.Installments)
              .HasForeignKey(x => x.ApprovedLoanId);

        entity.HasOne(x => x.InstallmentPayment)
              .WithMany(x => x.Installments)
              .HasForeignKey(x => x.InstallmentPaymentId);
    }
}
