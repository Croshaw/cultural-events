namespace CulturalEvents.App.Core.Entity;

public class EventAuditableEntity: BaseAuditableEntity
{
    public required int EventTypeId { get; set; }
    public required int CulturalId{ get; set; }
    public required string Name { get; set; }
    public string? ShortDescription { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly Time { get; set; }
    public required int Places { get; set; }
}