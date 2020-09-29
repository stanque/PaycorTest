using System;

namespace PaycorTest.Application.Models
{
    public class PurchaseOrderHeader
    {
        public int PurchaseOrderID { get; set; }
        public byte RevisionNumber { get; set; }
        public byte Status { get; set; }
        public int EmployeeID { get; set; }
        public int VendorID { get; set; }
        public int ShipMethodID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public DateTime ModifiedDate { get; set; }


        public Vendor Vendor { get; set; }
        public ShipMethod ShipMethod { get; set; }
    }
}
