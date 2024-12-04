namespace CulturalEvents.Core.Entity;

public class CityEntity : BaseEntityAuditableEntity
{
    public required int RegionId { get; set; }
    public required string Name { get; set; }
}