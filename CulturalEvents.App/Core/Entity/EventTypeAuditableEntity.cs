namespace CulturalEvents.App.Core.Entity;

public class EventTypeAuditableEntity : BaseAuditableEntity
{
    public required int CulturalTypeId { get; set; }
    public required string Name { get; set; }
}