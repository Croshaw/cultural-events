using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class CulturalTypeConfiguration : IEntityTypeConfiguration<CulturalTypeAuditableEntity>
{
    public void Configure(EntityTypeBuilder<CulturalTypeAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
    }
}