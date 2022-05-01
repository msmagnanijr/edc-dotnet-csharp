using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAwesomeTomatoes.Models;

namespace AwesomeTomatoes.API;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<EFContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
        {
            Description = "Awesome Tomatoes Implementation Using Web API",
            Title = "Awesome Tomatoes API",
            Version = "v1",
            Contact = new OpenApiContact()
            {
                Name = "Mauricio Magnani",
                Url = new Uri("https://www.linkedin.com/in/mauriciomagnanijr")
            }
        }));
    }
}

