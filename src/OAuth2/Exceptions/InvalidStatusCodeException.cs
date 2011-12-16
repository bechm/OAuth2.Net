using System;
using System.Net;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class InvalidStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode;
        public HttpWebResponse Response;
        public String ResponseText;

        public InvalidStatusCodeException(HttpStatusCode statusCode, HttpWebResponse response, String responseText)
        {
            StatusCode = statusCode;
            Response = response;
            ResponseText = responseText;
        }
    }
}