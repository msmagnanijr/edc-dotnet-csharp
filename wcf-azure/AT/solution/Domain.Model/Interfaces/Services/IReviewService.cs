using Domain.Model.Entities;

namespace Domain.Model.Interfaces.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewEntity>> GetAllAsync();
    Task<ReviewEntity> GetByIdAsync(int id);
    Task InsertAsync(ReviewEntity reviewEntity);
    Task UpdateAsync(ReviewEntity reviewEntity0);
    Task DeleteAsync(ReviewEntity reviewEntity);
}
