using Domain.Model.Entities;

namespace Domain.Model.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<ReviewEntity>> GetAllAsync();
    Task<ReviewEntity> GetByIdAsync(int id);
    Task InsertAsync(ReviewEntity reviewEntity);
    Task UpdateAsync(ReviewEntity reviewEntity);
    Task DeleteAsync(ReviewEntity reviewEntity);
}
