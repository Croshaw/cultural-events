using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<EventAuditableEntity>
{
    public void Configure(EntityTypeBuilder<EventAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.HasOne(e => e.EventType).WithMany().HasForeignKey(e => e.EventTypeId).IsRequired();
        builder.Navigation(e => e.EventType).AutoInclude();
        builder.HasOne(e => e.Cultural).WithMany().HasForeignKey(e => e.CulturalId).IsRequired();
        builder.Navigation(e => e.Cultural).AutoInclude();
    }
}