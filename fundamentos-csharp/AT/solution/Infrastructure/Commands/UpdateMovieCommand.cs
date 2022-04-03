using Domain.Models;
using Infrastructure.Repositories;
using Console = Colorful.Console;
using System.Drawing;
using Infrastructure.Resources;

namespace Infrastructure.Commands
{
    public class UpdateMovieCommand : ICommand
    {
        public string Description => Language.updateMovieLabel;
        public void Execute(RepositoryType database)
        {
            UpdateMovie(database);
        }
        public static void UpdateMovie(RepositoryType database)
        {
            try
            {
                Console.WriteLine("\n{0}", Language.movieToUpdate);
                Console.Write("\n>> ");
                string name = Console.ReadLine();

                var operations = MovieRepositoryFactory.Create(database);
                var movies = operations.GetBySearch(name);
                if (movies.Count > 0)
                {
                    Console.WriteLine("\n{0}\n", Language.chooseMovieUpdate);
                    IDictionary<int, Guid> keyValuePairs = new Dictionary<int, Guid>();
                    int count = 0;
                    foreach (Movies p in movies)
                    {
                        count++;
                        Console.WriteLine("{0} | {2} | {1} | {3} |", Language.selectIdMovie, Language.updateMovieMessage, count, p.Name, Color.Aquamarine);
                        keyValuePairs.Add(count, p.Id);
                    }
                    Console.Write("\n>> ");
                    int choice = int.Parse(Console.ReadLine());
                    Guid key = keyValuePairs[choice];
                    var movie = operations.GetById(key);

                    Console.WriteLine($"\n{Language.nameLabel} {movie.Name}", Color.LightSeaGreen);
                    Console.WriteLine($"{Language.studioLabel} {movie.FilmStudio}", Color.LightSeaGreen);
                    Console.WriteLine($"{Language.releaseDateLabel} {movie.ReleaseDate}", Color.LightSeaGreen);
                    Console.WriteLine($"{Language.boxOfficeLabel} {movie.BoxOffice}", Color.LightSeaGreen);
                    Console.WriteLine("\n");

                    Console.Write($"{Language.movieNameUpdate}" );
                    string? nameUpdate = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nameUpdate)) 
                    {
                        movie.Name = nameUpdate;
                    }

                    Console.Write($"{Language.movieFilmStudioUpdate}");
                    string? filmStudioUpdate = Console.ReadLine();
                    if (!string.IsNullOrEmpty(filmStudioUpdate))
                    {
                        movie.FilmStudio = filmStudioUpdate;
                    }

                    Console.Write($"{Language.movieReleaseDateUpdate}");
                    var releaseDateUpdate = Console.ReadLine();
                    if (!releaseDateUpdate.Equals(""))
                    {
                        movie.ReleaseDate = DateTime.Parse(releaseDateUpdate);
                    }

                    Console.Write($"{Language.movieBoxOfficeUpdate}");
                    var boxOfficeUpdate = Console.ReadLine();
                    if (!boxOfficeUpdate.Equals(""))
                    {
                        movie.BoxOffice = double.Parse(boxOfficeUpdate);
                    }

                    Console.WriteLine("\n{0}\n", Language.askForConfirmation, Color.Aquamarine);
                    Console.WriteLine($"{Language.nameLabel} {movie.Name}");
                    Console.WriteLine($"{Language.studioLabel} {movie.FilmStudio}");
                    Console.WriteLine($"{Language.releaseDateLabel} {movie.ReleaseDate}");
                    Console.WriteLine($"{Language.boxOfficeLabel} {movie.BoxOffice}");
                    Console.WriteLine("\n 1 >> {0}", Language.yesLabel, Color.Aquamarine);
                    Console.WriteLine(" 2 >> {0}", Language.noLabel);
                    Console.Write("\n>> ");
                    var resposta = Console.ReadLine();
                    if (resposta.Equals("1"))
                    {
                        operations.Update(movie);
                        Console.Write("\n");
                        Console.WriteLine(Language.movieUpdated, Color.DarkOliveGreen);
                    }
                    else
                    {
                        Console.Write("\n");
                        Console.WriteLine(Language.backMainMenu);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Um erro inesperado ocorreu: {0}", ex.Message);
            }
        }
    }
}
