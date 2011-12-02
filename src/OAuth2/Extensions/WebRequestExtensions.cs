using System;
using System.Net;
using System.ServiceModel.Web;

namespace NNS.Authentication.OAuth2.Extensions
{
    public static class WebRequestExtensions
    {
        public static void RedirectToAuthorization(this IWebOperationContext context, Server server, Uri redirectionUri, ResourceOwner resourceOwner)
        {

        }
        
        public static void RedirectToAuthorization(this IWebOperationContext context, ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            context.RedirectToAuthorization(server,server.RedirectionUri,resourceOwner);
        }

        public static void AuthenticationRedirect(this HttpWebRequest webRequest, Server server, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }

        public static void SignRequest(this HttpWebRequest webRequest, Server server, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }
    }
}
