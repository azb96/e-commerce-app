namespace uyumsoft_ticaret_app.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeatureDetail")]
    public partial class FeatureDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeatureDetail()
        {
            Product_Feature = new HashSet<Product_Feature>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string FeatureDetailName { get; set; }

        [StringLength(500)]
        public string Detail { get; set; }

        public int? FeatureTypeID { get; set; }

        public virtual FeatureType FeatureType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Feature> Product_Feature { get; set; }
    }
}
