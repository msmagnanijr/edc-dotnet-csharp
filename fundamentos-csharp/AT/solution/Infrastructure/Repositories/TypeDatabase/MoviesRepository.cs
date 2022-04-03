using Domain.Models;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;

// Needs migrations first!
namespace Infrastructure.Repositories.TypeDatabase

{
    public sealed class MoviesRepository : IMovieRepository
    {
        private static readonly List<Movies> movies = new();
        private readonly EFContext dbContext = new ();
        public List<Movies> GetAll()
        {
            return dbContext.Movies.ToList();
        }
        public Movies GetById(Guid id)
        {
            Movies? retorno = null;
            if (!id.Equals(""))
            {
                retorno = dbContext.Movies.Find(id);
            }

            if (retorno == null) retorno = new Movies();

            return retorno;
        }
        public List<Movies> GetBySearch(string searchString)
        {
            List<Movies>? retorno = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                var query = from e in dbContext.Movies
                            where EF.Functions.Like(e.Name, searchString + "%")
                            select e;

                retorno = query.ToList();
            }

            if (retorno == null) retorno = new List<Movies>();

            return retorno;
        }
        public void Create(Movies movie)
        {
            dbContext.Add(movie);
            dbContext.SaveChanges();
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
                    retorno.BoxOffice = movie.BoxOffice;
                }
            }
        }
        public void Delete(Movies movie)
        {
            dbContext.Movies.Remove(movie);
            dbContext.SaveChanges(); 
        }
    }
}
