using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinheTest2.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> SellingPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}