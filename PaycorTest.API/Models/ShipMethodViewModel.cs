using System;

namespace PaycorTest.Api.Models
{
    public class ShipMethodViewModel
    {
        public int ShipMethodID { get; set; }
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
