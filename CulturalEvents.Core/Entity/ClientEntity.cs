namespace CulturalEvents.Core.Entity;

public class ClientEntity : BaseEntityAuditableEntity
{
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public required string Patronymic { get; set; }
    public required string Phone { get; set; }
}