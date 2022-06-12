using Domain.Model.Entities;

namespace Domain.Model.Interfaces.Repositories;

public interface ICommentRepository
{
    Task InsertAsync(CommentEntity commentEntity);
    Task<IEnumerable<CommentEntity>> GetAllAsync();
    Task UpdateAsync(CommentEntity commentEntity);
    Task<CommentEntity> GetByIdAsync(int id);
    Task<IEnumerable<CommentEntity>> GetAllByReviewId(int id);
}
