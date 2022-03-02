namespace AwesomeTomatoes.Commands
{
    class ExitCommand : ICommand
    {
        public string Description => "Sair";
        public void Execute() { Environment.Exit(0); }
    }
}
