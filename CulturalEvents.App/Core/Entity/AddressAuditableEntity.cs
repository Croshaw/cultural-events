namespace CulturalEvents.App.Core.Entity;

public class AddressAuditableEntity : BaseAuditableEntity
{
    public required int StreetId { get; set; }
    public required int House { get; set; }
    public string? Addition { get; set; }
}