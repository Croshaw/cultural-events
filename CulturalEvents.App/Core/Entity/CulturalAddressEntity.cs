namespace CulturalEvents.App.Core.Entity;

public class CulturalAddressEntity : BaseEntityAuditableEntity
{
    public required int CulturalId { get; set; }
    public required int AddressId { get; set; }
}