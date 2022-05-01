using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace WebAwesomeTomatoes.Repositories;

public interface IMovieRepository
{
    Task Create(Movie movie);
    Task Delete(Movie movie);
    Task<IEnumerable<Movie>> GetAll();
    Task<IEnumerable<Movie>> GetByFilter(string filter);
    Task<Movie> GetById(int id);
    Task SaveChangesAsync();
    Task Update(Movie movie);
    bool EntityExists(int id);
    DbSet<Movie> Movies();
}
