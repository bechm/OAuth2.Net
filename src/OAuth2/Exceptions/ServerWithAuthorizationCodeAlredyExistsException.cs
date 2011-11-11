using System;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class ServerWithAuthorizationCodeAlredyExistsException : Exception
    {
        public String ClientId { get; private set; }
        public Uri AuthorizationRequestUri { get; private set; }
        public Uri RedirectionUri { get; private set; }

        public ServerWithAuthorizationCodeAlredyExistsException(string clientId, Uri authorizationRequestUri, Uri redirectionUri)
        {
            ClientId = clientId;
            AuthorizationRequestUri = authorizationRequestUri;
            RedirectionUri = redirectionUri;
        }
    }
}