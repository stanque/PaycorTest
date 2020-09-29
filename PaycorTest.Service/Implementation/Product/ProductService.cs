using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NinjaNye.SearchExtensions;
using PaycorTest.Common.Result;
using PaycorTest.Data;
using PaycorTest.Data.Entities;
using PaycorTest.Service.Abstraction;
using PaycorTest.Service.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using DataModels = PaycorTest.Data.Entities;

namespace PaycorTest.Service.Implementation.Product
{
    public sealed class ProductService : BaseService, IProductService
    {
        private readonly IRepository<int, DataModels.Product> _productRepository;
        private readonly IRepository<int, ProductModel> _productModelRepository;
        private readonly IRepository<int, int, string, ProductModelProductDescriptionCulture> _productModelProductDescriptionCultureRepository;
        private readonly IRepository<string, Culture> _cultureRepository;
        private readonly IRepository<int, ProductDescription> _productDescriptionRepository;

        private static readonly Func<IQueryable<DataModels.Product>, IIncludableQueryable<DataModels.Product, object>> _includes =
            source =>
                source.Include(x => x.ProductModel)
                .Include(x => x.ProductSubcategory)
                .ThenInclude(x => x.ProductCategory)
                .Include(x => x.SizeUnitMeasure)
                .Include(x => x.WeightUnitMeasure);
                

        public ProductService
        (
            IRepository<int, DataModels.Product> repo,
            IRepository<int, ProductModel> productModelRepository,
            IRepository<int, int, string, ProductModelProductDescriptionCulture> productModelProductDescriptionCultureRepository,
            IRepository<string, Culture> cultureRepository,
            IRepository<int, ProductDescription> productDescriptionRepository,
            IMapper mapper
        ) 
            : base(mapper)
        {
            _productRepository = repo;
            _productModelRepository = productModelRepository;
            _productModelProductDescriptionCultureRepository = productModelProductDescriptionCultureRepository;
            _cultureRepository = cultureRepository;
            _productDescriptionRepository = productDescriptionRepository;
        }

        public async Task<ProductDto> GetProductById(int id, bool addIncludes)
        {
            ProductDto result = null;

            DataModels.Product resultDb = null;
            if(addIncludes)
            {
                resultDb = await _productRepository.All(_includes).FirstOrDefaultAsync(x => x.ProductID == id);
            }
            else
            {
                resultDb = await _productRepository.FindAsync(id);
            }

            result = _mapper.Map<ProductDto>(resultDb);

            return result;
        }

        public async Task<PaginatedResult<ProductDto>> GetProductsFiltered
        (
            string productName, 
            DateTime? sellStartDate, 
            PaginationData pagination,
            string[] keywords,
            string culture = "en"
        )
        {
            IQueryable<DataModels.Product> resultsDb = _productRepository.All();

            if(!string.IsNullOrWhiteSpace(productName))
            {
                resultsDb = resultsDb.Where(x => x.Name.ToLower().Contains(productName.ToLower()));
            }

            if(sellStartDate.HasValue)
            {
                var searchDate = sellStartDate.Value.Date;
                resultsDb = resultsDb.Where(x => x.SellStartDate.Date == searchDate);
            }

            IQueryable<ProductDescProjection> product_productDescription = null;
            if (keywords.Any())
            {
                var descriptionsWithKeyWords = _productDescriptionRepository.All()
                                                .Search
                                                (
                                                    x => x.Description.ToLower()
                                                ).Containing(keywords.Select(kw => kw.ToLower()).ToArray());

                product_productDescription =
                    resultsDb
                    .Join
                    (
                        _productModelProductDescriptionCultureRepository.All().Where(z => z.CultureID.ToLower().Trim() == "en"),
                        prod => prod.ProductModelID,
                        descLink => descLink.ProductModelID,
                        (prod, descLink) => new { Product = prod, Link = descLink }
                    ).Join
                    (
                        descriptionsWithKeyWords,
                        prod_prodLink => prod_prodLink.Link.ProductDescriptionID,
                        desc => desc.ProductDescriptionID,
                        (prod_prodLink, desc) => new ProductDescProjection { Product = prod_prodLink.Product, Description = desc.Description }
                    );

                resultsDb = product_productDescription.Select(x => x.Product);
            }

            var result = await Paginate
            (
                (EntityQueryable<DataModels.Product>)resultsDb, 
                pagination, 
                x => x.ProductID
            );

            if (product_productDescription != null)
            {
                await EnrichResultWithDescription(result, product_productDescription);
            }

            return new PaginatedResult<ProductDto>()
            {
                PaginationData = result.PaginationData,
                Result = _mapper.Map<List<ProductDto>>(result.Result)
            };
        }

        private async Task EnrichResultWithDescription
        (
            PaginatedResult<DataModels.Product> result,
            IQueryable<ProductDescProjection> productsWithDescriptions
        )
        {
            var resultIds = result.Result.Select(x => x.ProductID);
            var prodIdsWithDescriptions = await productsWithDescriptions
            .Where
            (
                x => resultIds.Contains(x.Product.ProductID)
            )
            .Select(x => new { ProductID = x.Product.ProductID, Description = x.Description})
            .ToListAsync();

            result.Result = result.Result.Join
            (
                prodIdsWithDescriptions,
                x => x.ProductID,
                y => y.ProductID,
                (x, y) =>
                {
                    x.ProductDescription = y.Description;
                    return x;
                }
            ).ToList();
        }

        private class ProductDescProjection
        {
            public DataModels.Product Product { get; set; }
            public string Description { get; set; }
        }
    }
}
