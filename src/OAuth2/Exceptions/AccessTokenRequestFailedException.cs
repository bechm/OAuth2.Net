using System;
using System.Net;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class AccessTokenRequestFailedException : Exception
    {
        public String Info;
        public HttpWebResponse Response;
        public Exception InnerExcetion;

        public AccessTokenRequestFailedException(string info, HttpWebResponse response)
        {
            Info = info;
            Response = response;
        }

        public AccessTokenRequestFailedException(string info, HttpWebResponse response, Newtonsoft.Json.JsonReaderException ex)
        {
            Info = info;
            Response = response;
            InnerExcetion = ex;
        }
    }
}