using AutoMapper;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PaycorTest.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaycorTest.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public abstract class PaycorTestControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        protected readonly ILog _logger;

        protected PaycorTestControllerBase(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = LogManager.GetLogger(GetType());
        }

        protected void FixPageData(PaginationData paginationData)
        {
            if(paginationData.CurrentPage < 1)
            {
                paginationData.CurrentPage = 1;
            }

            if (paginationData.CurrentPage == 0)
            {
                paginationData.CurrentPage = 1;
            }

            if (paginationData.RowsPerPage == 0)
            {
                paginationData.RowsPerPage = 1;
            }
        }
    }
}
