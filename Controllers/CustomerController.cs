using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var customers = _db.Customers.Include(c => c.MembershipType).ToList();


            var viewModel = new CustomerViewModel { Customers = customers };
            return View(viewModel);




        }
        public ActionResult Details(int id)
        {
            var customers = _db.Customers.Include(c => c.MembershipType).ToList();
            var customer = customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = new CustomerViewModel { Customer = customer };
                return View(viewModel);
            }

        }

        public ActionResult New()
        {
            var membershipTypes = _db.MembershipTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            var viewModel = new CustomerFormViewModel { MembershipTypes = membershipTypes };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypes = _db.MembershipTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                var viewModel = new CustomerFormViewModel { Customer = customer, MembershipTypes = membershipTypes };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                _db.Customers.Add(customer);
            }
            else
            {
                _db.Customers.Update(customer);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int id)
        {
            var customers = _db.Customers.Include(c => c.MembershipType).ToList();
            var customer = customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            var membershipTypes = _db.MembershipTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,

                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }



    }
}
