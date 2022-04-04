using WebAwesomeTomatoes.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAwesomeTomatoes.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly EFContext _context;

        public MovieRepository(EFContext context) => _context = context;
        public async Task<Movie> GetById(int id) => await _context.Set<Movie>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies
                    .AsNoTracking()
                    .OrderByDescending(m => m.ReleaseDate).ToListAsync();
        }
        public async Task<IEnumerable<Movie>> GetByFilter(string filter)
        {
            return await _context.Movies
                      .Where(m => m.Name.StartsWith(filter) || m.Name.Contains(filter))
                      .AsNoTracking()
                      .OrderByDescending(m => m.ReleaseDate).ToListAsync();
        }
        public async Task Create(Movie movie) => await _context.Set<Movie>().AddAsync(movie);
        public async Task Update(Movie movie) => _context.Set<Movie>().Update(movie);
        public async Task Delete(Movie movie) => _context.Set<Movie>().Remove(movie);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public bool EntityExists(int id) => _context.Set<Movie>().Any(e => e.Id == id);

        public DbSet<Movie> Movies() => _context.Set<Movie>();

    }
}
