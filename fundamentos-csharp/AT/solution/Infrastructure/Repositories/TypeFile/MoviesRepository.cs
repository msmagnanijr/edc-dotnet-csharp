using Domain.Models;
using Infrastructure.Utils;

namespace Infrastructure.Repositories.TypeFile

{
    public sealed class MoviesRepository : IMovieRepository
    {
        private static readonly List<Movies> movies = new();
        public List<Movies> GetAll()
        {
            movies.Clear();
            List<String> movieData = Utilities.ReadTextFromFile("movies.txt");
            foreach (var m in movieData) {
                 Movies movie = new();
                 movie.Id = Guid.Parse(m.Split(";")[0]);
                 movie.Name = m.Split(";")[1];
                 movie.FilmStudio = m.Split(";")[2];
                 movie.ReleaseDate = DateTime.Parse(m.Split(";")[3]);
                 movie.BoxOffice = double.Parse(m.Split(";")[4]);
                 movies.Add(movie);
            }
            return movies;
        }
        public Movies GetById(Guid id)
        {
            Movies? retorno = null;
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                List<String> movieData = Utilities.ReadTextFromFile("movies.txt");
                foreach (var m in movieData)
                {
                    if (m.Contains(id.ToString())) 
                    {
                        Movies movie = new();
                        movie.Id = Guid.Parse(m.Split(";")[0]);
                        movie.Name = m.Split(";")[1];
                        movie.FilmStudio = m.Split(";")[2];
                        movie.ReleaseDate = DateTime.Parse(m.Split(";")[3]);
                        movie.BoxOffice = double.Parse(m.Split(";")[4]);
                        retorno = movie;
                    }
                }
            }
            if (retorno == null) retorno = new Movies();
            return retorno;
        }
        public List<Movies> GetBySearch(string searchString)
        {
            GetAll();
            List<Movies>? retorno = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                retorno = movies.Where(p => p.Name.StartsWith(searchString) 
                                    || p.Name.Contains(searchString)).ToList();
            }
            if (retorno == null) retorno = new List<Movies>();
            return retorno;
        }
        public void Create(Movies movie) => Utilities.SaveToTextFile(movie.ToString(), "movies.txt");
        public void Update(Movies movie)
        {
            Utilities.RemoveLineFromFile("movies.txt", movie.Id.ToString());
            Utilities.SaveToTextFile(movie.ToString(), "movies.txt");
        }
        public void Delete(Movies movie) => Utilities.RemoveLineFromFile("movies.txt", movie.Id.ToString());  
    }
}
