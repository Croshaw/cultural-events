using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class ClientAddressConfiguration : IEntityTypeConfiguration<ClientAddressAuditableEntity>
{
    public void Configure(EntityTypeBuilder<ClientAddressAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.ClientId).IsRequired();
        builder.Property(e => e.AddressId).IsRequired();
        builder.HasOne(e => e.Client).WithMany().HasForeignKey(e => e.ClientId).IsRequired();
        builder.Navigation(e => e.Client).AutoInclude();
        builder.HasOne(e => e.Address).WithMany().HasForeignKey(e => e.AddressId).IsRequired();
        builder.Navigation(e => e.Address).AutoInclude();
    }
}