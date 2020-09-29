using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaycorTest.Api.Models;
using PaycorTest.API.Models.Request;
using PaycorTest.Application.Commands;
using PaycorTest.Common.Exceptions;
using PaycorTest.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaycorTest.Common.Extensions;

namespace PaycorTest.API.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : PaycorTestControllerBase
    {

        public ProductController(IMediator mediator, IMapper mapper) 
            : base(mediator, mapper)
        {

        }

        /// <summary>
        /// Get Product by Id
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="addIncludes">Add additional relation data</param>
        /// <returns>Product model</returns>
        [HttpGet("productId/{id:int}/includes/{addIncludes:bool}")]
        public async Task<ActionResult> GetProduct(int id, bool addIncludes)
        {
            var appResult = await _mediator.Send(new GetProductCommand { Id = id, AddIncludes = addIncludes });
            var result = _mapper.Map<ProductViewModel>(appResult);

            return Ok(result);
        }

        /// <summary>
        /// Search for a Product with different search parameters
        /// </summary>
        /// <param name="request">GetFilteredProductsRequest - ProductName: Part of the Product Name for search, SellDateTime: Selling start date,
        /// PaginationData: data for Current Page and number of Rows per Page, Keywords: Words for match in Product Description</param>
        /// <returns>Paged and filtered collection of Products</returns>
        [HttpGet("getFiltered")]
        public async Task<ActionResult<PaginatedResult<ProductViewModel>>> Get([FromQuery] GetFilteredProductsRequest request)
        {
            FixPageData(request.PaginationData);
            var resultApp = await _mediator.Send(_mapper.Map<GetFilteredProductsCommand>(request));
            var result = resultApp.Remap<ProductViewModel>(_mapper);

            return Ok(result);
        }
    }
}
