using CulturalEvents.App.Core.Entity;
using CulturalEvents.App.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvents.App.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RegionEntity> Regions { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<StreetEntity> Streets { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RegionConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new StreetConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
    }
}
