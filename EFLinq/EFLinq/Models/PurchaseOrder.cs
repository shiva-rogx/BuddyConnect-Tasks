//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFLinq.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrder
    {
        public int POID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public System.DateTime DateOfPurchase { get; set; }
        public decimal Amount { get; set; }
    }
}
