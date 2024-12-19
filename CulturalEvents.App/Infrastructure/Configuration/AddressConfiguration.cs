using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<AddressAuditableEntity>
{
    public void Configure(EntityTypeBuilder<AddressAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasOne(e => e.Street).WithMany().HasForeignKey(e => e.StreetId).IsRequired();
        builder.Property(e => e.House).IsRequired();
        builder.Navigation(e => e.Street).AutoInclude();
    }
}