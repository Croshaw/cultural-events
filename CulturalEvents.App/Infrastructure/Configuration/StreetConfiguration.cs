using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class StreetConfiguration : IEntityTypeConfiguration<StreetAuditableEntity>
{
    public void Configure(EntityTypeBuilder<StreetAuditableEntity> builder)
    {
        builder.HasKey(street => street.Id);
        builder.Property(street => street.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasOne<CityAuditableEntity>().WithMany().HasForeignKey(street => street.CityId).IsRequired();
        builder.Property(street => street.Name).IsRequired();
    }
}