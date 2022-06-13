using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities.Commons;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
