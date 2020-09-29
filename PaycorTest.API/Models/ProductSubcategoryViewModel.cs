using System;

namespace PaycorTest.Api.Models
{
    public class ProductSubcategoryViewModel
    {
        public int ProductSubcategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ProductCategoryViewModel ProductCategory { get; set; }
    }
}
