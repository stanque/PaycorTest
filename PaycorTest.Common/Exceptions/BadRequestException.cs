using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Common.Exceptions
{
    /// <summary>
    /// Exception class for signalling HTTP 400 response
    /// </summary>
    public class BadRequestException : Exception, IHttpResponseException
    {
        public BadRequestException() : base("The request was not understood by the server.") { }
        public BadRequestException(string message) : base(message) { }

        public int HttpResponseCode => 400;
    }
}
