using Boecker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(i => i.InvoiceId);
        builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
        builder.HasOne(i => i.Client)
               .WithMany(c => c.Invoices)
               .HasForeignKey(i => i.ClientId);
    }
}
