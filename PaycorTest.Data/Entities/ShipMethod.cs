using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name:"ShipMethod", Schema = "Purchasing")]
    public class ShipMethod
    {
        [Key]
        public int ShipMethodID { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ShipBase { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ShipRate { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
