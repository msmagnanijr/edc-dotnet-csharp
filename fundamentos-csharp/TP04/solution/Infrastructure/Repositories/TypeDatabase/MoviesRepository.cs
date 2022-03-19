using Domain.Models;
using Domain.Utils;

namespace Infrastructure.Repositories.TypeDatabase

{
    public sealed class MoviesRepository : IMovieRepository
    {
        private static readonly List<Movies> movies = new();
        public List<Movies> GetAll()
        {
            return movies;
        }
        public Movies GetById(Guid id)
        {
            Movies? retorno = null;
            if (!id.Equals(""))
            {
                retorno = movies.FirstOrDefault(p => p.Id.Equals(id));
            }

            if (retorno == null) retorno = new Movies();

            return retorno;
        }
        public List<Movies> GetBySearch(string searchString)
        {
            List<Movies>? retorno = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                retorno = movies.Where(p => p.Name.StartsWith(searchString)).ToList();
            }

            if (retorno == null) retorno = new List<Movies>();

            return retorno;
        }
        public void Create(Movies movie)
        {
            movies.Add(movie);
        }
        public void Update(Movies movie)
        {
            Movies? retorno = null;

            if (!movie.Id.Equals(""))
            {
                retorno = movies.FirstOrDefault(p => p.Id.Equals(movie.Id));

                if (retorno != null)
                {
                    retorno.Name = movie.Name;
                    retorno.FilmStudio = movie.FilmStudio;
                    retorno.ReleaseDate = movie.ReleaseDate;
                }
            }
        }
        public void Delete(Guid id)
        {
            Movies? retorno = null;

            if (!id.Equals("")) retorno = movies.FirstOrDefault(p => p.Id.Equals(id));
            if (retorno != null) movies.Remove(retorno);
        }
    }
}
