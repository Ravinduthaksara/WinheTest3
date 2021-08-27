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
                    CustomerId = customer.CustomerId,
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
                    CustomerName =customer.CustomerName,
                    CustomerEmail=customer.CustomerEmail,
                    Address=customer.Address,
                    ContactNumber=customer.ContactNumber,
                    DateofBirth=customer.DateofBirth,
                    Gender=customer.Gender
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
        public ActionResult Edit(int id)
        {
            using (var db = new Winhe_ITEntities1())
            { 
               var editcustomer = db.Customers.Where(x => x.CustomerId == id).First();
                Customer customer = new Customer
                {
                    CustomerName = editcustomer.CustomerName,
                    CustomerEmail = editcustomer.CustomerEmail,
                    Address = editcustomer.Address,
                    ContactNumber = editcustomer.ContactNumber,
                    DateofBirth = (DateTime)editcustomer.DateofBirth,
                    Gender = editcustomer.Gender
                };
                return View("Edit",editcustomer);
            }
                             
        }
        public ActionResult SaveEdit(CustomerModel customerModel)
        {
            using (var db = new Winhe_ITEntities1())
            {
                Customer cusomer = new Customer
                {
                    CustomerName = customerModel.CustomerName,
                    CustomerEmail = customerModel.CustomerEmail,
                    Address = customerModel.Address,
                    ContactNumber = customerModel.ContactNumber,
                    DateofBirth = (DateTime)customerModel.DateofBirth,
                    Gender = customerModel.Gender

                };
                db.Customers.Attach(cusomer);
                db.Entry(cusomer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}