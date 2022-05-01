using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using WebAwesomeTomatoes.Repositories;

namespace WebAwesomeTomatoes.Controllers;
public class ReviewsController : Controller
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMovieRepository _movieRepository;
    public ReviewsController(IReviewRepository reviewRepository, IMovieRepository movieRepository)
    {
        _reviewRepository = reviewRepository;
        _movieRepository = movieRepository;
    }
    public async Task<IActionResult> Index(string filter)
    {
        if (!string.IsNullOrEmpty(filter)) return View(await _reviewRepository.GetByFilter(filter));
        return View(await _reviewRepository.GetAll());
    }

    public async Task<IActionResult> Details(int id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review == null) return NotFound();
        return View(review);
    }

    public IActionResult Create()
    {
        ViewData["MovieId"] = new SelectList(_movieRepository.Movies(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Review review)
    {
        if (ModelState.IsValid)
        {
            await _reviewRepository.Create(review);
            await _reviewRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MovieId"] = new SelectList(_movieRepository.Movies(), "Id", "Name");
        return View(review);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review == null) return NotFound();
        ViewData["MovieId"] = new SelectList(_movieRepository.Movies(), "Id", "Name");
        return View(review);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Review review)
    {
        if (id != review.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                await _reviewRepository.Update(review);
                await _reviewRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_reviewRepository.EntityExists(review.Id))
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
        ViewData["MovieId"] = new SelectList(_movieRepository.Movies(), "Id", "Name");
        return View(review);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var review = await _reviewRepository.GetById(id);
        if (review == null) return NotFound();
        return View(review);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var review = await _reviewRepository.GetById(id);
        await _reviewRepository.Delete(review);
        await _reviewRepository.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}