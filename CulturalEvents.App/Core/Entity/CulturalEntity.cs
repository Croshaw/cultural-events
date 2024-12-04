namespace CulturalEvents.App.Core.Entity;

public class CulturalEntity : BaseEntityAuditableEntity
{
    public required int CulturalTypeId { get; set; }
    public required string Name { get; set; }
}