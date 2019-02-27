using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

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
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int? id)
        {
            Customer customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        #endregion

        #region Add Action
        public ActionResult Add()
        {
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View(viewModel);
        }
        #endregion

        #region Save Action
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            _context.Customers.Add(customer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e )
            {

                throw;
            }
           
            return RedirectToAction("Index", "Customers");
        }
        #endregion
    }
}