//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RAAMEN.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RamenId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Ramen Ramen { get; set; }
        public virtual User User { get; set; }
    }
}
