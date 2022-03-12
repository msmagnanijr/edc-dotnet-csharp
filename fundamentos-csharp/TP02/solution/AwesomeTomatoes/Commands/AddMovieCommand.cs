using AwesomeTomatoes.Models;
using AwesomeTomatoes.Repositories;


namespace AwesomeTomatoes.Commands
{
    class AddMovieCommand : ICommand
    {
        public string Description => "Adicionar um novo Filme";
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
                Console.WriteLine("Um erro inesperado ocorreu: {0}", ex.Message);
            }
        }

        private static void GatheringInformation() 
        {
            Console.Write("\nDigite o nome da filme que deseja adicionar: ");
            string? name = Console.ReadLine();

            Console.Write("Digite o nome do estúdio de cinema responsável pela produção do filme: ");
            string? filmStudio = Console.ReadLine();

            Console.Write("Digite a data de lançamento do filme no formato dd/MM/yyyy: ");
            var releaseDate = DateTime.Parse(Console.ReadLine());

            var movieData = new Movies(name, filmStudio, releaseDate);

            Console.WriteLine("\nOs dados abaixo estão corretos? Digite  ** 1 ** para Sim ou  ** 2 ** para Não\n");
            Console.WriteLine($"Nome: {name}");
            Console.WriteLine($"Estúdio reponsável: {filmStudio}");
            Console.WriteLine($"Data de lançamento: {releaseDate}");
            Console.WriteLine("\n 1 >> Sim");
            Console.WriteLine(" 2 >> Não");
            Console.Write("\n>> ");
            var resposta = Console.ReadLine();
            if (resposta == "1")
            {
                ExecuteTransaction(movieData);
            }
            else Console.WriteLine("Retornando ao menu inicial!"); return;
        }
        private static void ExecuteTransaction(Movies movie) {
            using var db = new EFContext();
            var movieRepository = new Repository<Movies>(db);
            movieRepository.Insert(movie);
            db.SaveChanges();
            Console.WriteLine($"\nO cadastro do filme {movie.Name} foi realizado com sucesso!");
        }
    }
}
