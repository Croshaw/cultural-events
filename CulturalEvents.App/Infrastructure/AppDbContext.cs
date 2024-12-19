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
    
    public DbSet<CulturalTypeAuditableEntity> CulturalTypes { get; set; }
    public DbSet<CulturalAuditableEntity> Culturals { get; set; }
    public DbSet<CulturalAddressAuditableEntity> CulturalAddresses { get; set; }
    
    public DbSet<EventTypeAuditableEntity> EventTypes { get; set; }
    public DbSet<EventAuditableEntity> Events { get; set; }
    public DbSet<EventPriceAuditableEntity> EventPrices { get; set; }
    public DbSet<EventDescriptionAuditableEntity> EventDescriptions { get; set; }
    
    public DbSet<ClientAuditableEntity> Clients { get; set; }
    public DbSet<ClientAddressAuditableEntity> ClientAddresses { get; set; }
    public DbSet<OrderAuditableEntity> Orders { get; set; }
    public DbSet<OrderPlaceAuditableEntity> OrderPlaces { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RegionConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new StreetConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CulturalTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CulturalConfiguration());
        modelBuilder.ApplyConfiguration(new CulturalAddressConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new ClientAddressConfiguration());
        
        modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new EventPriceConfiguration());
        modelBuilder.ApplyConfiguration(new EventDescriptionConfiguration());
        
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderPlaceConfiguration());
    }
}
