using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Common.Exceptions
{
    public class NotFoundException : Exception, IHttpResponseException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }

        public int HttpResponseCode => 404;
    }
}
