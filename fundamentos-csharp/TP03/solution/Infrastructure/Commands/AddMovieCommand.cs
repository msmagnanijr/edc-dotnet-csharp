using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Resources;
using System.Globalization;
using Console = Colorful.Console;
using System.Drawing;

namespace Infrastructure.Commands
{
    public class AddMovieCommand : ICommand
    {
        public string Description => Language.addMovieLabel;
        public void Execute()
        {
            AddMovie();
        }

        private static void AddMovie()
        {
            try
            {
               GatheringInformation();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", Language.errorMessage, ex.Message, Color.Red);
            }
        }

        private static void GatheringInformation() 
        {
            Console.Write("\n{0}",Language.addMovieName);
            string? name = Console.ReadLine();

            Console.Write(Language.addStudioName);
            string? filmStudio = Console.ReadLine();

            Console.Write(Language.addReleaseDate);
            var releaseDate = DateTime.Parse(Console.ReadLine());

            Console.Write(Language.addBoxOffice);
            var boxOffice = double.Parse(Console.ReadLine());
          
            var movieData = new Movies(name, filmStudio, releaseDate, boxOffice);

            Console.WriteLine("\n{0}\n",Language.askForConfirmation, Color.Aquamarine);
            Console.WriteLine($"{Language.nameLabel} {name}");
            Console.WriteLine($"{Language.studioLabel} {filmStudio}");
            Console.WriteLine($"{Language.releaseDateLabel} {releaseDate}");
            Console.WriteLine($"{Language.boxOfficeLabel} {boxOffice}");
            Console.WriteLine("\n 1 >> {0}", Language.yesLabel, Color.Aquamarine);
            Console.WriteLine(" 2 >> {0}", Language.noLabel);
            Console.Write("\n>> ");
            var resposta = Console.ReadLine();
            if (resposta == "1")
            {
                ExecuteTransaction(movieData);
            }
            else
            {
                Console.Write("\n");
                Console.WriteLine(Language.backMainMenu);
                return;
            }
        }
        private static void ExecuteTransaction(Movies movie) {
            MoviesRepository.Create(movie);
            Console.Write("\n");
            Console.WriteLine(Language.addSuccessMessage, Color.DarkOliveGreen);
        }
    }
}
