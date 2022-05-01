using AwesomeTomatoes.BLL.Models.Commons;
using AwesomeTomatoes.BLL.Models.Enums;

namespace AwesomeTomatoes.BLL.Models;
public class Review : BaseEntity
{
    public string TextReview { get; set; }
    public ReviwerSatisfaction ReviwerSatisfaction { get; set; }
    public DateTime ReviewDate { get; set; } = DateTime.Now;
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}

  

