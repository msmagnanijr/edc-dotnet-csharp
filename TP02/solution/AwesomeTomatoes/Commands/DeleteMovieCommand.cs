using AwesomeTomatoes.Models;
using Microsoft.EntityFrameworkCore;
using AwesomeTomatoes.Repositories;


namespace AwesomeTomatoes.Commands
{
    class DeleteMovieCommand : ICommand
    {
        public string Description => "Remover um Filme";
        public void Execute()
        {
            DeleteMovie();
        }

        // TODO TP03: Refatorar para melhorar o desacoplamento
        // TODO TP03: Injetar EFContext ?
        public static void DeleteMovie()
        {
            try
            {
                Console.WriteLine("\nDigite o nome, ou parte do nome do filme que deseja remover:");
                Console.Write("\n>> ");
                string name = Console.ReadLine();

                using var db = new EFContext();

                // TODO TP03: Atualizar para utilização de "Predicados" no Repository.
                // TODO TP03: Like parece não funcionar para nomes de filme composto.
                var query = from e in db.Movies
                            where EF.Functions.Like(e.Name, name + "%")
                            select e;

                List<Movies> movies = query.ToList();
                if (movies.Count > 0)
                {
                    foreach (Movies p in movies)
                    {
                        Console.WriteLine(" Selecione o Id ** {0} ** para que o filme ** {1} ** seja removido!", p.Id, p.Name);

                    }
                    Console.Write("\n>> ");

                    var key = int.Parse(Console.ReadLine());
                    var movieRepository = new Repository<Movies>(db);
                    Movies movie = movieRepository.GetById(key);

                    if (movie.Id != null)
                    {
                        movieRepository.Delete(movie);
                        db.SaveChanges();
                        Console.WriteLine($"\nO filme {movie.Name} foi removido com sucesso!");
                    }
                    else Console.WriteLine("O filme não foi removido! Retornando ao menu principal."); return;

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
