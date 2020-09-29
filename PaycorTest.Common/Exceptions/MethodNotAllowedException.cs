using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Common.Exceptions
{
    public class MethodNotAllowedException : Exception, IHttpResponseException
    {
        public MethodNotAllowedException() : base() { }
        public MethodNotAllowedException(string message) : base(message) { }

        public int HttpResponseCode => 405;
    }
}
