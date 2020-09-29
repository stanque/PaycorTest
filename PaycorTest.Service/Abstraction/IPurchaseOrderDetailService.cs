using PaycorTest.Common.Result;
using PaycorTest.Service.ServiceModels;
using PaycorTest.Service.ServiceModels.AggregationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaycorTest.Service.Abstraction
{
    public interface IPurchaseOrderDetailService
    {
        Task<PurchaseOrderDetailDto> GetPurchaseOrderDetail(int purchaseOrderID, int purchaseOrderDetailID, bool addIncludes);
        Task<PaginatedResult<LineTotalAndOrderQtySumPerDayDto, LineTotalAndOrderQtySumPerTimePeriodDto>> GetLineTotalAndOrderQuantitySumPerDay
        (
            DateTime startDate,
            DateTime endDate,
            PaginationData paginationData
        );
    }
}
