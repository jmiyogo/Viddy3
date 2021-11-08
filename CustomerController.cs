using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Viddly3.Models;
using System.Data.Entity;
using Viddly3.ViewModels;

namespace Viddly3.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController()
        {
            _context = new AppDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // GET: Customer
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType);
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c=> c.MembershipType).SingleOrDefault(c => c.Id == id);
            return View(customer);
        }

        public ActionResult New()
        {
            CustomerFormViewModel viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                CustomerFormViewModel viewModel = new CustomerFormViewModel(customer)
                {
                        MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm",viewModel);
            }
            if(customer.Id == 0)
            _context.Customers.Add(customer);
            Customer customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;      
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter; 
            customerInDb.MembershipTypeId = customer.MembershipTypeId; 
            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.Single(c => c.Id == id);
            CustomerFormViewModel viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }
    }
}