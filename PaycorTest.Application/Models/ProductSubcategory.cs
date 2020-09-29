using System;

namespace PaycorTest.Application.Models
{
    public class ProductSubcategory
    {
        public int ProductSubcategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ProductCategory ProductCategory { get; set; }
    }
}
