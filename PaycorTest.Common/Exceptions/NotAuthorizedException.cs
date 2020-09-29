using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Common.Exceptions
{
    /// <summary>
    /// Although the HTTP standard specifies "unauthorized", semantically this response means "unauthenticated". 
    /// That is, the client must authenticate itself to get the requested response.
    /// </summary>
    public class NotAuthorizedException : Exception, IHttpResponseException
    {
        public NotAuthorizedException() : base() { }
        public NotAuthorizedException(string message) : base(message) { }

        public int HttpResponseCode => 401;
    }
}
