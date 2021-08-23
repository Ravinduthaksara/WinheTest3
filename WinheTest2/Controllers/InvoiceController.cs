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
            using (var db = new Winhe_ITEntities())
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

            return View(invoice);
        }

        [HttpPost]
        public ActionResult Create(InvoiceModel invoiceretrieve)
        {
            var cusinvoice = new Invoice
            {
                InvoiceNumber = invoiceretrieve.InvoiceNumber,
                InvoiceDate = invoiceretrieve.InvoiceDate,
                CustomerId=invoiceretrieve.CustomerId,
                TotalUnits=invoiceretrieve.TotalUnits,
                TotalPrice=invoiceretrieve.TotalPrice,
                Discount = invoiceretrieve.Discount,
                ProductId = invoiceretrieve.ProductId,
                Quantity = invoiceretrieve.Quantity,
                UnitPrice = invoiceretrieve.UnitPrice

        };
            using (var db = new Winhe_ITEntities())
            {
                db.Invoices.Add(cusinvoice);
                db.SaveChanges();
            }


            return RedirectToAction("Detail");
        }
    }
}