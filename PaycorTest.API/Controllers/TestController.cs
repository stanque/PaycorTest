using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaycorTest.Common.Exceptions;

namespace PaycorTest.API.Controllers
{
    [Route("api/v1/basicTest")]
    [ApiController]
    public class TestController : PaycorTestControllerBase
    {
        public TestController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        {

        }


        /// <summary>
        /// Test endpoint
        /// </summary>
        /// <returns>Test string (not JSON)</returns>
        [HttpGet("test")]
        public async Task<ActionResult<string>> Get()
        {
            _logger.Info("Test request arrived!");
            return await Task.FromResult(Ok("Everything is Fine."));
        }
        /// <summary>
        /// Throws a test BadRequest exception
        /// </summary>
        /// <returns>Exception</returns>
        [HttpGet("testException")]
        public IActionResult GetException()
        {
            var exc = new BadRequestException("Test for bad request");
            _logger.Error("This is a test exception", exc);
            throw exc;
        }

        
    }
}
