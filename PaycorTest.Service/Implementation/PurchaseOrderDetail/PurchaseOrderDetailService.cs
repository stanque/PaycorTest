using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PaycorTest.Common.Result;
using PaycorTest.Data;
using PaycorTest.Service.Abstraction;
using PaycorTest.Service.ServiceModels;
using PaycorTest.Service.ServiceModels.AggregationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataModels = PaycorTest.Data.Entities;

namespace PaycorTest.Service.Implementation.PurchaseOrderDetail
{
    public class PurchaseOrderDetailService : BaseService, IPurchaseOrderDetailService
    {
        private readonly IRepository<int, int, DataModels.PurchaseOrderDetail> _purchaseOrderDetailRepository;

        private static readonly Func<IQueryable<DataModels.PurchaseOrderDetail>, IIncludableQueryable<DataModels.PurchaseOrderDetail, object>> _includes =
            source =>
                source.Include(x => x.PurchaseOrder).ThenInclude(x => x.Vendor)
                .Include(x => x.PurchaseOrder).ThenInclude(x => x.ShipMethod)
                .Include(x => x.Product).ThenInclude(x => x.WeightUnitMeasure)
                .Include(x => x.Product).ThenInclude(x => x.ProductModel)
                .Include(x => x.Product).ThenInclude(x => x.SizeUnitMeasure)
                .Include(x => x.Product).ThenInclude(x => x.ProductSubcategory).ThenInclude(x => x.ProductCategory);

        public PurchaseOrderDetailService(IRepository<int, int, DataModels.PurchaseOrderDetail> purchaseOrderDetailRepository, IMapper mapper) 
            : base(mapper)
        {
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
        }

        public async Task<PurchaseOrderDetailDto> GetPurchaseOrderDetail(int purchaseOrderID, int purchaseOrderDetailID, bool addIncludes)
        {
            DataModels.PurchaseOrderDetail resultDb = null;
            PurchaseOrderDetailDto result = null;

            if (addIncludes)
            {
                resultDb = await _purchaseOrderDetailRepository.All(_includes)
                                    .FirstOrDefaultAsync
                                    (
                                        x => x.PurchaseOrderID == purchaseOrderID &&
                                        x.PurchaseOrderDetailID == purchaseOrderDetailID
                                    );
            }
            else
            {
                resultDb = await _purchaseOrderDetailRepository.FindAsync(purchaseOrderID, purchaseOrderDetailID);
            }

            result = _mapper.Map<PurchaseOrderDetailDto>(resultDb);

            return result;
        }

        public async Task<PaginatedResult<LineTotalAndOrderQtySumPerDayDto, LineTotalAndOrderQtySumPerTimePeriodDto>> GetLineTotalAndOrderQuantitySumPerDay
        (
            DateTime startDate, 
            DateTime endDate, 
            PaginationData paginationData
        )
        {
            var startData = _purchaseOrderDetailRepository.All();

            var dataInDateRange =   startData
                                    .Where
                                    (
                                        x => x.DueDate >= startDate.Date &&
                                        x.DueDate <= endDate.Date
                                    ).Select(x => new { x.DueDate, x.LineTotal, x.OrderQty });

            var groupedByDayWithSum = dataInDateRange.GroupBy(x => x.DueDate)
                                        .Select
                                        (
                                            grp =>
                                            new LineTotalAndOrderQtySumPerDayDto
                                            {
                                                Date = grp.Key,
                                                LineTotalSum = grp.Sum(a => a.LineTotal) ?? 0,
                                                OrderQtySum = grp.Sum(b => b.OrderQty)
                                            }
                                        );

            var paginated = await Paginate(groupedByDayWithSum, paginationData, x => x.Date);

            var result = new PaginatedResult<LineTotalAndOrderQtySumPerDayDto, LineTotalAndOrderQtySumPerTimePeriodDto>
            {
                PaginationData = paginated.PaginationData,
                Result = paginated.Result,
                AdditionalData = new LineTotalAndOrderQtySumPerTimePeriodDto()
                {
                    LineTotalSumPerPeriod = paginated.Result.Sum(x => x.LineTotalSum),
                    OrderQtySumPerPeriod = paginated.Result.Sum(x => x.OrderQtySum)
                }  
            };

            return result;
        }
    }
}
