using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("culturals"), Display(Name ="Культурные заведения")]
public class CulturalAuditableEntity : BaseAuditableEntity
{
    [Column("type_id")]
    public required int CulturalTypeId { get; set; }
    
    [Display(Name = "Тип"), Editable(true)]
    [ForeignKey(nameof(CulturalTypeId))]
    public CulturalTypeAuditableEntity CulturalType { get; set; }
    
    [Column("name")]
    [Display(Name = "Название"), Editable(true)]
    public required string Name { get; set; }

    public override string ToString()
    {
        return $"{CulturalType} {Name}";
    }
}