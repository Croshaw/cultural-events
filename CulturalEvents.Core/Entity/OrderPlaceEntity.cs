namespace CulturalEvents.Core.Entity;

public class OrderPlaceEntity : BaseEntityAuditableEntity
{
    public required int OrderId { get; set; }
    public required int Place { get; set; }
}