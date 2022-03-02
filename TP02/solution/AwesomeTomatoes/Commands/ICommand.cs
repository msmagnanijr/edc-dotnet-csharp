namespace AwesomeTomatoes.Commands
{
    interface ICommand
    {
        string Description { get; }
        void Execute();
    }
}
