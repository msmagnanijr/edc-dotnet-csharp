using Infrastructure.Commands;
using Infrastructure.Utils;
using Infrastructure.Resources;
using System.Globalization;
using Console = Colorful.Console;
using System.Drawing;
using Infrastructure.Repositories;

namespace Presentation
{
    class Manager
    {
        static RepositoryType database = RepositoryType.List;
        static void Main(string[] args)
        {
            Utilities.CenterText("Bem vindo <3 Welcome - Awesome Tomatoes v0.0.1\n");
            Console.WriteAscii("Awesome Tomatoes", Color.MediumAquamarine);

            SetupLanguage();

            SetupDatabase();

            Menu();
        }

        protected static void Menu() {

            var commands = new ICommand[]
            {
                new AddReviewCommand(),
                new AddMovieCommand(),
                new FindAllMovieCommand(),
                new FindMovieCommand(),
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
            var isValidInMemoryDatabase = false;
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
}

