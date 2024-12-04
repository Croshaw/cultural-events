namespace CulturalEvents.App.Core.Entity;

public class EventPriceAuditableEntity : BaseAuditableEntity
{
    public required int EventId { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
    public required decimal Price { get; set; }
}