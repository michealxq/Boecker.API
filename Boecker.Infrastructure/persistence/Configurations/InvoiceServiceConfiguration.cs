using Boecker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.persistence.Configurations;

public class InvoiceServiceConfiguration : IEntityTypeConfiguration<InvoiceService>
{
    public void Configure(EntityTypeBuilder<InvoiceService> builder)
    {
        builder.HasKey(isv => isv.InvoiceServiceId);
        builder.HasOne(isv => isv.Invoice)
               .WithMany(i => i.InvoiceServices)
               .HasForeignKey(isv => isv.InvoiceId);
        builder.HasOne(isv => isv.Service)
               .WithMany(s => s.InvoiceServices)
               .HasForeignKey(isv => isv.ServiceId);
    }
}
