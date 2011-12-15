using System;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class ServerWithAuthorizationCodeAlredyExistsException : Exception
    {
        public String ClientId { get; private set; }
        public Uri AuthorizationRequestUri { get; private set; }
        public Uri AccessTokenRequestUri { get; private set; }
        public Uri RedirectionUri { get; private set; }

        public ServerWithAuthorizationCodeAlredyExistsException(string clientId, Uri authorizationRequestUri,Uri accessTokenRequestUri, Uri redirectionUri)
        {
            ClientId = clientId;
            AuthorizationRequestUri = authorizationRequestUri;
            AccessTokenRequestUri = accessTokenRequestUri;
            RedirectionUri = redirectionUri;
        }
    }
}