using System;

namespace PaycorTest.Api.Models
{
    public class PurchaseOrderDetailViewModel
    {
        public int PurchaseOrderDetailID { get; set; }
        public int PurchaseOrderID { get; set; }
        public DateTime DueDate { get; set; }
        public Int16 OrderQty { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? LineTotal { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal RejectedQty { get; set; }
        public decimal StockedQty { get; set; }
        public DateTime ModifiedDate { get; set; }

        public PurchaseOrderHeaderViewModel PurchaseOrder { get; set; }
        public ProductViewModel Product { get; set; }

    }
}
