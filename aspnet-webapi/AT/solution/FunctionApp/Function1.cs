using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace FunctionApp;

public static class Function1
{
    [FunctionName("Function1")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        int movieId = data?.movieId;

        var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

        using (SqlConnection conn = new(connectionString))
        {
            conn.Open();
            var textSql = $@"UPDATE [dbo].[Movies] SET [LastView] = GETDATE() WHERE [Id] = {movieId};";

            using SqlCommand cmd = new(textSql, conn);
            var rowsAffected = cmd.ExecuteNonQuery();
            log.LogInformation($"rowsAffected: {rowsAffected}");
        }

        return new OkResult();
    }
}