using Microsoft.AspNetCore.Mvc;
using WebATomataoes.Repositories;
using WebATomataoes.ViewModels;

namespace WebATomataoes.Controllers
{
    public class FilmesController : Controller
    {
        // GET: FilmeController
        public ActionResult Index()
        {
            var filmes = FilmeRepository.GetAll();
            return View(filmes);
        }

        // POST: FilmeController/xxx
        [HttpPost]
        public ActionResult Index(string searchString)
        {
            List<FilmesViewModel> filmes = new List<FilmesViewModel>();
            if (!string.IsNullOrEmpty(searchString))
            {
                filmes = FilmeRepository.GetBySearch(searchString);
            }
            else {
                filmes = FilmeRepository.GetAll();
            }
            return View(filmes);
        }

        // GET: FilmeController/Details/5
        public ActionResult Details(Guid id)
        {
            var filmes = FilmeRepository.GetById(id);
            return View(filmes);
        }

        // GET: FilmeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilmeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilmesViewModel filmes)
        {
            try
            {
                filmes.Id = Guid.NewGuid();
                FilmeRepository.Create(filmes);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilmeController/Edit/5
        public ActionResult Edit(Guid id)
        {
            Console.WriteLine("Meu id Controller: ", id);
            var filmes = FilmeRepository.GetById(id);
            return View(filmes);
        }

        // POST: FilmeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, FilmesViewModel filmes)
        {
            try
            {
                FilmeRepository.Update(filmes);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilmeController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var filmes = FilmeRepository.GetById(id);
            return View(filmes);
        }

        // POST: FilmeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, FilmesViewModel filmes)
        {
            try
            {
                FilmeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
