using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name: "PurchaseOrderHeader", Schema = "Purchasing")]
    public class PurchaseOrderHeader
    {
        [Key]
        public int PurchaseOrderID { get; set; }
        public byte RevisionNumber { get; set; }
        public byte Status { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("Vendor")]
        public int VendorID { get; set; }
        [ForeignKey("ShipMethod")]
        public int ShipMethodID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TaxAmt { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Freight { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalDue { get; set; }
        public DateTime ModifiedDate { get; set; }


        public virtual Vendor Vendor { get; set; }
        public virtual ShipMethod ShipMethod { get; set; }
    }
}
