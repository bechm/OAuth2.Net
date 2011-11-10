using System;
using System.Net;

namespace NNS.Authentication.OAuth2.Extensions
{
    public static class WebRequestExtensions
    {
        public static void AuthenticationRedirect(this WebRequest webRequest, Server server, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }

        public static void SignRequest(this WebRequest webRequest, Server server, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }
    }
}
