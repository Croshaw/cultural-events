namespace CulturalEvents.App.Core.Entity;

public class EventTypeEntity : BaseEntityAuditableEntity
{
    public required int CulturalTypeId { get; set; }
    public required string Name { get; set; }
}