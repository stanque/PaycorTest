using PaycorTest.Common.Result;
using PaycorTest.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaycorTest.Service.Abstraction
{
    public interface IProductService
    {
        Task<ProductDto> GetProductById(int id, bool addIncludes);
        Task<PaginatedResult<ProductDto>> GetProductsFiltered
        (
            string productName,
            DateTime? sellStartDate,
            PaginationData pagination,
            string[] keywords,
            string culture = "en"
        );
    }
}
