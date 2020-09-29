using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaycorTest.Data.Entities
{
    [Table(name: "PurchaseOrderDetail", Schema = "Purchasing")]
    public class PurchaseOrderDetail
    {
        public int PurchaseOrderDetailID { get; set; }
        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderID { get; set; }
        public DateTime DueDate { get; set; }
        public Int16 OrderQty { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? LineTotal { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ReceivedQty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal RejectedQty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal StockedQty { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual PurchaseOrderHeader PurchaseOrder { get; set; }
        public virtual Product Product { get; set; }

    }
}
