using Domain.Model.Entities;

namespace Domain.Model.Interfaces.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<MovieEntity>> GetAllAsync();
    Task<MovieEntity> GetByIdAsync(int id);
    Task InsertAsync(MovieEntity movieEntity);
    Task UpdateAsync(MovieEntity movieEntity);
    Task DeleteAsync(MovieEntity movieEntity);

}
