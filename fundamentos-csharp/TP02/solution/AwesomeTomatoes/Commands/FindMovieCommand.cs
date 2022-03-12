using AwesomeTomatoes.Models;
using Microsoft.EntityFrameworkCore;
using AwesomeTomatoes.Repositories;

namespace AwesomeTomatoes.Commands
{
    class FindMovieCommand : ICommand
    {
        public string Description => "Pesquisar um Filme";
        public void Execute()
        {
            FindMovie();
        }

        // TODO TP03: Refatorar para melhorar o desacoplamento
        // TODO TP03: Injetar EFContext ?
        public static void FindMovie()
        {
            try
            {
                Console.WriteLine("\nDigite o nome, ou parte do nome do filme que deseja encontrar:");
                Console.Write("\n>> ");
                string name = Console.ReadLine();

                using var db = new EFContext();

                // TODO TP03: Atualizar para utilização de "Predicados" no Repository.
                // TODO TP03: Like parece não funciona para nomes de filme composto.
                var query = from e in db.Movies
                            where EF.Functions.Like(e.Name, name + "%")
                            select e;

                List<Movies> movies = query.ToList();
                if (movies.Count > 0)
                {
                    Console.WriteLine("\nSelecione uma das opções abaixo para visualizar os dados do filme desejado:\n");
                    foreach (Movies p in movies)
                    {
                        Console.WriteLine(" Selecione o Id ** {0} ** para receber mais informações sobre o filme ** {1} **", p.Id, p.Name);

                    }
                    Console.Write("\n>> ");
                    var key = int.Parse(Console.ReadLine());
                    var movieRepository = new Repository<Movies>(db);
                    Movies movie = movieRepository.GetById(key);

                    if (movie.Id != null)
                    {
                        Console.WriteLine("\n--- Dados do Filme ---\n");
                        Console.WriteLine($"Nome: {movie.Name}");
                        Console.WriteLine($"Estúdio responsável: {movie.FilmStudio}");
                        // TODO TP03: Alterar o ToString para ToShortDateString ( depende da configuração da Cultura )
                        Console.WriteLine($"Data de lançamento: {movie.ReleaseDate.ToString("dd/MM/yyyy")}");
                        Console.WriteLine($"Esse filme foi lançado a {Utilities.DaysFromReleaseDate(movie.ReleaseDate):0.} dias atrás ou {Utilities.YearsFromReleaseDate(movie.ReleaseDate):0.#} anos.");
                    }
                    return;
                }
                else Console.WriteLine("Nenhum resultado encontrado! Retornando ao menu principal."); return;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Um erro inesperado ocorreu: {0}", ex.Message);
            }
        }
    }
}
