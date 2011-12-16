using System;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using NNS.Authentication.OAuth2.Exceptions;

namespace NNS.Authentication.OAuth2.Extensions
{
    public static class WebRequestExtensions
    {
        public static void RedirectToAuthorization(this OutgoingWebResponseContext outgoingResponse, ServerWithAuthorizationCode server, Uri redirectionUri, ResourceOwner resourceOwner)
        {
            outgoingResponse.StatusCode = HttpStatusCode.Redirect;
            SetRedirectUriInToken(server, resourceOwner, redirectionUri);
            outgoingResponse.Location = GetAuthorizationLocation(server, redirectionUri, resourceOwner);
        }

        public static void RedirectToAuthorization(this IOutgoingWebResponseContext outgoingResponse, ServerWithAuthorizationCode server, Uri redirectionUri, ResourceOwner resourceOwner)
        {

            outgoingResponse.StatusCode = HttpStatusCode.Redirect;
            SetRedirectUriInToken(server, resourceOwner, redirectionUri);
            outgoingResponse.Location = GetAuthorizationLocation(server, redirectionUri, resourceOwner);
        }

        private static void SetRedirectUriInToken(ServerWithAuthorizationCode server, ResourceOwner resourceOwner, Uri redirectionUri)
        {
            var token = Tokens.GetToken(server, resourceOwner);
            token.RedirectUri = redirectionUri;
        }

        private static string GetAuthorizationLocation(ServerWithAuthorizationCode server, Uri redirectionUri, ResourceOwner resourceOwner)
        {
            var scopes = server.Scopes.Aggregate("", (current, scope) => current + (scope + " ")).Trim();

            return server.AuthorizationRequestUri + "?response_type=code&client_id=" +
                   server.ClientId +
                   "&state=" + server.Guid + "_" + resourceOwner.Guid +
                   "&scope=" + HttpUtility.UrlEncode(scopes) +
                   "&redirect_uri=" + HttpUtility.UrlEncode(redirectionUri.ToString());
        }

        public static void RedirectToAuthorization(this OutgoingWebResponseContext outgoingResponse, ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            outgoingResponse.RedirectToAuthorization(server, server.RedirectionUri, resourceOwner);
        }

        public static void RedirectToAuthorization(this IOutgoingWebResponseContext outgoingResponse, ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            outgoingResponse.RedirectToAuthorization(server, server.RedirectionUri, resourceOwner);
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

        internal static void SetBasicAuthenticationFor(this HttpWebRequest webRequest, Server server)
        {
            var authInfo = server.ClientId + ":" + server.ClientSharedSecret;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            webRequest.Headers["Authorization"] = "Basic " + authInfo;
        }
        
    }
}
