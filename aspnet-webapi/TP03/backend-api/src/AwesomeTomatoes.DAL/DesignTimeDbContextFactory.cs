using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WebAwesomeTomatoes.Models;

namespace AwesomeTomatoes.DAL;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EFContext>
{
    public EFContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile(@Directory.GetCurrentDirectory()
           + "/../AwesomeTomatoes.API/appsettings.json")
           .Build();

        var builder = new DbContextOptionsBuilder<EFContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseSqlServer(connectionString);

        return new EFContext(builder.Options);
    }
}
