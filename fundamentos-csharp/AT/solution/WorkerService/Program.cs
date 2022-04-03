using Infrastructure.Repositories;
using Infrastructure.Repositories.TypeFile;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IMovieRepository, MoviesRepository> ();
                    services.AddHostedService<Worker>();
                });
    }
}
