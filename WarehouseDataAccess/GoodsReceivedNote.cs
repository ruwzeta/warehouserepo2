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
    
    public partial class GoodsReceivedNote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoodsReceivedNote()
        {
            this.GoodsReceiveItems = new HashSet<GoodsReceiveItem>();
        }
    
        public string RecNoteID { get; set; }
        public System.DateTime NoteDate { get; set; }
        public string IssueEmpID { get; set; }
        public string Type { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsReceiveItem> GoodsReceiveItems { get; set; }
    }
}
