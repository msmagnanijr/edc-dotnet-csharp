using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAwesomeTomatoes.Models;
using WebAwesomeTomatoes.Repositories;

namespace WebAwesomeTomatoes.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IActionResult> Index(string filter)
        {
            if (!string.IsNullOrEmpty(filter)) return View(await _movieRepository.GetByFilter(filter));
            return View(await _movieRepository.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return NotFound();
            return View(movie);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(z => z.Exception));
            Console.Write(errors);
            if (ModelState.IsValid)
            {
                if (_movieRepository.EntityExists(movie.Id)) return BadRequest("Esse filme já existe.");
                await _movieRepository.Create(movie);
                await _movieRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _movieRepository.Update(movie);
                    await _movieRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_movieRepository.EntityExists(movie.Id))
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
            return View(movie);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieRepository.GetById(id);
            await _movieRepository.Delete(movie);
            await _movieRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
