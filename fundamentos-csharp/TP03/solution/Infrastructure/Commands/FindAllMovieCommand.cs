using Infrastructure.Repositories;
using Domain.Models;
using Infrastructure.Resources;
using Console = Colorful.Console;
using System.Drawing;

namespace Infrastructure.Commands
{
    public class FindAllMovieCommand : ICommand
    {
        public string Description => Language.findAllMovieLabel;
        public void Execute()
        {
            FindMovie();
        }

        public static void FindMovie()
        {
            try
            {
              var movies =  MoviesRepository.GetAll().ToList();
              Console.WriteLine("\n--- {0} ---\n", Language.movieListLabel, Color.Aquamarine);                
              foreach(Movies movie in movies) {
                Console.WriteLine($"\n{Language.nameLabel} {movie.Name}");
                Console.WriteLine($"{Language.studioLabel} {movie.FilmStudio}");
               }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", Language.errorMessage, ex.Message);
            }
        }
    }
}
