using System;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class NoAuthorizationCodeException : Exception
    {
        public Server Server;
        public ResourceOwner ResourceOwner;

        public NoAuthorizationCodeException(ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            Server = server;
            ResourceOwner = resourceOwner;
        }
    }
}