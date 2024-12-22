using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("event_types"), Display(Name = "Виды мероприятий")]
public class EventTypeAuditableEntity : BaseAuditableEntity
{
    [Column("cultural_type_id")]
    public required int CulturalTypeId { get; set; }
    
    [Display(Name = "Вид культурного заведения"), Editable(true)]
    [ForeignKey(nameof(CulturalTypeId))]
    public CulturalTypeAuditableEntity CulturalType { get; set; }
    
    [Column("name")]
    [Display(Name = "Название"), Editable(true)]
    public required string Name { get; set; }

    public override string ToString()
    {
        return $"{CulturalType} - {Name}";
    }
}