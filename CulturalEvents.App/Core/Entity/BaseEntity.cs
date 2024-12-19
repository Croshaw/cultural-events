using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

public class BaseEntity
{
    [Column("id")]
    public required int Id { get; set; }
}