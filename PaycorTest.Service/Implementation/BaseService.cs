using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PaycorTest.Common.Exceptions;
using PaycorTest.Common.Result;
using PaycorTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaycorTest.Service.Implementation
{
    public abstract class BaseService
    {
        protected readonly ILog _logger;
        protected readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            _logger = LogManager.GetLogger(GetType());
            _mapper = mapper;
        }

        protected async Task<PaginatedResult<T>> Paginate<T>
        (
            IQueryable<T> data, 
            PaginationData paginationData,
            params Expression<Func<T, object>>[] keys
        )
        {
            var skipRows = (paginationData.CurrentPage - 1) * paginationData.RowsPerPage;
            
            IOrderedQueryable<T> orderedData = null;
            for(int i = 0; i < keys.Count(); ++i)
            {
                if(i == 0)
                {
                    orderedData = data.OrderBy(keys[i]);
                }
                else
                {
                    orderedData = orderedData.ThenBy(keys[i]);
                }
            }

            var paginated = orderedData.Skip(skipRows).Take(paginationData.RowsPerPage);

            var result = new PaginatedResult<T>
            {
                Result = await paginated.ToListAsync(),
                PaginationData = new PaginationDataResult()
            };

            var dataCount = await orderedData.CountAsync();
            var pages = dataCount % paginationData.RowsPerPage > 0 ?
                                    (dataCount / paginationData.RowsPerPage) + 1 :
                                    dataCount / paginationData.RowsPerPage;

            result.PaginationData.PageCount = pages;
            result.PaginationData.CurrentPage = paginationData.CurrentPage;
            result.PaginationData.RowsPerPage = paginationData.RowsPerPage;
            result.PaginationData.RowCount = dataCount;

            if (result.PaginationData.CurrentPage > result.PaginationData.PageCount && result.PaginationData.PageCount != 0)
            {
                result.PaginationData.CurrentPage = result.PaginationData.PageCount;
            }

            return result;
        }
    }
}
