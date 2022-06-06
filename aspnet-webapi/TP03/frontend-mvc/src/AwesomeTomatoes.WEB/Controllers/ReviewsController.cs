namespace AwesomeTomatoes.WEB.Controllers;

public class ReviewsController : Controller
{
    private readonly ApiHttpClient _apiHttpClient;
    public ReviewsController(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }
    public async Task<ActionResult> Index()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            List<Review> reviews = new();

            var response = await _apiHttpClient.GetAsync(HttpContext, "/api/Reviews");

            if (response.IsSuccessStatusCode)
            {
                var reviewResponse = response.Content.ReadAsStringAsync().Result;
                reviews = JsonConvert.DeserializeObject<List<Review>>(reviewResponse);
            }

            return View(reviews);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public async Task<IActionResult> Create()
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
    public async Task<IActionResult> Create(Review review)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            if (ModelState.IsValid)
            {
                var reviewJson = JsonConvert.SerializeObject(review);
                var data = new StringContent(reviewJson, Encoding.UTF8, "application/json");
                var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Reviews", data);

                string responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            var review = GetReview(id);
            return View(review.Result);
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
            var review = GetReview(id);
            List<Movie> movies = new();

            var response = await _apiHttpClient.GetAsync(HttpContext, "/api/Movies");

            if (response.IsSuccessStatusCode)
            {
                var movieResponse = response.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<Movie>>(movieResponse);
            }
            ViewData["MovieId"] = new SelectList(movies, "Id", "Name");

            return View(review.Result);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> Edit(int id, Review review)
    {
        if (id != review.Id) return NotFound();
        if (ModelState.IsValid)
        {
            var reviewsJson = JsonConvert.SerializeObject(review);
            var data = new StringContent(reviewsJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PutAsync(HttpContext, $"/api/Reviews/{id}", data);
            string responseContent = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(review);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            var review = GetReview(id);
            return View(review.Result);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _apiHttpClient.DeleteAsync(HttpContext, $"/api/Reviews/{id}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<Review> GetReview(int id)
    {
        var review = new Review();
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Reviews/{id}");
        if (response.IsSuccessStatusCode)
        {
            var reviewResponse = response.Content.ReadAsStringAsync().Result;
            review = JsonConvert.DeserializeObject<Review>(reviewResponse);
        }
        return review;
    }
}
