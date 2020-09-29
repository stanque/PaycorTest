using AutoMapper;
using MediatR;
using PaycorTest.Application.Models;
using PaycorTest.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaycorTest.Application.Commands
{
    public class GetProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public bool AddIncludes { get; set; }
    }

    public class GetProductCommandHandler : BaseMediatorHandler, IRequestHandler<GetProductCommand, Product>
    {
        private readonly IProductService _productService;

        public GetProductCommandHandler(IProductService productSrc, IMapper mapper) : base(mapper)
        {
            _productService = productSrc;
        }


        public async Task<Product> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var dto = FireNotFoundExceptionOnEmptyResult
            (
                await _productService.GetProductById(request.Id, request.AddIncludes)
            );
            var result = _mapper.Map<Product>(dto);

            return result;
        }
    }
}
