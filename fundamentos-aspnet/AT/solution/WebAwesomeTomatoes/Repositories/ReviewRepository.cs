using WebAwesomeTomatoes.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAwesomeTomatoes.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly EFContext _context;

        public ReviewRepository(EFContext context) => _context = context;
        public async Task<Review> GetById(int id) 
        {
            return await _context.Set<Review>()
                         .Include(m => m.Movie)
                         .FirstOrDefaultAsync(m => m.Id == id);
        } 
        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews.Include(r => r.Movie)
                    .AsNoTracking()
                    .OrderByDescending(r => r.ReviewDate).ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByFilter(string filter)
        {
            return await _context.Reviews
                            .Include(
                            r => r.Movie)
                            .Where(Movie => Movie.Movie.Name.StartsWith(filter) || Movie.Movie.Name.Contains(filter))
                            .AsNoTracking()
                            .OrderByDescending(Movie => Movie.ReviewDate).ToListAsync();
        }
        public async Task Create(Review review) => await _context.Set<Review>().AddAsync(review);
        public async Task Update(Review review) => _context.Set<Review>().Update(review);
        public async Task Delete(Review review) => _context.Set<Review>().Remove(review);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public bool EntityExists(int id) => _context.Set<Review>().Any(e => e.Id == id);

    }
}
