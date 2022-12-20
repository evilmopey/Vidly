using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var movies = _db.Movies.Include(c => c.Genre).ToList();
            var ViewModel = new MovieViewModel { Movies = movies };

            return View(ViewModel);
        }

        public ActionResult Details(int id)
        {
            var movies = _db.Movies.Include(c => c.Genre).ToList();
            var movie = movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                var MovieModel = new MovieViewModel { Movie = movie };
                return View(MovieModel);
            }
        }

        public ActionResult New()
        {

            var genres = _db.Genres.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            var viewModel = new MovieFormViewModel { Genres = genres };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _db.Movies.Add(movie);
            }
            else
            {
                _db.Movies.Update(movie);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }

        public ActionResult Edit(int id)
        {
            var movies = _db.Movies.Include(c => c.Genre).ToList();
            var movie = movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            var genres = _db.Genres.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,

                Genres = genres
            };

            return View("MovieForm", viewModel);
        }






        //[Route("movies/released/{year}/{month: regex(\\d{2}), range(1,12)}")] somethingwent wrong here
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        //GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Cust 1"},
                new Customer { Name = "Cust 2" }
            };

            var ViewModel = new MovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            //ViewData["Movie"] = movie;

            return View(ViewModel);
            //return new ViewResult();
            //return Content("Hello World");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortby = "name"});

        }
    }
}
