using System;
using System.Net;
using System.ServiceModel.Web;

namespace NNS.Authentication.OAuth2.Extensions
{
    public static class WebRequestExtensions
    {
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
