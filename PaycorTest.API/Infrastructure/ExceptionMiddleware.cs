using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using PaycorTest.Common.Exceptions;
using PaycorTest.API.Models;

namespace PaycorTest.API.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetLogger(this.GetType());
                logger.Error($"Exception: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var exceptionMessage = exception is AggregateException aggregateException ? aggregateException.Flatten().Message : exception.Message;

            IHttpResponseException exceptionWithStatusCode = exception as IHttpResponseException;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = exceptionWithStatusCode != null ?
                                exceptionWithStatusCode.HttpResponseCode :
                                context.Response.StatusCode,
                Message = exceptionMessage
            }.ToString());
        }
    }
}
