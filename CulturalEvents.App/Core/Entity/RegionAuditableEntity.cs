using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("regions"), Display(Name ="Регионы")]
public class RegionAuditableEntity : BaseAuditableEntity
{
    [Column("name")]
    [Display(Name = "Название"), Editable(true)]
    public required string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}