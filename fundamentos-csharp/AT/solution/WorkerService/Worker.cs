using Infrastructure.Commands;
using Infrastructure.Utils;
using Infrastructure.Resources;
using System.Globalization;
using Console = Colorful.Console;
using System.Drawing;
using Infrastructure.Repositories;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IMovieRepository _movieRepository;
        static RepositoryType database = RepositoryType.File;

        public Worker(IMovieRepository movieRepository)
        { 
            _movieRepository = movieRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            WelcomeLoadData();
            SetupLanguage();
            SetupDatabase();
            Menu();
        }
        protected void WelcomeLoadData()
        {
            Utilities.CenterText("Bem vindo <3 Welcome - Awesome Tomatoes v0.0.1\n");
            Console.WriteAscii("Awesome Tomatoes", Color.MediumAquamarine);

            var movies = _movieRepository.GetAll();

            if (movies.Count > 0)
            {
                /// O arquivo texto está configurado para ser carregado/salvo no caminho (Environment.SpecialFolder.Personal) movies.txt
                Console.WriteLine("Listando os filmes que já estavam cadastrados no arquivo movies.txt\n", Color.Tomato);
                movies.Reverse();
                var index = 0;
                foreach (var movie in movies)
                {
                    if (index <= 4)
                    {
                        Console.WriteLine($"Filme: {movie.Name} | Estúdio Reponsável: {movie.FilmStudio} |" +
                         $" Data de Lançamento: {movie.ReleaseDate} | Bilheteria: {movie.BoxOffice}", Color.White);
                    }
                    index++;
                }
            }
            else
            {
                Console.WriteLine("Não existem dados prévios a serem exibidos!\n", Color.Tomato);
            }
        }
        protected static void Menu()
        {
            var commands = new ICommand[]
            {
                new AddMovieCommand(),
                new FindAllMovieCommand(),
                new FindMovieCommand(),
                new UpdateMovieCommand(),
                new DeleteMovieCommand(),
                new ExitCommand()
            };

            while (true)
            {
                Console.WriteLine("\n {0} \n", Language.whatToDo, Color.Aquamarine);

                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine("[{0}] - {1}", i + 1, commands[i].Description, Color.Aquamarine);
                }
                Console.Write("\n>> ");
                string? userChoice;
                int commandIndex;
                do
                {
                    userChoice = Console.ReadLine();
                }
                while (!int.TryParse(userChoice, out commandIndex) || commandIndex > commands.Length);
                commands[commandIndex - 1].Execute(database);
            }
        }
        protected static void SetupLanguage()
        {
            var isValidChoice = false;
            while (!isValidChoice)
            {
                Console.WriteLine("\n{0}\n", Language.languageChoiceMessage, Color.Aquamarine);
                Console.Write("\n>> ");
                var languageChoice = Console.ReadLine();


                if (languageChoice.Equals("PT", StringComparison.OrdinalIgnoreCase) || languageChoice.Equals("EN", StringComparison.OrdinalIgnoreCase))
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(languageChoice);
                    Language.Culture = CultureInfo.GetCultureInfo(languageChoice);
                    Console.WriteLine("\n{0}\n", Language.languageChoice, Color.DarkOliveGreen);
                    isValidChoice = true;
                }
                else
                {
                    Console.WriteLine("\n{0} {1} \n", languageChoice, Language.languageNotSupported, Color.Orange);
                }
            }
        }
        protected static void SetupDatabase()
        {
            try
            {
                Console.WriteLine(" \n{0} \n", Language.messageFile, Color.DarkOliveGreen);
                Console.WriteLine("{0}",Language.askDatabase, Color.Aquamarine);
                Console.Write("\n>> ");
                var takeChoice = Console.ReadLine();
                if (takeChoice.Equals("1"))
                {

                    bool isValidInMemoryDatabase = false;
                    while (!isValidInMemoryDatabase)
                    {
                        Console.WriteLine("\n{0}\n", Language.databaseChoice, Color.Aquamarine);
                        Console.Write("\n>> ");
                        var databaseChoice = int.Parse(Console.ReadLine());
                        if (databaseChoice == 1)
                        {
                            database = RepositoryType.List;
                            Console.WriteLine(" \n{0}\n", Language.messageList, Color.DarkOliveGreen);
                            isValidInMemoryDatabase = true;
                        }
                        else if (databaseChoice == 2)
                        {
                            database = RepositoryType.LinkedList;
                            Console.WriteLine(" \n{0} \n", Language.messageLinkedList, Color.DarkOliveGreen);
                            isValidInMemoryDatabase = true;
                        }
                        else if (databaseChoice == 3)
                        {
                            database = RepositoryType.File;
                            Console.WriteLine(" \n{0} \n", Language.messageFile, Color.DarkOliveGreen);
                            isValidInMemoryDatabase = true;
                        }
                        else if (databaseChoice == 4)
                        {
                            database = RepositoryType.Database;
                            Console.WriteLine(" \n{0} \n", Language.messageDatabase, Color.DarkOliveGreen);
                            isValidInMemoryDatabase = true;
                        }
                        else
                        {
                            Console.WriteLine("\n{0} {1}\n", databaseChoice, Language.databaseNotSupported, Color.Orange);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", Language.errorMessage, ex.Message, Color.Red);
            }
        }
    }
}