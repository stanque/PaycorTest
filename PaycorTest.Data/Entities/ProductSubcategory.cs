using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name: "ProductSubcategory", Schema = "Production")]
    public class ProductSubcategory
    {
        [Key]
        public int ProductSubcategoryID { get; set; }
        [ForeignKey("ProductCategory")]
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
