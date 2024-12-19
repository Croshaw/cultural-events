using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("addresses"), Display(Name ="Адреса")]

public class AddressAuditableEntity : BaseAuditableEntity
{
    [Column("street_id")]
    public required int StreetId { get; set; }
    [Display(Name = "Улица"), Editable(true)]
    [ForeignKey(nameof(StreetId))]
    public StreetAuditableEntity Street { get; set; }
    [Column("house")]
    [Display(Name = "Дом"), Editable(true)]
    public required int House { get; set; }
    [Column("addition")]
    [Display(Name = "Строение"), Editable(true)]
    public string? Addition { get; set; }
}