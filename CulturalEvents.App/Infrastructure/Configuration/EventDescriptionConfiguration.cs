using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class EventDescriptionConfiguration : IEntityTypeConfiguration<EventDescriptionAuditableEntity>
{
    public void Configure(EntityTypeBuilder<EventDescriptionAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.Description).IsRequired();
        builder.HasOne(e => e.Event).WithMany().HasForeignKey(e => e.EventId).IsRequired();
        builder.Navigation(e => e.Event).AutoInclude();
    }
}