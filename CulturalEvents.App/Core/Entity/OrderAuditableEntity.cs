using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("orders"), Display(Name = "Заказы")]
public class OrderAuditableEntity : BaseAuditableEntity
{
    [Column("address_id")]
    public required int AddressId { get; set; }
    [Display(Name="Адрес"), Editable(true)]
    [ForeignKey(nameof(AddressId))]
    public AddressAuditableEntity Address { get; set; }
    [Column("event_id")]
    public required int EventId { get; set; }
    [Display(Name="Дата"), Editable(true)]
    [ForeignKey(nameof(EventId))]
    public EventAuditableEntity Event { get; set; }
    [Column("date")]
    [Display(Name="Дата"), Editable(true)]
    public required DateOnly Date { get; set; }
    [Column("time")]
    [Display(Name="Время"), Editable(true)]
    public required TimeOnly Time { get; set; }
    [Column("is_delivery")]
    [Display(Name="Доставка"), Editable(true)]
    public required bool IsDelivery { get; set; }
    [Column("status")]
    [Display(Name="Статус"), Editable(true)]
    public required OrderStatus Status { get; set; }
}