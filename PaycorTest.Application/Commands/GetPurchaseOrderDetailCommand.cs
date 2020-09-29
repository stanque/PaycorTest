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
    public class GetPurchaseOrderDetailCommand : IRequest<PurchaseOrderDetail>
    {
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public bool AddIncludes { get; set; }
    }

    public class GetPurchaseOrderDetailCommandHandler : BaseMediatorHandler, IRequestHandler<GetPurchaseOrderDetailCommand, PurchaseOrderDetail>
    {
        private readonly IPurchaseOrderDetailService _purchaseOrderDetailService;

        public GetPurchaseOrderDetailCommandHandler(IPurchaseOrderDetailService purchaseOrderDetailService, IMapper mapper) : base(mapper)
        {
            _purchaseOrderDetailService = purchaseOrderDetailService;
        }

        public async Task<PurchaseOrderDetail> Handle(GetPurchaseOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var dto = FireNotFoundExceptionOnEmptyResult
            (
                await _purchaseOrderDetailService.GetPurchaseOrderDetail
                (
                    request.PurchaseOrderId,
                    request.PurchaseOrderDetailId,
                    request.AddIncludes
                )
            );

            var result = _mapper.Map<PurchaseOrderDetail>(dto);

            return result;
        }
    }
}
