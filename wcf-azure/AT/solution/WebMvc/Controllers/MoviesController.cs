using Domain.Model.Entities;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WebMvc.Controllers;

public class MoviesController : Controller
{
    private readonly ApiHttpClient _apiHttpClient;
    private readonly IConfiguration _configuration;
    public MoviesController(ApiHttpClient apiHttpClient, IConfiguration configuration)
    {
        _apiHttpClient = apiHttpClient;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            List<MovieEntity> movies = new();

            var response = await _apiHttpClient.GetAsync(HttpContext, "/api/Movies/getmovies");

            if (response.IsSuccessStatusCode)
            {
                var movieResponse = response.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<MovieEntity>>(movieResponse);
            }

            return View(movies);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public async Task<MovieEntity> GetMovie(int id)
    {
        var movie = new MovieEntity();
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Movies/getmovie/{id}");
        if (response.IsSuccessStatusCode)
        {
            var movieResponse = response.Content.ReadAsStringAsync().Result;
            movie = JsonConvert.DeserializeObject<MovieEntity>(movieResponse);
        }
        return movie;
    }

    public async Task<IActionResult> Details(int id)
    {
        var movie = await GetMovie(id);
        if (movie == null)
        {
            return NotFound();
        }

        //TODO: Mover para a Camada de Serviço
        var movieJson = JsonConvert.SerializeObject(new { movieId = id });
        var data = new StringContent(movieJson, Encoding.UTF8, "application/json");
        var baseAddressFunction = _configuration.GetValue<string>("FunctionBaseAddress");
        _ = await _apiHttpClient.PostAsync(HttpContext, baseAddressFunction, data);

        return View(movie);
    }

    public IActionResult Create()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
            return View();

        return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] MovieEntity movie)
    {

        var file = Request.Form.Files.SingleOrDefault();
        await using var stream = file.OpenReadStream();

        Console.WriteLine(file.Length.ToString());

        var multipartFormContent = new MultipartFormDataContent();

        //TODO: Refatorar para utilizar um custom model binder
        multipartFormContent.Add(new StringContent(movie.Name), name: "Name");
        multipartFormContent.Add(new StringContent(movie.FilmStudio), name: "FilmStudio");
        multipartFormContent.Add(new StringContent(movie.ReleaseDate.ToString()), name: "ReleaseDate");
        multipartFormContent.Add(new StringContent(movie.BoxOffice.ToString()), name: "BoxOffice");
        multipartFormContent.Add(new StringContent(movie.VideoUrl), name: "VideoUrl");

        var fileStreamContent = new StreamContent(stream);
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        multipartFormContent.Add(fileStreamContent, name: "file", fileName: "movie.png");

        var response = await _apiHttpClient.PostAsyncMultipartFormDataContent(HttpContext, "/api/Movies/createmovie", multipartFormContent);

        string responseContent = await response.Content.ReadAsStringAsync();

        return RedirectToAction("Index", "Movies");

    }

    public async Task<IActionResult> Edit(int id)
    {
        var movie = await GetMovie(id);
        Console.WriteLine("Passando no edit");
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditMovie([FromForm] MovieEntity movie)
    {
        var file = Request.Form.Files.SingleOrDefault();
        await using var stream = file.OpenReadStream();

        var multipartFormContent = new MultipartFormDataContent();

        Console.WriteLine($"File name Controller: {file.Name}");
        Console.WriteLine(file.Length.ToString());

        //TODO: Refatorar para utilizar um custom model binder
        multipartFormContent.Add(new StringContent(movie.Id.ToString()), name: "Id");
        multipartFormContent.Add(new StringContent(movie.Name), name: "Name");
        multipartFormContent.Add(new StringContent(movie.FilmStudio), name: "FilmStudio");
        multipartFormContent.Add(new StringContent(movie.ReleaseDate.ToString()), name: "ReleaseDate");
        multipartFormContent.Add(new StringContent(movie.BoxOffice.ToString()), name: "BoxOffice");
        multipartFormContent.Add(new StringContent(movie.VideoUrl), name: "VideoUrl");

        var fileStreamContent = new StreamContent(stream);
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        multipartFormContent.Add(fileStreamContent, name: "file", fileName: "movie.png");

        var response = await _apiHttpClient.PutAsyncMultipartFormDataContent(HttpContext, "/api/Movies/updatemovie", multipartFormContent);

        string responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseContent);

        return RedirectToAction("Index", "Movies");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var movie = await GetMovie(id);
        if (movie == null)
        {
            return NotFound();
        }
  
        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
     
        var response = await _apiHttpClient.DeleteAsync(HttpContext, $"/api/Movies/removemovie/{id}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
        }

        return RedirectToAction(nameof(Index));
    }

    private bool MovieEntityExists(int id)
    {
        return GetMovie(id) != null;
    }
}