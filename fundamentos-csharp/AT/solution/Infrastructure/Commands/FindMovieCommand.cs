using Infrastructure.Repositories;
using Domain.Models;
using Infrastructure.Utils;
using Infrastructure.Resources;
using Console = Colorful.Console;
using System.Drawing;

namespace Infrastructure.Commands
{
    public class FindMovieCommand : ICommand
    {
        public string Description => Language.findMovieDetailsLabel;
        public void Execute(RepositoryType database)
        {
            FindMovie(database);
        }
        public static void FindMovie(RepositoryType database)
        {
            try
            {
                Console.WriteLine("\n{0}", Language.movieToFind);
                Console.Write("\n>> ");
                string name = Console.ReadLine();
                var operations = MovieRepositoryFactory.Create(database);
                var movies = operations.GetBySearch(name);
                if (movies.Count > 0)
                {
                    Console.WriteLine("\n{0}\n", Language.chooseMovie);
                    IDictionary<int,Guid> keyValuePairs = new Dictionary<int,Guid>();
                    int count = 0;
                    foreach (Movies p in movies)
                    {
                        count++;
                        Console.WriteLine("{0} | {2} | {1} | {3} |", Language.selectIdMovie, Language.moreMovieInfo, count, p.Name, Color.Aquamarine);
                        keyValuePairs.Add(count, p.Id);
                    }
                    Console.Write("\n>> ");
                    int choice =  int.Parse(Console.ReadLine());
                    Guid key = keyValuePairs[choice];
                    var movie = operations.GetById(key);

                    if (movie.Id != null)
                    {
                        Console.WriteLine("\n--- {0} ---\n", Language.movieDetails);
                        Console.WriteLine($"{Language.textMovieDetailsFilmeStudio} {movie.FilmStudio} {Language.textMovieDetailsReponsable} {movie.Name}");
                        Console.WriteLine($"{movie.Name} {Language.textMovieDetailsRelease} {movie.ReleaseDate}");
                        Console.WriteLine($"{Language.textMovieDetailsBoxOffice} {movie.BoxOffice}");
                        Console.WriteLine($"{Language.textMovieDetailsReleaseCalc} {Utilities.DaysFromReleaseDate(movie.ReleaseDate):0.} {Language.textMovieDetailsReleaseDays} " +
                            $"{Utilities.YearsFromReleaseDate(movie.ReleaseDate):0.#} {Language.textMovieDetailsReleaseYears}.");
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", Language.errorMessage, ex.Message);
            }
        }
    }
}
