using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinheTest2.Models;

namespace WinheTest2.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            //This invoicemodel is a seperate data collection point in our app
            List<InvoiceModel> invoices = new List<InvoiceModel>();
            //this Invoide is representing database class(dataset or entity) 
            List<Invoice> dbinvoice;
            using (var db = new Winhe_ITEntities1())
            {
                dbinvoice = db.Invoices.ToList();
            }
            foreach (var invoice in dbinvoice)
            {
                var modelinvoice = new InvoiceModel
                {
                    InvoiceNumber = invoice.InvoiceNumber,
                    InvoiceDate = invoice.InvoiceDate,
                    CustomerId = invoice.CustomerId,
                    TotalUnits = invoice.TotalUnits,
                    TotalPrice = invoice.TotalPrice,
                    Discount = invoice.Discount,
                    ProductId = invoice.ProductId,
                    Quantity = invoice.Quantity,
                    UnitPrice = invoice.UnitPrice
                };
                invoices.Add(modelinvoice);
            }
            return View(invoices);
        }


        public ActionResult CreateInvoice()
        {
            var invoice = new InvoiceModel();

            //When you have limited no of options, you can pass it in a viewbag
            var productList = new List<SelectListItem>();
            using (var db = new Winhe_ITEntities1())
            {
                productList = db.Products.Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.ProductName }).ToList();
            }
            ViewBag.ProductList = productList;
            return View(invoice);
        }


        [HttpPost]
        public ActionResult Create(InvoiceModel invoiceretrieve)
        {
            var cusinvoice = new Invoice
            {
                InvoiceNumber = invoiceretrieve.InvoiceNumber,
                InvoiceDate = (DateTime)invoiceretrieve.InvoiceDate,
                CustomerId = (int)invoiceretrieve.CustomerId,
                TotalUnits = invoiceretrieve.TotalUnits,
                TotalPrice = invoiceretrieve.TotalPrice,
                Discount = invoiceretrieve.Discount,
                ProductId = (int)invoiceretrieve.ProductId,
                Quantity = invoiceretrieve.Quantity,
                UnitPrice = invoiceretrieve.UnitPrice

            };
            using (var db = new Winhe_ITEntities1())
            {
                db.Invoices.Add(cusinvoice);
                db.SaveChanges();
            }


            return RedirectToAction("Detail");
        }

        public ActionResult Edit(int id)
        {
            Invoice invoice = new Invoice();
            using (var db = new Winhe_ITEntities1())
            {
                invoice = db.Invoices.Where(x => x.InvoiceNumber == id).First();

                var modelinvoice = new InvoiceModel
                {
                    InvoiceNumber = invoice.InvoiceNumber,
                    InvoiceDate = invoice.InvoiceDate,
                    CustomerId = invoice.CustomerId,
                    TotalUnits = invoice.TotalUnits,
                    TotalPrice = invoice.TotalPrice,
                    Discount = invoice.Discount,
                    ProductId = invoice.ProductId,
                    Quantity = invoice.Quantity,
                    UnitPrice = invoice.UnitPrice,
                };
                db.Invoices.Attach(invoice);
                db.SaveChanges();
                return View("Edit", modelinvoice);
            }

            
        }

        public ActionResult Delete(int id)
        {
            using (var db = new Winhe_ITEntities1())
            {

                var removeinvoice = db.Invoices.Where(x => x.InvoiceNumber == id).First();
                db.Invoices.Remove(removeinvoice);
                db.SaveChanges();
                //var list = db.Products.ToList();

                return RedirectToAction("Detail");
            }
        }
    }

}