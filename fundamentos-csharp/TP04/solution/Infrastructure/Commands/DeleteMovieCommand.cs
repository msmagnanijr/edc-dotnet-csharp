using Domain.Models;
using Infrastructure.Repositories;
using Console = Colorful.Console;
using System.Drawing;
using Infrastructure.Resources;

namespace Infrastructure.Commands
{
    public class DeleteMovieCommand : ICommand
    {
        public string Description => Language.deleteMovieLabel;
        public void Execute(RepositoryType database)
        {
            DeleteMovie(database);
        }

        public static void DeleteMovie(RepositoryType database)
        {
            try
            {
                Console.WriteLine("\n{0}", Language.movieToRemove);
                Console.Write("\n>> ");
                string name = Console.ReadLine();

                var operations = MovieRepositoryFactory.Create(database);
                var movies = operations.GetBySearch(name);
                if (movies.Count > 0)
                {
                    Console.WriteLine("\n{0}\n", Language.chooseMovieRemove);
                    IDictionary<int, Guid> keyValuePairs = new Dictionary<int, Guid>();
                    int count = 0;
                    foreach (Movies p in movies)
                    {
                        count++;
                        Console.WriteLine("{0} | {2} | {1} | {3} |", Language.selectIdMovie, Language.removeMovieMessage, count, p.Name, Color.Aquamarine);
                        keyValuePairs.Add(count, p.Id);
                    }
                    Console.Write("\n>> ");
                    int choice = int.Parse(Console.ReadLine());
                    Guid key = keyValuePairs[choice];
                    var movie = operations.GetById(key);

                    Console.WriteLine("\n 1 >> {0}", Language.yesLabel, Color.Aquamarine);
                    Console.WriteLine(" 2 >> {0}", Language.noLabel);
                    Console.Write("\n>> ");
                    var resposta = Console.ReadLine();
                    if (resposta == "1")
                    {
                        operations.Delete(key);
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
