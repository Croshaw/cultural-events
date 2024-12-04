using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class CityConfiguration : IEntityTypeConfiguration<CityAuditableEntity>
{
    public void Configure(EntityTypeBuilder<CityAuditableEntity> builder)
    {
        builder.HasKey(city => city.Id);
        builder.Property(city => city.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasOne<RegionAuditableEntity>().WithMany().HasForeignKey(city => city.RegionId).IsRequired();
        builder.Property(city => city.Name).IsRequired();
    }
}