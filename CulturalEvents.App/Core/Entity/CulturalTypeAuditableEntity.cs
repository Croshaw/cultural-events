using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("cultural_types"), Display(Name ="Виды культурных заведений")]
public class CulturalTypeAuditableEntity : BaseAuditableEntity
{
    [Column("name")]
    [Display(Name="Название"), Editable(true)]
    public required string Name { get; set; }
}