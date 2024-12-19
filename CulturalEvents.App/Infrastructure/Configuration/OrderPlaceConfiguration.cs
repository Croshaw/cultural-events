using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class OrderPlaceConfiguration : IEntityTypeConfiguration<OrderPlaceAuditableEntity>
{
    public void Configure(EntityTypeBuilder<OrderPlaceAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.Place).IsRequired();
        builder.Property(e => e.OrderId).IsRequired();
        builder.HasOne(e => e.Order).WithMany().HasForeignKey(e => e.OrderId).IsRequired();
        builder.Navigation(e => e.Order).AutoInclude();
    }
}