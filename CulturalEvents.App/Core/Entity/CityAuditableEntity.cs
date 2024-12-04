namespace CulturalEvents.App.Core.Entity;

public class CityAuditableEntity : BaseAuditableEntity
{
    public required int RegionId { get; set; }
    public required string Name { get; set; }
}