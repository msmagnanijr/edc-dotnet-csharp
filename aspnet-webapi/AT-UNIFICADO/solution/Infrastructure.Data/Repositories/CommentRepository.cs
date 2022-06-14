using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly EFContext _context;

    public CommentRepository(EFContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        return await _context.Comments
                .AsNoTracking()
                .OrderBy(m => m.CreatedAt).ToListAsync();
    }
    public async Task InsertAsync(CommentEntity commentEntity)
    {
        _context.Add(commentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CommentEntity commentEntity)
    {
        _context.Update(commentEntity);
        await _context.SaveChangesAsync();
    }


    public async Task<CommentEntity> GetByIdAsync(int id)
    {
        return await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
    }


    public async Task<IEnumerable<CommentEntity>> GetAllByReviewId(int id)
    {

        return await _context.Comments
              .Where(m => m.ReviewId == id)
              .AsNoTracking()
              .OrderBy(m => m.CreatedAt).ToListAsync();
    }


}