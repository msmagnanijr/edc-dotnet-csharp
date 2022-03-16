using Domain.Models;

namespace Infrastructure.Repositories

{
    public static class MoviesRepository
    {
        private static readonly List<Movies> movies = new();
        public static List<Movies> GetAll()
        {
            return movies;
        }

        public static Movies GetById(Guid id)
        {
            Movies? retorno = null;
            if (!id.Equals(""))
            {
                retorno = movies.FirstOrDefault(p => p.Id.Equals(id));
            }

            if (retorno == null) retorno = new Movies();

            return retorno;
        }

        public static List<Movies> GetBySearch(string searchString)
        {
            List<Movies>? retorno = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                retorno = movies.Where(p => p.Name.StartsWith(searchString)).ToList();
            }

            if (retorno == null) retorno = new List<Movies>();

            return retorno;
        }

        public static void Create(Movies movie)
        {
            movies.Add(movie);
        }

        public static void Update(Movies movie)
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

        public static void Delete(Guid id)
        {
            Movies? retorno = null;

            if (!id.Equals("")) retorno = movies.FirstOrDefault(p => p.Id.Equals(id));
            if (retorno != null) movies.Remove(retorno);
        }
    }
}
