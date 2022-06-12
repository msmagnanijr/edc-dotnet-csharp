using Domain.Model.Entities;
using Infrastructure.Services.Queue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace WebMvc.Controllers;

public class ReviewsController : Controller
{
    private readonly ApiHttpClient _apiHttpClient;
    private readonly IConfiguration _configuration;

    public ReviewsController(ApiHttpClient apiHttpClient, IConfiguration configuration)
    {
        _apiHttpClient = apiHttpClient;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            List<ReviewEntity> reviews = new();

            var response = await _apiHttpClient.GetAsync(HttpContext, "/api/Reviews/getreviews");

            if (response.IsSuccessStatusCode)
            {
                var reviewResponse = response.Content.ReadAsStringAsync().Result;
                reviews = JsonConvert.DeserializeObject<List<ReviewEntity>>(reviewResponse);
            }

            return View(reviews);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public async Task<ReviewEntity> GetReview(int id)
    {
        var review = new ReviewEntity();
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Reviews/getreview/{id}");
        if (response.IsSuccessStatusCode)
        {
            var reviewResponse = response.Content.ReadAsStringAsync().Result;
            review = JsonConvert.DeserializeObject<ReviewEntity>(reviewResponse);
        }
        return review;
    }


    public async Task<List<MovieEntity>> GetMovies()
    {
        List<MovieEntity> movies = new();

        var response = await _apiHttpClient.GetAsync(HttpContext, "/api/Movies/getmovies");

        if (response.IsSuccessStatusCode)
        {
            var movieResponse = response.Content.ReadAsStringAsync().Result;
            movies = JsonConvert.DeserializeObject<List<MovieEntity>>(movieResponse);
        }

        return movies;
    }

    public async Task<IActionResult> Details(int id)
    {
        var review = await GetReview(id);
        if (review == null)
        {
            return NotFound();
        }
        return View(review);
    }

    public async Task<IActionResult> CreateAsync()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            List<MovieEntity> movies = await GetMovies();
            ViewData["MovieId"] = new SelectList(movies, "Id", "Name");
            return View();
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReviewEntity review)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            if (ModelState.IsValid)
            {
                review.Reviewer = HttpContext.Session.GetString("username");
                var reviewJson = JsonConvert.SerializeObject(review);
                var data = new StringContent(reviewJson, Encoding.UTF8, "application/json");
                var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Reviews/createreview", data);
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


    public async Task<IActionResult> Edit(int id)
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            ReviewEntity review = await GetReview(id);
            List<MovieEntity> movies = await GetMovies();
            ViewData["MovieId"] = new SelectList(movies, "Id", "Name");
            return View(review);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }



    [HttpPost]
    public async Task<IActionResult> Edit(int id, ReviewEntity review)
    {
        if (id != review.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                review.Reviewer = HttpContext.Session.GetString("username");
                var reviewsJson = JsonConvert.SerializeObject(review);
                var data = new StringContent(reviewsJson, Encoding.UTF8, "application/json");
                var response = await _apiHttpClient.PutAsync(HttpContext, $"/api/Reviews/updatereview", data);
                string responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewEntityExists(review.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        List<MovieEntity> movies = await GetMovies();
        ViewData["MovieId"] = new SelectList(movies, "Id", "Name");
        return View(review);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var review = await GetReview(id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        var response = await _apiHttpClient.DeleteAsync(HttpContext, $"/api/Reviews/removereview/{id}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
        }

        return RedirectToAction(nameof(Index));
    }
    private bool ReviewEntityExists(int id)
    {
        return GetReview(id) != null;
    }

    public async Task<IActionResult> Comment(int id)
    {
        var review = await GetReview(id);
        var comments = new List<CommentEntity>();
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Reviews/getcomments/{review.Id}");
        if (response.IsSuccessStatusCode)
        {
            var commentResponse = response.Content.ReadAsStringAsync().Result;
            comments = JsonConvert.DeserializeObject<List<CommentEntity>>(commentResponse);
        }

        ViewData["Comments"] = comments;
        ViewData["ReviewId"] = review.Id;

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> CommentCreate(CommentEntity commentEntity, int reviewId)
    {
        if (ModelState.IsValid)
        {
            commentEntity.CreatedBy = HttpContext.Session.GetString("username");
            commentEntity.CreatedAt = DateTime.UtcNow;
            commentEntity.ReviewId = reviewId;

            Console.WriteLine($"comentario {commentEntity.Comment}");

            var commentJson = JsonConvert.SerializeObject(commentEntity);
            var data = new StringContent(commentJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Reviews/createcomment", data);
            string responseContent = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Upvote([FromBody] int id)
    {

        var comment = await GetComment(id);

        comment.Upvote++;

        await UpdateComment(comment);

        await GetAllComments();

        return Json(comment.Upvote);
        
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Downvote([FromBody] int id)
    {

        var comment = await GetComment(id);

        comment.Upvote--;

        await UpdateComment(comment);

        await GetAllComments();

        return Json(comment.Upvote);

    }

    public async Task<CommentEntity> GetComment(int id)
    {
        var comment = new CommentEntity();
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Reviews/getcomment/{id}");
        if (response.IsSuccessStatusCode)
        {
            var commentResponse = response.Content.ReadAsStringAsync().Result;
            comment = JsonConvert.DeserializeObject<CommentEntity>(commentResponse);
        }

        return comment;
    }

    public async Task<List<CommentEntity>> GetAllComments()
    {
        var comments = new List<CommentEntity>();
        var response = await _apiHttpClient.GetAsync(HttpContext, $"/api/Reviews/getallcomments");
        if (response.IsSuccessStatusCode)
        {
            var commentResponse = response.Content.ReadAsStringAsync().Result;
            comments = JsonConvert.DeserializeObject<List<CommentEntity>>(commentResponse);
        }

        return comments;
    }

    [HttpPost]
    public async Task<ActionResult<CommentEntity>> UpdateComment(CommentEntity commentEntity)
    {
        
        var commentJson = JsonConvert.SerializeObject(commentEntity);
        var data = new StringContent(commentJson, Encoding.UTF8, "application/json");
        var response = await _apiHttpClient.PutAsync(HttpContext, "/api/Reviews/updatecomment", data);
        string responseContent = await response.Content.ReadAsStringAsync();
        return NoContent();
    }
}