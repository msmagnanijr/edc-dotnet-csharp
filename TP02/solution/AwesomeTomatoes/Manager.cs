using AwesomeTomatoes.Commands;

namespace AwesomeTomatoes
{
    // TODO TP03: Configurar a cultura de maneira global
    class Manager
    {
        static void Main(string[] args)
        {
            var commands = new ICommand[]
            {
                new AddMovieCommand(),
                new FindMovieCommand(),
                new DeleteMovieCommand(),
                new ExitCommand()
            };

            Utilities.CenterText("Bem vindo ao Awesome Tomatoes");

            while (true)
            {

                Console.WriteLine("\nO que você deseja fazer?\n");

                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine("[{0}] - {1}", i + 1, commands[i].Description);
                }
                Console.Write("\n>> ");
                string? userChoice;
                int commandIndex;
                do
                {
                    userChoice = Console.ReadLine();
                }
                while (!int.TryParse(userChoice, out commandIndex) || commandIndex > commands.Length);

                commands[commandIndex - 1].Execute();
            }
        }
    }
}

