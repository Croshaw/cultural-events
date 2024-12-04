using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class RegionConfiguration : IEntityTypeConfiguration<RegionEntity>
{
    public void Configure(EntityTypeBuilder<RegionEntity> builder)
    {
        builder.HasKey(region => region.Id);
        builder.Property(region => region.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(region => region.Name).IsRequired();
    }
}