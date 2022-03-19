namespace Domain.Models;

public class Movies
{
    public Movies() { 
    }
    public Movies(string name, string filmStudio, DateOnly releaseDate, double boxOffice)
    {
        Name = name;
        FilmStudio = filmStudio;
        ReleaseDate = releaseDate;
        BoxOffice = boxOffice;

    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string FilmStudio { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public double BoxOffice { get; set; }
    public override string ToString()
    {
        return String.Format("{0};{1};{2};{3};{4}\n", this.Id, this.Name, this.FilmStudio, this.ReleaseDate, this.BoxOffice);
    }
}
