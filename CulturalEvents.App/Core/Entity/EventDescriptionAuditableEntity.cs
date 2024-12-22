using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("event_description"), Display(Name = "Описание мероприятий")]
public class EventDescriptionAuditableEntity : BaseAuditableEntity
{
    [Column("event_id")]
    public required int EventId { get; set; }
    [Display(Name = "Мероприятие"), Editable(true)]
    [ForeignKey(nameof(EventId))]
    public EventAuditableEntity Event { get; set; }
    [Display(Name = "Описание"), Editable(true)]
    [Column("description")]
    public required string Description { get; set; }

    public override string ToString()
    {
        return Description;
    }
}