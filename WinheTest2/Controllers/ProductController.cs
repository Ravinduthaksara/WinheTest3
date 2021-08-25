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
            List<ProductModel> winproduct = new List<ProductModel>();
            List<Product> dbproduct;
            using (var db = new Winhe_ITEntities1())
            {
                dbproduct = db.Products.ToList();
            }
            foreach(var product in dbproduct)
            {
                var modelProduct = new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    PurchasePrice = product.PurchasePrice,
                    SellingPrice = product.SellingPrice,
                    Quantity = product.Quantity
                };
                winproduct.Add(modelProduct);
            }
            return View(winproduct);

        }
        [HttpPost]
        public ActionResult Search(string searchText)
        {
            List<ProductModel> winproduct = new List<ProductModel>();
            List<Product> dbproduct;
            using (var db = new Winhe_ITEntities1())
            {
                dbproduct = db.Products.Where(x=>x.ProductName.Contains(searchText)).ToList();
            }
            foreach (var product in dbproduct)
            {
                var modelProduct = new ProductModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    PurchasePrice = product.PurchasePrice,
                    SellingPrice = product.SellingPrice,
                    Quantity = product.Quantity
                };
                winproduct.Add(modelProduct);
            }
            return View("List",winproduct);

        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel product)
        {
            var addproduct = new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice,
                Quantity = product.Quantity
            };
            using (var db = new Winhe_ITEntities1())
            {
                db.Products.Add(addproduct);
                db.SaveChanges();
            }
            return RedirectToAction("Add");
        }

        public ActionResult Delete(int id)
        {
            using (var db = new Winhe_ITEntities1())
            { 
            
             var removeproduct =  db.Products.Where(x => x.ProductId == id).First();
             db.Products.Remove(removeproduct);
             db.SaveChanges();
                //var list = db.Products.ToList();

                return RedirectToAction("List");
            }
           
            
        }
        public ActionResult Add()
        {
            var product = new ProductModel();
            return View(product);
        }
        public ActionResult Edit(int id)
        {
            using (var db = new Winhe_ITEntities1())
            {
                var editproduct = db.Products.Where(x => x.ProductId == id).First();
                ProductModel product = new ProductModel
                {
                    ProductId = editproduct.ProductId,
                    ProductName = editproduct.ProductName,
                    ProductDescription = editproduct.ProductDescription,
                    PurchasePrice = editproduct.PurchasePrice,
                    SellingPrice = editproduct.SellingPrice,
                    Quantity = editproduct.Quantity
                };
                return View("Edit", product);
            }
                

        }
        [HttpPost]
        public ActionResult SaveEdit(ProductModel productModel)
        {
            using (var db = new Winhe_ITEntities1())
            {
                Product product = new Product
                {
                    ProductId = productModel.ProductId,
                    ProductName = productModel.ProductName,
                    ProductDescription = productModel.ProductDescription,
                    PurchasePrice = productModel.PurchasePrice,
                    SellingPrice = productModel.SellingPrice,
                    Quantity = productModel.Quantity
                };
                db.Products.Attach(product);
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}