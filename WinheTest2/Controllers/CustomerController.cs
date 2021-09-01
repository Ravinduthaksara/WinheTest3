using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinheTest2.Models;

namespace WinheTest2.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<CustomerModel> wincustomer = new List<CustomerModel>();
            List<Customer> dbcustomer;
            using (var db = new Winhe_ITEntities1())
            {
                dbcustomer = db.Customers.ToList();
            }
            foreach (var customer in dbcustomer)
            {
                var modelCustomer = new CustomerModel
                {
                    CustomerName = customer.CustomerName,
                    CustomerEmail = customer.CustomerEmail,
                    Address = customer.Address,
                    ContactNumber = customer.ContactNumber,
                    DateofBirth = customer.DateofBirth,
                    Gender = customer.Gender
                };
                wincustomer.Add(modelCustomer);
            }
            return View(wincustomer);

        }

        [HttpPost]
        public ActionResult Search(string searchText)
        {
            List<CustomerModel> wincustomer = new List<CustomerModel>();
            List<Customer> dbcustomer;
            using (var db = new Winhe_ITEntities1())
            {
                dbcustomer = db.Customers.Where(x => x.CustomerName.Contains(searchText)).ToList();
            }
            foreach (var customer in dbcustomer)
            {
                var modelcustomer = new CustomerModel
                {
                    CustomerName = customer.CustomerName,
                    CustomerEmail = customer.CustomerEmail,
                    Address = customer.Address,
                    ContactNumber = customer.ContactNumber,
                    DateofBirth = customer.DateofBirth,
                    Gender = customer.Gender
                };
                wincustomer.Add(modelcustomer);
            }
            return View("List", wincustomer);

        }

        [HttpPost]
        public ActionResult Create(CustomerModel customer)
        {
            var addcustomer = new Customer
            {
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                Address = customer.Address,
                ContactNumber = customer.ContactNumber,
                DateofBirth = (DateTime)customer.DateofBirth,
                Gender = customer.Gender
            };
            using (var db = new Winhe_ITEntities1())
            {
                db.Customers.Add(addcustomer);
                db.SaveChanges();
            }
            return RedirectToAction("Add");
        }
        public ActionResult Add()
        {
            var customer = new CustomerModel();
            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            using (var db = new Winhe_ITEntities1())
            {

                var removecustomer = db.Customers.Where(x => x.CustomerId == id).First();
                db.Customers.Remove(removecustomer);
                db.SaveChanges();

                return RedirectToAction("List");
            }


        }
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult GetCustomersList(string searchText)
        {
            var customers = new List<SelectListItem>();
            using (var db = new Winhe_ITEntities1())
            {
                customers = db.Customers.Where(x => x.CustomerName.Contains(searchText)).Select(y => new SelectListItem { Text = y.CustomerName, Value = y.CustomerId.ToString() }).ToList();
                return PartialView("CustomerListPartial", customers);
            }
        }
    }
}