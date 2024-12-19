using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("events"), Display(Name = "Мероприятия")]
public class EventAuditableEntity: BaseAuditableEntity
{
    [Column("type_id")]
    public required int EventTypeId { get; set; }
    [Display(Name = "Вид мероприятия"), Editable(true)]
    [ForeignKey(nameof(EventTypeId))]
    public EventTypeAuditableEntity EventType { get; set; }
    [Column("cultural_id")]
    public required int CulturalId{ get; set; }
    [Display(Name = "Культурное заведение"), Editable(true)]
    [ForeignKey(nameof(CulturalId))]
    public CulturalAuditableEntity Cultural { get; set; }
    [Column("name")]
    [Display(Name = "Название"), Editable(true)]
    public required string Name { get; set; }
    [Column("short_description")]
    [Display(Name = "Короткое описание"), Editable(true)]
    public string? ShortDescription { get; set; }
    [Column("date")]
    [Display(Name = "Дата"), Editable(true)]
    public required DateOnly Date { get; set; }
    [Column("time")]
    [Display(Name = "Время"), Editable(true)]
    public required TimeOnly Time { get; set; }
    [Column("places")]
    [Display(Name = "Кол-во мест"), Editable(true)]
    public required int Places { get; set; }
}