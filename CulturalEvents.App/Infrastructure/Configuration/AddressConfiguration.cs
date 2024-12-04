using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.House).IsRequired();
        builder.HasOne<StreetEntity>().WithMany().HasForeignKey(c => c.StreetId).IsRequired();
    }
}