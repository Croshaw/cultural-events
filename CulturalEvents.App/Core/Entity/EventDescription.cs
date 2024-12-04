namespace CulturalEvents.App.Core.Entity;

public class EventDescription : BaseEntityAuditableEntity
{
    public required int EventId { get; set; }
    public required string Description { get; set; }
}