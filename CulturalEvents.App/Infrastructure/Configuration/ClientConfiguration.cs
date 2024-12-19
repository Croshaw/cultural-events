using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<ClientAuditableEntity>
{
    public void Configure(EntityTypeBuilder<ClientAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Surname).IsRequired();
        builder.Property(e => e.Patronymic).IsRequired();
        builder.Property(e => e.Phone).IsRequired();
    }
}