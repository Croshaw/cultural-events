namespace CulturalEvents.App.Core.Entity;

public class StreetEntity : BaseEntityAuditableEntity
{
    public required int CityId { get; set; }
    public required string Name { get; set; }
}