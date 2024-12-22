using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;
[Table("prices"), Display(Name = "Price Auditable")]
public class EventPriceAuditableEntity : BaseAuditableEntity
{
    [Column("event_id")]
    public required int EventId { get; set; }
    [Display(Name = "Мероприятие"), Editable(true)]
    [ForeignKey(nameof(EventId))]
    public EventAuditableEntity Event  { get; set; }
    [Column("from")]
    [Display(Name = "От"), Editable(true)]
    public int? From { get; set; }
    [Column("to")]
    [Display(Name = "До"), Editable(true)]
    public int? To { get; set; }
    [Column("price")]
    [Display(Name = "Цена"), Editable(true)]
    public required decimal Price { get; set; }

    public override string ToString()
    {
        return $"{From} {To} {Price}";
    }
}