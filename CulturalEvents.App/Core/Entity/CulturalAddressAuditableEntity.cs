using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("cultural_addresses"), Display(Name="Адреса культурных заведений")]
public class CulturalAddressAuditableEntity : BaseAuditableEntity
{
    [Column("cultural_id")]
    public required int CulturalId { get; set; }
    
    [Display(Name ="Культурное заведение"), Editable(true)]
    [ForeignKey(nameof(CulturalId))]
    public CulturalAuditableEntity Cultural { get; set; }
    [Column("address_id")]
    public required int AddressId { get; set; }
    [Display(Name ="Адрес"), Editable(true)]
    [ForeignKey(nameof(AddressId))]
    public AddressAuditableEntity Address { get; set; }
}