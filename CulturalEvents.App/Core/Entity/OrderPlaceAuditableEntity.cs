using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("places"), Display(Name = "Купленные места")]
public class OrderPlaceAuditableEntity : BaseAuditableEntity
{
    [Column("order_id")]
    public required int OrderId { get; set; }
    [Display(Name = "Заказ"), Editable(true)]
    [ForeignKey(nameof(OrderId))]
    public OrderAuditableEntity Order { get; set; }
    [Display(Name = "Место"), Editable(true)]
    [Column("place")]
    public required int Place { get; set; }
}