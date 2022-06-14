using System;
using Domain.Model.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace FunctionApp;

public static class Function2
{
    [FunctionName("Function2")]
    public static void Run([QueueTrigger("update-last-view", Connection = "AzureWebJobsStorage")] MovieEntity movie, ILogger log)
    {
        log.LogInformation($"C# Queue trigger function processed.");

        var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

        using SqlConnection conn = new(connectionString);
        conn.Open();
        var textSql = $@"UPDATE [dbo].[Movies] SET [LastViewQueue] = GETDATE() WHERE [Id] = {movie.Id};";

        using SqlCommand cmd = new(textSql, conn);
        var rowsAffected = cmd.ExecuteNonQuery();
        log.LogInformation($"rowsAffected: {rowsAffected}");

    }
}