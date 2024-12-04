namespace CulturalEvents.App.Core.Entity;

public class ClientAddressAuditableEntity : BaseAuditableEntity
{
    public required int ClientId { get; set; }
    public required int AddressId { get; set; }
    public required int Entrance { get; set; }
    public required int Floor { get; set; }
    public required int Apartment { get; set; }
}