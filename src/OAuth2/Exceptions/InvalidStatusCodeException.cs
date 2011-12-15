using System;
using System.Net;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class InvalidStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode;
        public HttpWebResponse Response;

        public InvalidStatusCodeException(HttpStatusCode statusCode, HttpWebResponse response)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}