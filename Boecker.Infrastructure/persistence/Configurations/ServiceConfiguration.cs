using Boecker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(s => s.ServiceId);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(150);
        builder.HasOne(s => s.ServiceCategory)
               .WithMany(sc => sc.Services)
               .HasForeignKey(s => s.ServiceCategoryId);
    }
}
