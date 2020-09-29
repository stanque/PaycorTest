using System;

namespace PaycorTest.Api.Models
{
    public class ProductCategoryViewModel
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
