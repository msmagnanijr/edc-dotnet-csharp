using AwesomeTomatoes.BLL.Models.Commons;

namespace AwesomeTomatoes.BLL.Models;
public class Movie : BaseEntity
{
    public string Name { get; set; }
    public string FilmStudio { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal BoxOffice { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
