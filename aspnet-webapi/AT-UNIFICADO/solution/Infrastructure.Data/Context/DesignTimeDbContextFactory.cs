using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Context;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EFContext>
{
    public EFContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile(@Directory.GetCurrentDirectory()
           + "/../WebApi/appsettings.json")
           .Build();

        var builder = new DbContextOptionsBuilder<EFContext>();

        var connectionString = configuration.GetConnectionString("AzureContext");
        builder.UseSqlServer(connectionString);

        return new EFContext(builder.Options);
    }
}
