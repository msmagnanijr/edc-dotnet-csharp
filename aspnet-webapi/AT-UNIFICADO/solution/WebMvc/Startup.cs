using Infrastructure.Data.Context;
using Infrastructure.IoC;
using Microsoft.EntityFrameworkCore;

namespace WebMvc;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllersWithViews();

        services.AddSingleton<ApiHttpClient>();

        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddCors(policy =>
        {
            policy.AddPolicy("CorsPolicy", opt => opt
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
        });

        services.AddMvc();
        services.AddControllers();
    }
}