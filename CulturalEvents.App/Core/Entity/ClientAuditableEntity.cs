using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalEvents.App.Core.Entity;

[Table("clients"), Display(Name = "Клиенты")]
public class ClientAuditableEntity : BaseAuditableEntity
{
    [Display(Name = "Фамилия"), Column("surname"), Editable(true)]
    public required string Surname { get; set; }
    [Display(Name = "Имя"), Column("name"), Editable(true)]
    public required string Name { get; set; }
    [Display(Name = "Отчество"), Column("patronymic"), Editable(true)]
    public required string Patronymic { get; set; }
    [Display(Name = "Телефон"), Column("phone"), Editable(true)]
    public required string Phone { get; set; }
}