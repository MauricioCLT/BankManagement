using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.FirstName)
              .IsRequired();

        entity.Property(x => x.LastName)
              .IsRequired();

        entity.HasIndex(x => x.Cid)
              .IsUnique();

        entity.Property(x => x.Email)
              .IsRequired();

        entity.Property(x => x.Phone)
              .IsRequired();

        entity.Property(x => x.Address)
              .IsRequired();

        entity.HasMany(x => x.ApprovedLoans)
              .WithOne(x => x.Customer)
              .HasForeignKey(x => x.CustomerId);

        entity.HasMany(x => x.LoanRequests)
              .WithOne(x => x.Customer)
              .HasForeignKey(x => x.CustomerId);
    }
}
