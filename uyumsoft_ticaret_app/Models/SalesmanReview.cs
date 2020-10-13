namespace uyumsoft_ticaret_app.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesmanReview")]
    public partial class SalesmanReview
    {
        public int id { get; set; }

        public int SalesmanID { get; set; }

        public int Rating { get; set; }

        [StringLength(500)]
        public string Review { get; set; }

        public virtual UserRecord UserRecord { get; set; }
    }
}
