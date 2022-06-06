namespace AwesomeTomatoes.WEB.Models;

public class FileInputModel
{
    public string Description { get; set; }
    public IFormFile FileToUpload { get; set; }
    public Movie Movie { get; set; }
    public int MovieId { get; set; }

}
