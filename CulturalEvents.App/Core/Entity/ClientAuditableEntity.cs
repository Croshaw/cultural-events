namespace CulturalEvents.App.Core.Entity;

public class ClientAuditableEntity : BaseAuditableEntity
{
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public required string Patronymic { get; set; }
    public required string Phone { get; set; }
}