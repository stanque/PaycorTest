﻿using System;

namespace PaycorTest.Application.Models
{
    public class Vendor
    {
        public int BusinessEntityID { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public byte CreditRating { get; set; }
        public bool PreferredVendorStatus { get; set; }
        public bool ActiveFlag { get; set; }
        public string PurchasingWebServiceURL { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
