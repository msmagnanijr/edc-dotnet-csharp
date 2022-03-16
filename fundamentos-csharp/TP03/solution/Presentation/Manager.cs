using Infrastructure.Commands;
using Infrastructure.Utils;
using Infrastructure.Resources;
using System.Globalization;
using Console = Colorful.Console;
using System.Drawing;

namespace Presentation
{
    class Manager
    {
        static void Main(string[] args)
        {
            var commands = new ICommand[]
            {
                new AddMovieCommand(),
                new FindAllMovieCommand(),
                new FindMovieCommand(),
                new ExitCommand()
            };

            Utilities.CenterText("Bem vindo ao Awesome Tomatoes v0.0.1\n");

            Console.WriteAscii("Awesome Tomatoes", Color.MediumAquamarine);
           
            var validChoice = false;
            while (!validChoice)
            {
                Console.WriteLine("\nPor favor configure o idioma da aplicação. Escolha | PT | para Português ou | EN | para Inglês.\n", Color.Aquamarine);
                Console.Write("\n>> ");
                var languageChoice = Console.ReadLine();


                if (languageChoice.Equals("PT", StringComparison.OrdinalIgnoreCase) || languageChoice.Equals("EN", StringComparison.OrdinalIgnoreCase))
                {
                    //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(languageChoice);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(languageChoice);
                    Language.Culture = CultureInfo.GetCultureInfo(languageChoice);
                    Console.WriteLine("\n{0}\n", Language.languageChoice, Color.DarkOliveGreen);
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("\n{0} não é um idioma válido ou suportado! \n", languageChoice, Color.Orange);
                }
            }

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

                commands[commandIndex - 1].Execute();
            }
        }
    }
}

