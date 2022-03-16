namespace Domain.Models;

public class Movies
{

    public Movies() { 
    }
    public Movies(string name, string filmStudio, DateTime releaseDate, double boxOffice)
    {
        Name = name;
        FilmStudio = filmStudio;
        ReleaseDate = releaseDate;
        BoxOffice = boxOffice;

    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string FilmStudio { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double BoxOffice { get; set; }
}
