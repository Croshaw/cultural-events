namespace CulturalEvents.App.Core.Entity;

public class OrderEntity : BaseEntityAuditableEntity
{
    public required int AddressId { get; set; }
    public required int EventId { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly Time { get; set; }
    public required bool IsDelivery { get; set; }
    public required OrderStatus Status { get; set; }
}