using System;

namespace PaycorTest.Service.ServiceModels
{
    public class ProductSubcategoryDto
    {
        public int ProductSubcategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ProductCategoryDto ProductCategory { get; set; }
    }
}
