using System.Net.Http.Headers;

namespace WebMvc;

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

    public async Task<HttpResponseMessage> PutAsync(HttpContext context, string endpoint, StringContent data)
    {
        var client = GetHttpClient(context);
        var response = await client.PutAsync(endpoint, data);

        return response;
    }

    public async Task<HttpResponseMessage> PostAsyncMultipartFormDataContent(HttpContext context, string endpoint, MultipartFormDataContent data)
    {
        var client = GetHttpClient(context);
        var response = await client.PostAsync(endpoint, data);

        return response;
    }


    public async Task<HttpResponseMessage> PutAsyncMultipartFormDataContent(HttpContext context, string endpoint, MultipartFormDataContent data)
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
