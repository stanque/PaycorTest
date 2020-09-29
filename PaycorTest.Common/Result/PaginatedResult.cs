using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Common.Result
{
    public class PaginatedResult<T>
    {
        public List<T> Result { get; set; }
        public PaginationDataResult PaginationData { get; set; }

        public PaginatedResult<TOut> Remap<TOut>(IMapper mapper)
        {
            PaginatedResult<TOut> result = new PaginatedResult<TOut>
            {
                PaginationData = this.PaginationData,
                Result = mapper.Map<List<TOut>>(this.Result)
            };

            return result;
        }
    }

    public class PaginatedResult<T,TAdditionalData> : PaginatedResult<T>
    {
        public TAdditionalData AdditionalData { get; set; }

        public PaginatedResult<TOut,TAdditionOut> Remap<TOut,TAdditionOut>(IMapper mapper)
        {
            PaginatedResult<TOut, TAdditionOut> result = new PaginatedResult<TOut, TAdditionOut>
            {
                PaginationData = this.PaginationData,
                Result = mapper.Map<List<TOut>>(this.Result),
                AdditionalData = mapper.Map<TAdditionOut>(this.AdditionalData)
            };

            return result;
        }
    }
}
