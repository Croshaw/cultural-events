namespace CulturalEvents.App.Core.Entity;

public class StreetAuditableEntity : BaseAuditableEntity
{
    public required int CityId { get; set; }
    public required string Name { get; set; }
}