namespace AwesomeTomatoes.Models
{
    public class Movies
    {

        public Movies(string name, string filmStudio, DateTime releaseDate)
        {
            Name = name;
            FilmStudio = filmStudio;
            ReleaseDate = releaseDate;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FilmStudio { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
