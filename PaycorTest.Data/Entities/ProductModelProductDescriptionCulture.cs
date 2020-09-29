using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name: "ProductModelProductDescriptionCulture", Schema = "Production")]
    public class ProductModelProductDescriptionCulture
    {
        [ForeignKey("ProductModel")]
        public int ProductModelID { get; set; }
        [ForeignKey("ProductDescription")]
        public int ProductDescriptionID { get; set; }
        [ForeignKey("Culture")]
        public string CultureID { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductModel ProductModel { get; set; }
        public virtual ProductDescription ProductDescription { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
