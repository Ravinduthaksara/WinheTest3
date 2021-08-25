//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinheTest2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        public int InvoiceNumber { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> TotalUnits { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<System.DateTime> UpdateAt { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
