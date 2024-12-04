using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class StreetConfiguration : IEntityTypeConfiguration<StreetEntity>
{
    public void Configure(EntityTypeBuilder<StreetEntity> builder)
    {
        builder.HasKey(street => street.Id);
        builder.Property(street => street.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasOne<CityEntity>().WithMany().HasForeignKey(street => street.CityId).IsRequired();
        builder.Property(street => street.Name).IsRequired();
    }
}