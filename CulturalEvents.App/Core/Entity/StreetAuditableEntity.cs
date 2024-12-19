using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("streets"), Display(Name ="Улицы")]
public class StreetAuditableEntity : BaseAuditableEntity
{
    [Column("city_id")]
    public required int CityId { get; set; }
    
    [Column("name")]
    [Display(Name = "Название"), Editable(true)]
    public required string Name { get; set; }
    
    [Display(Name = "Город"), Editable(true)]
    [ForeignKey(nameof(CityId))]
    public CityAuditableEntity City { get; set; }
    
    public override string ToString()
    {
        return City.ToString() + ", " + "ул. " + Name;
    }
}