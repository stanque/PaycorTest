using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name: "Product", Schema = "Production")]
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public Int16? ReorderPoint { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal StandardCost { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        [ForeignKey("SizeUnitMeasure")]
        public string SizeUnitMeasureCode { get; set; }
        [ForeignKey("WeightUnitMeasure")]
        public string WeightUnitMeasureCode { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Weight { get; set; }
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        [ForeignKey("ProductSubcategory")]
        public int? ProductSubcategoryID { get; set; }
        [ForeignKey("ProductModel")]
        public int? ProductModelID { get; set; }
        public DateTime SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        [NotMapped]
        public string ProductDescription { get; set; }

        public virtual ProductModel ProductModel { get; set; }
        public virtual ProductSubcategory ProductSubcategory { get; set; }
        public virtual UnitMeasure SizeUnitMeasure { get; set; }
        public virtual UnitMeasure WeightUnitMeasure { get; set; }
    }
}
