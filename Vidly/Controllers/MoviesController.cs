using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        IEnumerable<Movie> Movies = new List<Movie>
        {
            new Movie { Id = 1, Name = "Harry Potter"}
        };

        // GET: Movies
        public ActionResult Index()
        {
            return View(Movies);
        }

        public ActionResult Details(int? id)
        {
            var movie = Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
    }
}