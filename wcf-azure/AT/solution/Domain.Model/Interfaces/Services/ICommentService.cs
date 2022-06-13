using Domain.Model.Entities;

namespace Domain.Model.Interfaces.Services;

public interface ICommentService
{
    Task<IEnumerable<CommentEntity>> GetAllAsync();
    Task InsertAsync(CommentEntity commentEntity);
    Task UpdateAsync(CommentEntity commentEntity);
    Task<CommentEntity> GetByIdAsync(int id);
    Task<IEnumerable<CommentEntity>> GetAllByReviewId(int id);
}
