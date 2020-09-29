using AutoMapper;
using MediatR;
using PaycorTest.Application.Models.AggregationModels;
using PaycorTest.Common.Result;
using PaycorTest.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaycorTest.Application.Commands
{
    public class GetAggregatedLineTotalAndOrderQtyInTimePeriodCommand 
        : IRequest<PaginatedResult<LineTotalAndOrderQtySumPerDay, LineTotalAndOrderQtySumPerTimePeriod>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PaginationData paginationData { get; set; }
    }

    public class GetAggregatedLineTotalAndOrderQtyInTimePeriodHandler
        : BaseMediatorHandler,
        IRequestHandler<GetAggregatedLineTotalAndOrderQtyInTimePeriodCommand, PaginatedResult<LineTotalAndOrderQtySumPerDay, LineTotalAndOrderQtySumPerTimePeriod>>
    {
        private readonly IPurchaseOrderDetailService _purchaseOrderDetailService;

        public GetAggregatedLineTotalAndOrderQtyInTimePeriodHandler(IPurchaseOrderDetailService purchaseOrderDetailService, IMapper mapper) 
            : base(mapper)
        {
            _purchaseOrderDetailService = purchaseOrderDetailService;
        }

        public async Task<PaginatedResult<LineTotalAndOrderQtySumPerDay, LineTotalAndOrderQtySumPerTimePeriod>> Handle
        (
            GetAggregatedLineTotalAndOrderQtyInTimePeriodCommand request, 
            CancellationToken cancellationToken
        )
        {
            var dtoResult = await _purchaseOrderDetailService.GetLineTotalAndOrderQuantitySumPerDay
            (
                request.StartDate,
                request.EndDate,
                request.paginationData
            );

            var result = dtoResult.Remap<LineTotalAndOrderQtySumPerDay, LineTotalAndOrderQtySumPerTimePeriod>(_mapper);

            return result;
        }
    }
}
