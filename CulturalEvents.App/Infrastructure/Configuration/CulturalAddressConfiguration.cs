using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class CulturalAddressConfiguration : IEntityTypeConfiguration<CulturalAddressAuditableEntity>
{
    public void Configure(EntityTypeBuilder<CulturalAddressAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.CulturalId).IsRequired();
        builder.Property(e => e.AddressId).IsRequired();
        builder.HasOne(e => e.Cultural).WithMany().HasForeignKey(e => e.CulturalId).IsRequired();
        builder.Navigation(e => e.Cultural).AutoInclude();
        builder.HasOne(e => e.Address).WithMany().HasForeignKey(e => e.AddressId).IsRequired();
        builder.Navigation(e => e.Address).AutoInclude();
    }
}