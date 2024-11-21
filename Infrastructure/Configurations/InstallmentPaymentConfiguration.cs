using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class InstallmentPaymentConfiguration : IEntityTypeConfiguration<InstallmentPayment>
{
    public void Configure(EntityTypeBuilder<InstallmentPayment> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.PaymentAmount)
              .IsRequired();

        entity.Property(x => x.PaymentAmount)
              .IsRequired();

        entity.Property(x => x.Status)
              .IsRequired();

        entity.HasOne(x => x.Installment)
              .WithOne(x => x.InstallmentPayment)
              .HasForeignKey<InstallmentPayment>(x => x.InstallmentId);
    }
}
