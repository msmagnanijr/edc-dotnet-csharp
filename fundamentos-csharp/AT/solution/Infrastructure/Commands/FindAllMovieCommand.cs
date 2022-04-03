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
        public void Execute(RepositoryType database)
        {
            FindMovie(database);
        }
        public static void FindMovie(RepositoryType database)
        {
            try
            {
                var operations = MovieRepositoryFactory.Create(database);
                var movies = operations.GetAll();
               
                if (movies.Count > 0)
                {
                    Console.WriteLine("\n--- {0} ---\n", Language.movieListLabel, Color.Aquamarine);
                    foreach (Movies movie in movies)
                    {
                        Console.WriteLine($"\n{Language.nameLabel} {movie.Name}", Color.LightSeaGreen);
                        Console.WriteLine($"{Language.studioLabel} {movie.FilmStudio}", Color.LightSeaGreen);                 
                    }
                }
                else Console.WriteLine(Language.moviesNotFound, Color.DarkOliveGreen);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", Language.errorMessage, ex.Message);
            }
        }
    }
}
