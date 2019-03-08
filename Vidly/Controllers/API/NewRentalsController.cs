using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class NewRentalsController : ApiController
    {
        #region DB Connection Object
        private ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRentalDto newRentalDto)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerId);
            if (customer is null)
                return BadRequest("No customer matched what you have searched for.");

            var moviesList = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();
            if (moviesList.Count is 0)
                return BadRequest("No movie matched what you have searched for.");
            
            foreach (var movie in moviesList)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                var rental = new Rental()
                {
                    CustomerId = customer.Id,
                    MovieId = movie.Id,
                    RentedDay = DateTime.Now
                };
                movie.NumberAvailable--;
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();

        }








    }
}
