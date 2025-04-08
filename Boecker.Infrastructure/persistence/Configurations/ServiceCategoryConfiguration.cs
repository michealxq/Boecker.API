using Boecker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Boecker.Infrastructure.persistence.Configurations;

public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.HasKey(sc => sc.ServiceCategoryId);
        builder.Property(sc => sc.Name).IsRequired().HasMaxLength(100);
    }
}
