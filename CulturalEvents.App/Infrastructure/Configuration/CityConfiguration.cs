using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class CityConfiguration : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.HasKey(city => city.Id);
        builder.Property(city => city.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasOne<RegionEntity>().WithMany().HasForeignKey(city => city.RegionId).IsRequired();
        builder.Property(city => city.Name).IsRequired();
    }
}