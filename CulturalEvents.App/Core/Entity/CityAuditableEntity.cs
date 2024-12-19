using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("cities"), Display(Name ="Города")]
public class CityAuditableEntity : BaseAuditableEntity
{
    [Column("region_id")]
    public required int RegionId { get; set; }
    
    [Column("name")]
    [Display(Name = "Название"), Editable(true)]
    public required string Name { get; set; }
    
    [Display(Name = "Регион"), Editable(true)]
    [ForeignKey(nameof(RegionId))]
    public RegionAuditableEntity Region { get; set; }
    
    public override string ToString()
    {
        return Region.ToString() + ", " + "г. " + Name;
    }
}