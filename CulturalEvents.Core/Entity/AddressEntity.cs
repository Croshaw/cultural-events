namespace CulturalEvents.Core.Entity;

public class AddressEntity : BaseEntityAuditableEntity
{
    public required int StreetId { get; set; }
    public required int House { get; set; }
    public string? Addition { get; set; }
}