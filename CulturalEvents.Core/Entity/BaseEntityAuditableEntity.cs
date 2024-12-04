namespace CulturalEvents.Core.Entity;

public class BaseEntityAuditableEntity : BaseEntity
{
    public required DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public required DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}