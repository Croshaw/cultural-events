using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<AddressAuditableEntity>
{
    public void Configure(EntityTypeBuilder<AddressAuditableEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.House).IsRequired();
        builder.HasOne<StreetAuditableEntity>().WithMany().HasForeignKey(c => c.StreetId).IsRequired();
    }
}