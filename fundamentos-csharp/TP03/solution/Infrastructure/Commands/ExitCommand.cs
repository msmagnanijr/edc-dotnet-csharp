using Infrastructure.Resources;

namespace Infrastructure.Commands
{
    public class ExitCommand : ICommand
    {   
        public string Description => Language.exitMovieLabel;
        public void Execute() { Environment.Exit(0); }
    }
}
