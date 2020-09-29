using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Common.Exceptions
{
    public interface IHttpResponseException
    {
        int HttpResponseCode { get; }
    }
}
