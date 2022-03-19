using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IMovieRepository
    {
        public List<Movies> GetAll();
        public  Movies GetById(Guid id);
        public  List<Movies> GetBySearch(string searchString);
        public  void Create(Movies movie);
        public  void Update(Movies movie);
        public  void Delete(Guid id);
    }
}
