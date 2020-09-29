using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore.Internal;
using PaycorTest.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaycorTest.Application.Commands
{
    public abstract class BaseMediatorHandler
    {
        protected readonly IMapper _mapper;
        protected readonly ILog _logger;

        public BaseMediatorHandler(IMapper mapper)
        {
            _logger = LogManager.GetLogger(GetType());
            _mapper = mapper;
        }

        protected T FireNotFoundExceptionOnEmptyResult<T>(T result) where T:class
        {
            if(result == null)
            {
                throw new NotFoundException("No results were found.");
            }

            return result;
        }

        protected IEnumerable<T> FireNotFoundExceptionOnEmptyResult<T>(IEnumerable<T> results) where T : class
        {
            if (results == null || !results.Any())
            {
                throw new NotFoundException("No results were found.");
            }

            return results;
        }
    }
}
