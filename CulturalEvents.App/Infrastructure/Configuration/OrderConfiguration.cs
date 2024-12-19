using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<OrderAuditableEntity>
{
    public void Configure(EntityTypeBuilder<OrderAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.AddressId).IsRequired();
        builder.HasOne(e => e.Address).WithMany().HasForeignKey(e => e.AddressId).IsRequired();
        builder.Navigation(e => e.Address).AutoInclude();
        builder.Property(e => e.EventId).IsRequired();
        builder.HasOne(e => e.Event).WithMany().HasForeignKey(e => e.EventId).IsRequired();
        builder.Navigation(e => e.Event).AutoInclude();
    }
}