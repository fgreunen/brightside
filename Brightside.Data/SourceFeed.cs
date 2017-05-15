//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Brightside.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SourceFeed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SourceFeed()
        {
            this.Articles = new HashSet<Article>();
            this.SourceFeedsHistories = new HashSet<SourceFeedsHistory>();
        }
    
        public int Id { get; set; }
        public double Score { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public int SourceId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
        public virtual Source Source { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SourceFeedsHistory> SourceFeedsHistories { get; set; }
    }
}