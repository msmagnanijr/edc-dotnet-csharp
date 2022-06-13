using Domain.Model.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities;

public class CommentEntity : BaseEntity
{
    [Display(Name = "Comentário")]
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public int ReviewId { get; set; }
    public int Upvote { get; set; }
}
