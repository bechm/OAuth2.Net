using System;
using System.Net;
using System.Security.Authentication;
using System.ServiceModel.Web;
using NNS.Authentication.OAuth2.Exceptions;

namespace NNS.Authentication.OAuth2.Extensions
{
    public static class WebRequestExtensions
    {
        public static void RedirectToAuthorization(this IWebOperationContext context, Server server, Uri redirectionUri, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }
        
        public static void RedirectToAuthorization(this IWebOperationContext context, ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            context.RedirectToAuthorization(server,server.RedirectionUri,resourceOwner);
        }

        public static Tuple<ServerWithAuthorizationCode,ResourceOwner> GetCredentialsFromAuthorizationRedirect(this IIncomingWebRequestContext incomingWebRequestContext)
        {
            var code = incomingWebRequestContext.UriTemplateMatch.QueryParameters.Get("code");
            var state = incomingWebRequestContext.UriTemplateMatch.QueryParameters.Get("state");

            if (string.IsNullOrEmpty(code))
                throw new InvalidAuthorizationRequestException("the query parameters 'code' is not set.");

            if (string.IsNullOrEmpty(state))
                throw new InvalidAuthorizationRequestException("the query parameters 'state' is not set.");

            if(!state.Contains("_"))
                throw new InvalidAuthorizationRequestException("the query parameters 'state' must be of type '<GUID of Server>_<GUID of ResourceOwner>'");
            var states = state.Split('_');

            var server = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(new Guid(states[0]));
            var resourceOwner = ResourceOwners.GetResourceOwner(new Guid(states[1]));

            var token = Tokens.GetToken(server, resourceOwner);
            token.AuthorizationCode = code;

            return new Tuple<ServerWithAuthorizationCode, ResourceOwner>(server,resourceOwner);
        }

        public static void SignRequest(this HttpWebRequest webRequest, Server server, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }
    }
}
