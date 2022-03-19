using Infrastructure.Repositories;
using Infrastructure.Resources;

namespace Infrastructure.Commands
{
    public class ExitCommand : ICommand
    {   
        public string Description => Language.exitMovieLabel;
        public void Execute(RepositoryType database) { Environment.Exit(0); }
    }
}
