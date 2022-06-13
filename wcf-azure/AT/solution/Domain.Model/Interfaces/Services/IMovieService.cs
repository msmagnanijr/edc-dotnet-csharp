using Domain.Model.Entities;

namespace Domain.Model.Interfaces.Services;

public interface IMovieService
{
    Task<IEnumerable<MovieEntity>> GetAllAsync();
    Task<MovieEntity> GetByIdAsync(int id);
    Task InsertAsync(MovieEntity movieEntity, Stream stream);
    Task UpdateAsync(MovieEntity movieEntity, Stream stream);
    Task DeleteAsync(MovieEntity movieEntity);
}
