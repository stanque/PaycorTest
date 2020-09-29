using AutoMapper;
using MediatR;
using PaycorTest.Application.Models;
using PaycorTest.Common.Result;
using PaycorTest.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaycorTest.Application.Commands
{
    public class GetFilteredProductsCommand : IRequest<PaginatedResult<Product>>
    {
        public string ProductName { get; set; }
        public DateTime? SellDateTime { get; set; }
        public PaginationData PaginationData { get; set; }
        public List<string> Keywords { get; set; }
    }

    public class GetFilteredProductsCommandHandler : BaseMediatorHandler, IRequestHandler<GetFilteredProductsCommand, PaginatedResult<Product>>
    {
        private readonly IProductService _productService;

        public GetFilteredProductsCommandHandler(IProductService productService, IMapper mapper)
            :base(mapper)
        {
            _productService = productService;
        }

        public async Task<PaginatedResult<Product>> Handle(GetFilteredProductsCommand request, CancellationToken cancellationToken)
        {
            var dtos = await _productService.GetProductsFiltered
                            (
                                request.ProductName, 
                                request.SellDateTime, 
                                request.PaginationData, 
                                request.Keywords.ToArray()
                            );

            var paginatedResult = dtos.Remap<Product>(_mapper);
            
            return paginatedResult;
        }
    }
}
