using System;

namespace PaycorTest.Service.ServiceModels
{
    public class ShipMethodDto
    {
        public int ShipMethodID { get; set; }
        public string Name { get; set; }
        public decimal ShipBase { get; set; }
        public decimal ShipRate { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
