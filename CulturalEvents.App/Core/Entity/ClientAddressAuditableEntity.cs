using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CulturalEvents.App.Core.Entity;

[Table("client_addresses"), Display(Name = "Адреса клиентов")]
public class ClientAddressAuditableEntity : BaseAuditableEntity
{
    [Column("client_id")]
    public required int ClientId { get; set; }
    [Display(Name = "Клиент"), Editable(true)]
    [ForeignKey(nameof(ClientId))]
    public ClientAuditableEntity Client { get; set; }
    [Column("address_id")]
    public required int AddressId { get; set; }
    [Display(Name = "Адрес"), Editable(true)]
    [ForeignKey(nameof(AddressId))]
    public AddressAuditableEntity Address { get; set; }
    [Column("entrance")]
    [Display(Name = "Подъезд"), Editable(true)]
    public required int Entrance { get; set; }
    [Column("floor")]
    [Display(Name = "Этаж"), Editable(true)]
    public required int Floor { get; set; }
    [Column("apartment")]
    [Display(Name = "Квартира"), Editable(true)]
    public required int Apartment { get; set; }

    public override string ToString()
    {
        return $"{Address} под. {Entrance} эт. {Floor} кв. {Apartment}";
    }
}