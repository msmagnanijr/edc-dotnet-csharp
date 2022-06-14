using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly EFContext _context;

    public ReviewRepository(EFContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReviewEntity>> GetAllAsync()
    {
        return await _context.Reviews.Include(r => r.Movie)
               .AsNoTracking()
               .OrderByDescending(r => r.ReviewDate).ToListAsync();
    }

    public async Task<ReviewEntity> GetByIdAsync(int id)
    {
        return await _context.Set<ReviewEntity>()
                      .Include(m => m.Movie)
                      .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task InsertAsync(ReviewEntity reviewEntity)
    {
        _context.Add(reviewEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ReviewEntity reviewEntity)
    {
        _context.Update(reviewEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ReviewEntity reviewEntity)
    {
        _context.Remove(reviewEntity);
        await _context.SaveChangesAsync();
    }
}