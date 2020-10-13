namespace uyumsoft_ticaret_app.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class uyumticaret2 : DbContext
    {
        public uyumticaret2()
            : base("name=uyumticaret2")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cargo> Cargoes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<EntryDetail> EntryDetails { get; set; }
        public virtual DbSet<FeatureDetail> FeatureDetails { get; set; }
        public virtual DbSet<FeatureType> FeatureTypes { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Feature> Product_Feature { get; set; }
        public virtual DbSet<ProductReview> ProductReviews { get; set; }
        public virtual DbSet<RoleRecord> RoleRecords { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        public virtual DbSet<SalesmanReview> SalesmanReviews { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserRecord> UserRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>()
                .Property(e => e.Telephone)
                .IsFixedLength();

            modelBuilder.Entity<Category>()
                .HasMany(e => e.FeatureTypes)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeatureDetail>()
                .HasMany(e => e.Product_Feature)
                .WithRequired(e => e.FeatureDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeatureType>()
                .HasMany(e => e.Product_Feature)
                .WithRequired(e => e.FeatureType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderStatu>()
                .HasMany(e => e.Sales)
                .WithOptional(e => e.OrderStatu)
                .HasForeignKey(e => e.OrderStatusID);

            modelBuilder.Entity<Product>()
                .Property(e => e.Detail)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Feature)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductReviews)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.SaleDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleRecord>()
                .HasMany(e => e.UserRecords)
                .WithRequired(e => e.RoleRecord)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.TotalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Sale>()
                .HasMany(e => e.SaleDetails)
                .WithRequired(e => e.Sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SaleDetail>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<UserRecord>()
                .Property(e => e.Telephone)
                .IsFixedLength();

            modelBuilder.Entity<UserRecord>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.UserRecord)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRecord>()
                .HasOptional(e => e.EntryDetail)
                .WithRequired(e => e.UserRecord);

            modelBuilder.Entity<UserRecord>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.UserRecord)
                .HasForeignKey(e => e.SalesmanID);

            modelBuilder.Entity<UserRecord>()
                .HasMany(e => e.Sales)
                .WithOptional(e => e.UserRecord)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<UserRecord>()
                .HasMany(e => e.SalesmanReviews)
                .WithRequired(e => e.UserRecord)
                .HasForeignKey(e => e.SalesmanID)
                .WillCascadeOnDelete(false);
        }
    }
}
