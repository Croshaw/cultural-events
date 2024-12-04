namespace CulturalEvents.App.Core.Entity;

public class CulturalAddressAuditableEntity : BaseAuditableEntity
{
    public required int CulturalId { get; set; }
    public required int AddressId { get; set; }
}