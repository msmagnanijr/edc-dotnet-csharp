using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;

namespace Domain.Services.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;


    public CommentService(
        ICommentRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task InsertAsync(CommentEntity commentEntity)
    {
        await _repository.InsertAsync(commentEntity);
    }

    public async Task UpdateAsync(CommentEntity commentEntity)
    {
        await _repository.UpdateAsync(commentEntity);
    }

    public async Task<CommentEntity> GetByIdAsync(int id)
    {
        var CommentTask = _repository.GetByIdAsync(id);

        var CommentEntity = CommentTask.Result;

        return CommentEntity;
    }

    public async Task<IEnumerable<CommentEntity>> GetAllByReviewId(int id)
    {
        var CommentEntity = await _repository.GetAllByReviewId(id);

        return CommentEntity;
    }
}