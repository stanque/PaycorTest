using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name: "ProductDescription", Schema = "Production")]
    public class ProductDescription
    {
        [Key]
        public int ProductDescriptionID { get; set; }
        public string Description { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
