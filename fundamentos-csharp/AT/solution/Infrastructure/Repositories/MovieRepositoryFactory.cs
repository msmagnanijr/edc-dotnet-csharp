
namespace Infrastructure.Repositories
{
    public class MovieRepositoryFactory
    {
        public static IMovieRepository Create(RepositoryType repositoryType) {
            return repositoryType switch
            {
                RepositoryType.List => new TypeList.MoviesRepository(),
                RepositoryType.LinkedList => new TypeLinkedList.MoviesRepository(),
                RepositoryType.File => new TypeFile.MoviesRepository(),
                RepositoryType.Database => new TypeDatabase.MoviesRepository(),
                _ => throw new ArgumentException("Type not supported!"),
            };
        }
    }
}
