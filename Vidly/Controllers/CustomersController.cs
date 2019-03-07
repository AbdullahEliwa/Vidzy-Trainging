using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Data.Entity.Validation;
using System.Runtime.Caching;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        #region Database connection object
        ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        #region Index & Details Actions
        // GET: Customers
        public ActionResult Index()
        {

            //====================================================================
            //This code show how to cache data, but use it only after profile your app and
            // when found you really need to cache data over doing queries to DB
            // if(MemoryCache.Default["Genres"] == null)
            // {
            //      MemoryCache.Default["Genres"] = _context.Genres.ToList();
            //  }
            //var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;
            //===================================================================
            // Now i get data from API, so i don't need to get data from here any more
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View();
        }

        public ActionResult Details(int? id)
        {
            Customer customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        #endregion

        #region Add & Edit & Save Actions

        //Add Action
        public ActionResult Add()
        {
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        // Edit Action
        public ActionResult Edit(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customerInDb,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        // Save Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        #endregion
    }
}