using AwesomeTomatoes.BLL.Models.Commons;

namespace AwesomeTomatoes.BLL.Models;
public class MovieBlob : BaseEntity
{
    public string Description { get; set; }

    public string BlobUrl { get; set; }

    public int MovieId { get; set; }

    public Movie Movie { get; set; }
}
