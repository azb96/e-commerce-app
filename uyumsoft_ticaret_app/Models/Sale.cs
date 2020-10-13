namespace uyumsoft_ticaret_app.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sale")]
    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            SaleDetails = new HashSet<SaleDetail>();
        }

        public int id { get; set; }

        public int? CustomerID { get; set; }

        [Column(TypeName = "date")]
        public DateTime SaleDate { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }

        public bool inBasket { get; set; }

        public int? CargoID { get; set; }

        public int? OrderStatusID { get; set; }

        [StringLength(20)]
        public string CargoNo { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        public virtual UserRecord UserRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
