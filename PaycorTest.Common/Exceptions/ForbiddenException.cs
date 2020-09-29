using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Common.Exceptions
{
    public class ForbiddenException : Exception, IHttpResponseException
    {
        public ForbiddenException() : base() { }
        public ForbiddenException(string message) : base(message) { }

        public int HttpResponseCode => 403;
    }
}
