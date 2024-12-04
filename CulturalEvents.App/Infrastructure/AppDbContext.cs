using CulturalEvents.App.Core.Entity;
using CulturalEvents.App.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvents.App.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RegionAuditableEntity> Regions { get; set; }
    public DbSet<CityAuditableEntity> Cities { get; set; }
    public DbSet<StreetAuditableEntity> Streets { get; set; }
    public DbSet<AddressAuditableEntity> Addresses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RegionConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new StreetConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
    }
}
