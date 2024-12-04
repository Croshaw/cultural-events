namespace CulturalEvents.Core.Entity;

public class EventPriceEntity : BaseEntityAuditableEntity
{
    public required int EventId { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
    public required decimal Price { get; set; }
}