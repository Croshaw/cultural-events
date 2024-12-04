namespace CulturalEvents.App.Core.Entity;

public class CulturalAuditableEntity : BaseAuditableEntity
{
    public required int CulturalTypeId { get; set; }
    public required string Name { get; set; }
}