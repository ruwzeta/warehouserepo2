//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WarehouseDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReturnNoteItem
    {
        public string ReturnNoteItemID { get; set; }
        public string ReturnNoteID { get; set; }
        public string ItemID { get; set; }
    
        public virtual ReturnNote ReturnNote { get; set; }
    }
}
