using Infrastructure.Repositories;

namespace Infrastructure.Commands
{
    public interface ICommand
    {
        string Description { get; }
        void Execute(RepositoryType database);
    }
}
