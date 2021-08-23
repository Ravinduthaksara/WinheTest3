using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinheTest2.Models
{
    public class InvoiceModel
    {
        public int InvoiceNumber { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> TotalUnits { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}