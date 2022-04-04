using WebAwesomeTomatoes.Models;

namespace WebAwesomeTomatoes.Repositories
{
    public interface IReviewRepository
    {
        Task Create(Review review);
        Task Delete(Review review);
        Task<IEnumerable<Review>> GetAll();
        Task<IEnumerable<Review>> GetByFilter(string filter);
        Task<Review> GetById(int id);
        Task SaveChangesAsync();
        Task Update(Review review);
        bool EntityExists(int id);
    }
}