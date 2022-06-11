using Domain.Model.Entities;
using Domain.Model.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly EFContext _context;

    public MovieRepository(EFContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        return await _context.Movies
                .AsNoTracking()
                .OrderByDescending(m => m.ReleaseDate).ToListAsync();
    }

    public async Task<MovieEntity> GetByIdAsync(int id)
    {
        return await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task InsertAsync(MovieEntity movieEntity)
    {
        _context.Add(movieEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MovieEntity movieEntity)
    {
        _context.Update(movieEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MovieEntity movieEntity)
    {
        _context.Remove(movieEntity);
        await _context.SaveChangesAsync();
    }
}