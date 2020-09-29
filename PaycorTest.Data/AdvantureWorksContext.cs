using Microsoft.EntityFrameworkCore;
using PaycorTest.Data.Entities;
using System;

namespace PaycorTest.Data
{
    public class AdvantureWorksContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<ShipMethod> ShipMethods { get; set; }
        public DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }
        public DbSet<Culture> Cultures { get; set; }

        public AdvantureWorksContext(DbContextOptions<AdvantureWorksContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseOrderDetail>().HasKey("PurchaseOrderID", "PurchaseOrderDetailID");
            modelBuilder.Entity<ProductModelProductDescriptionCulture>().HasKey("ProductModelID", "ProductDescriptionID", "CultureID");
        }
    }
}
