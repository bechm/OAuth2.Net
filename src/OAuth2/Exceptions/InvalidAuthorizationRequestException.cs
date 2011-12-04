using System;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class InvalidAuthorizationRequestException : Exception
    {
        public new String Message;

        public InvalidAuthorizationRequestException(string message)
        {
            Message = message;
        }
    }
}