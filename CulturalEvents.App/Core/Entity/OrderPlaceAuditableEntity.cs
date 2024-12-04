namespace CulturalEvents.App.Core.Entity;

public class OrderPlaceAuditableEntity : BaseAuditableEntity
{
    public required int OrderId { get; set; }
    public required int Place { get; set; }
}