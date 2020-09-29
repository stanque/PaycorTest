using System;

namespace PaycorTest.Service.ServiceModels
{
    public class ProductCategoryDto
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
