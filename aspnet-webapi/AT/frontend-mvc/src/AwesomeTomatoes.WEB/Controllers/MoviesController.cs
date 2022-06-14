namespace AwesomeTomatoes.WEB.Controllers;

public class MoviesController : Controller
{

    private readonly ApiHttpClient _apiHttpClient;
    public MoviesController(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }
    public async Task<ActionResult> Index()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            List<Movie> movies = new();

            var response = await _apiHttpClient.GetAsync(HttpContext, "/api/Movies");

            if (response.IsSuccessStatusCode)
            {
                var movieResponse = response.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(movieResponse);
            }

            foreach(Movie m in movies)
            {
                var url = await GetMovieBlob(m.Id);
                m.MovieBlobUrl = url.Trim('"');
            }

            return View(movies);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public IActionResult Create()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
            return View();

        return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> Create(Movie movie)
    {
        if (ModelState.IsValid)
        {
            var moviesJson = JsonConvert.SerializeObject(movie);
            var data = new StringContent(moviesJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Movies", data);

            string responseContent = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }
    public async Task<IActionResult> Details(int id)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            var movie = GetMovie(id);
            return View(movie.Result);
        }
        else 
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext)) 
        {
            var movie = GetMovie(id);
            return View(movie.Result);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }

    }

    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> Edit(int id, Movie movie)
    {
        if (id != movie.Id) return NotFound();
        if (ModelState.IsValid)
        {
            var moviesJson = JsonConvert.SerializeObject(movie);
            var data = new StringContent(moviesJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PutAsync(HttpContext, $"/api/Movies/{id}", data);
            string responseContent = await response.Content.ReadAsStringAsync();
 
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            var movie = GetMovie(id);
            return View(movie.Result);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _apiHttpClient.DeleteAsync(HttpContext, $"/api/Movies/{id}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<Movie> GetMovie(int id)
    {
        var movie = new Movie();
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Movies/{id}");
        if (response.IsSuccessStatusCode)
        {
            var reviewResponse = response.Content.ReadAsStringAsync().Result;
            movie = JsonConvert.DeserializeObject<Movie>(reviewResponse);
            var url = await GetMovieBlob(movie.Id);
            movie.MovieBlobUrl = url.Trim('"');
        }
        return movie;
    }

    public async Task<string> GetMovieBlob(int id)
    {
        string movieBlobResponse = "";
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Movies/GetMovieBlob/{id}");
        if (response.IsSuccessStatusCode)
        {
            movieBlobResponse = await response.Content.ReadAsStringAsync();

        }
        return movieBlobResponse;
    }
    public async Task<IActionResult> Upload()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            List<Movie> movies = new();

            var response = await _apiHttpClient.GetAsync(HttpContext, "/api/Movies");

            if (response.IsSuccessStatusCode)
            {
                var movieResponse = response.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(movieResponse);
            }

            ViewData["MovieId"] = new SelectList(movies, "Id", "Name");

            return View();
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] FileInputModel model)
    {
        if (model == null || model.FileToUpload == null || model.FileToUpload.Length == 0)
            return Content("file not selected");

        string imagePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        var finalPath = Path.Combine(imagePath, model.FileToUpload.FileName);

        var ulr = await _apiHttpClient.PostAsyncMultipartFormDataContent(HttpContext, "/api/Movies/upload", finalPath);

        var dataUrl = await ulr.Content.ReadAsStringAsync();
        Console.WriteLine(dataUrl);

        var movieBlob = new MovieBlob();
        movieBlob.BlobUrl = dataUrl.Trim('"');
        movieBlob.Description = model.Description;
        movieBlob.MovieId = model.MovieId;

        var moviesJson = JsonConvert.SerializeObject(movieBlob);
        var data = new StringContent(moviesJson, Encoding.UTF8, "application/json");
        var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Movies/blob", data);

        await response.Content.ReadAsStringAsync();

        return RedirectToAction("Index", "Movies");
    }
}
