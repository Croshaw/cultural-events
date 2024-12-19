using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CulturalEvents.App.Infrastructure.Configuration;

public class CulturalConfiguration : IEntityTypeConfiguration<CulturalAuditableEntity>
{
    public void Configure(EntityTypeBuilder<CulturalAuditableEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.HasOne(e => e.CulturalType).WithMany().HasForeignKey(e => e.CulturalTypeId).IsRequired();
        builder.Navigation(e => e.CulturalType).AutoInclude();
    }
}