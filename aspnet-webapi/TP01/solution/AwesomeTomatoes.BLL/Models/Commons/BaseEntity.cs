using System.ComponentModel.DataAnnotations;

namespace AwesomeTomatoes.BLL.Models.Commons;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
