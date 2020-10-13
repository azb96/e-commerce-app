namespace uyumsoft_ticaret_app.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductReview")]
    public partial class ProductReview
    {
        public int id { get; set; }

        [StringLength(500)]
        public string Review { get; set; }

        public int Rating { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
