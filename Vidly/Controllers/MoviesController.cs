using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        #region DB Connection Object
        ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        #region Index & Deatils Actons
        // GET: Movies
        public ViewResult Index()
        {
            #region Old Code: Retreiving data
            // NOW i use api to retrieve data, SO i don't need this any more.
            //var movies = _context.Movies.Include(m => m.Genre).ToList();
            #endregion

            if (User.IsInRole(RoleName.CanManageMovie))
                return View();

            return View("ReadONlyMovieList");
        }

        public ActionResult Details(int? id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
        #endregion

        #region Add & Edit & Save Actions

        [ Authorize(Roles = RoleName.CanManageMovie) ]
        public ActionResult Add()
        {
            var viewModel = new MovieFormViewModel()
            {
                Id = 0,
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles =RoleName.CanManageMovie)]
        public ActionResult Edit(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movieInDb)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =RoleName.CanManageMovie)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);
            if (movieInDb == null)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}