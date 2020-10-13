namespace uyumsoft_ticaret_app.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        public int id { get; set; }

        public int UserID { get; set; }

        [Column("Address")]
        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string AddressName { get; set; }

        public virtual UserRecord UserRecord { get; set; }
    }
}
