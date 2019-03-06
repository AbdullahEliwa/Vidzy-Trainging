using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        #region Db Connection Object
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        #region Read (GET All Movies - GET One Movie)
        // GET /api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        #endregion

        #region Create (Create New Movie)
        // POST /api/customer
        [HttpPost]
        //[Authorize(Roles =RoleName.CanManageMovie)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!User.IsInRole(RoleName.CanManageMovie))
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }
        #endregion

        #region Update (Edit Movie Data)
        // PUT /api/movies/1
        [HttpPut]
        //[Authorize(Roles =RoleName.CanManageMovie)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!User.IsInRole(RoleName.CanManageMovie))
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            Mapper.Map<MovieDto, Movie>(movieDto, movie);
            _context.SaveChanges();
            return Ok();
        }
        #endregion

        #region Delete (Delete Movie)
        // DELETE /api/movies/1
        [HttpDelete]
        //[Authorize(Roles =RoleName.CanManageMovie)]
        public IHttpActionResult DeleteMovie(int id)
        {
            if (! User.IsInRole(RoleName.CanManageMovie))
                return BadRequest();

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie is null)
                return NotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }

        #endregion
    }
}
