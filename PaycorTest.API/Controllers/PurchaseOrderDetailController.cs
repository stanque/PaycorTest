using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaycorTest.Api.Models;
using PaycorTest.API.Models.AggregationModels;
using PaycorTest.API.Models.Request;
using PaycorTest.Application.Commands;
using PaycorTest.Common.Exceptions;
using PaycorTest.Common.Result;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaycorTest.Common.Extensions;

namespace PaycorTest.API.Controllers
{
    [Route("api/v1/purchaseOrderDetail")]
    [ApiController]
    public class PurchaseOrderDetailController : PaycorTestControllerBase
    {
        public PurchaseOrderDetailController(IMediator mediator, IMapper mapper) 
            : base(mediator, mapper)
        {

        }


        /// <summary>
        /// Get PurchaseOrderDetail by purchaseOrderId and purchaseOrderDetailId
        /// </summary>
        /// <param name="purchaseOrderId">purchaseOrderId</param>
        /// <param name="purchaseOrderDetailId">purchaseOrderDetailId</param>
        /// <param name="addIncludes">Add additional relational data</param>
        /// <returns>PurchaseOrderDetail model</returns>
        [HttpGet("purchaseOrderId/{purchaseOrderId:int}/purchaseOrderDetailId/{purchaseOrderDetailId:int}/addIncludes/{addIncludes:bool}")]
        public async Task<ActionResult<PurchaseOrderDetailViewModel>> Get(int purchaseOrderId, int purchaseOrderDetailId, bool addIncludes)
        {
            var dto = await _mediator.Send
            (
                new GetPurchaseOrderDetailCommand
                {
                    PurchaseOrderId = purchaseOrderId,
                    PurchaseOrderDetailId = purchaseOrderDetailId,
                    AddIncludes = addIncludes
                }
            );

            var result = _mapper.Map<PurchaseOrderDetailViewModel>(dto);

            return Ok(result);
        }

        /// <summary>
        /// Obtain sum of Line Total and Order Quantity in specified time period grouped per day
        /// </summary>
        /// <param name="request">GetAggregatedLineTotalAndOrderQtyInTimePeriodRequest - StartDate: beggining of specified time period, 
        /// EndDate: end of specified time period, 
        /// PaginationData: defines current page and number of rows per page</param>
        /// <returns>Paged aggregation data</returns>
        [HttpGet("getLineTotalAndOrderQtySumPerDay")]
        public async Task<ActionResult<PaginatedResult<LineTotalAndOrderQtySumPerDayViewModel, LineTotalAndOrderQtySumPerTimePeriodViewModel>>> 
            GetLineTotalAndOrderQtySumPerDay
        (
            [FromQuery]GetAggregatedLineTotalAndOrderQtyInTimePeriodRequest request
        )
        {
            FixPageData(request.PaginationData);
            if (request.StartDate > request.EndDate)
            {
                throw new BadRequestException("Start and end dates don't define a valid time period");
            }

            var appResult = await _mediator.Send(_mapper.Map<GetAggregatedLineTotalAndOrderQtyInTimePeriodCommand>(request));

            var result = appResult.Remap<LineTotalAndOrderQtySumPerDayViewModel, LineTotalAndOrderQtySumPerTimePeriodViewModel>(_mapper);

            return Ok(result);
        }
    }

    
}
