using Domain.Model.Interfaces.Infrastructure;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Services.Services;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Infrastructure.Services.Blob;
using Infrastructure.Services.Functions;
using Infrastructure.Services.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;

//Microsoft.Extensions.Configuration.Binder --> https://stackoverflow.com/questions/54767718/iconfiguration-does-not-contain-a-definition-for-getvalue

public class DependencyInjectorHelper
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EFContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AzureContext")));

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieService, MovieService>();

        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewService, ReviewService>();

        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommentService, CommentService>();

        var connStringStorageAccount = configuration.GetValue<string>("AzureConnectionString");

        services.AddScoped<IBlobService, BlobService>(provider =>
            new BlobService(connStringStorageAccount));

        services.AddScoped<IQueueService, QueueService>(provider =>
            new QueueService(connStringStorageAccount));

        services.AddScoped<IFunctionService, FunctionService>(provider =>
            new FunctionService(configuration.GetValue<string>("FunctionBaseAddress")));

    }
}