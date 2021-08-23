using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinheTest2.Models;

namespace WinheTest2.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<ProductModel> product = new List<ProductModel>();
            List<Product> dbproduct;
            using (var db = new Winhe_ITEntities())
            {
                dbproduct = db.Products.ToList();
            }
            foreach(var prod in dbproduct)
            {

            }
                return View();
        }
    }
}