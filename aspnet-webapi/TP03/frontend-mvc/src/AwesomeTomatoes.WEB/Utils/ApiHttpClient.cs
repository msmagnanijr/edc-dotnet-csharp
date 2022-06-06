namespace AwesomeTomatoes.WEB.Utils;

public class ApiHttpClient
{
    public  async Task<HttpResponseMessage> GetAsync(HttpContext context, string endpoint) 
    {
        var client = GetHttpClient(context);
        var response  = await client.GetAsync(endpoint);

        return response;
    }

    public  async Task<HttpResponseMessage> PostAsync(HttpContext context, string endpoint, StringContent data)
    {
        var client = GetHttpClient(context);
        var response = await client.PostAsync(endpoint, data);

        return response;
    }

    public async Task<HttpResponseMessage> PostAsyncMultipartFormDataContent(HttpContext context, string endpoint, string filePath)
    {
        var client = GetHttpClient(context);

        using (var multipartFormContent = new MultipartFormDataContent())
        {
            //Load the file and set the file's Content-Type header
            var fileStreamContent = new StreamContent(File.OpenRead(filePath));
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            //Add the file
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: filePath.GetHashCode().ToString());

            //Send it
            var response = await client.PostAsync(endpoint, multipartFormContent);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }


    public async Task<HttpResponseMessage> PutAsync(HttpContext context, string endpoint, StringContent data)
    {
        var client = GetHttpClient(context);
        var response = await client.PutAsync(endpoint, data);

        return response;
    }

    public  async Task<HttpResponseMessage> DeleteAsync(HttpContext context, string endpoint)
    {
        var client = GetHttpClient(context);
        var response = await client.DeleteAsync(endpoint);

        return response;
    }

    public HttpClient GetHttpClient(HttpContext context)
    {
        var baseUrl = SettingsConfigHelper.AppSetting("Endpoint:URL");
        var jwtToken = context.Session.GetString("token");

        var client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

        return client;
    }

    //TODO: refatorar / remover dessa classe
    public bool IsTokenInSession(HttpContext context) 
    {
        if (!string.IsNullOrEmpty(context.Session.GetString("token")))
            return true;
        return false;
    }
}
