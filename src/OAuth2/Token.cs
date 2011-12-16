using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Xml.Linq;
using NNS.Authentication.OAuth2.Exceptions;
using NNS.Authentication.OAuth2.Extensions;
using Newtonsoft.Json;

namespace NNS.Authentication.OAuth2
{
    internal class Token
    {
        internal Server Server { get; private set; }
        internal ResourceOwner ResourceOwner { get; private set; }
        internal Uri RedirectUri { get; set; }
        internal String AuthorizationCode;
        internal String AccessToken;
        internal String RefreshToken;
        internal DateTime Expires;


        internal Token(Server server, ResourceOwner resourceOwner)
        {
            Server = server;
            ResourceOwner = resourceOwner;
        }

        public static Boolean operator ==(Token token1, Token token2)
        {
            var token1IsNull = object.ReferenceEquals(token1, null);
            var token2IsNull = object.ReferenceEquals(token2, null);
            if (token1IsNull && token2IsNull)
                return true;
            if (token1IsNull || token2IsNull)
                return false;
            return token1.Equals(token2);
        }

        public static bool operator !=(Token token1, Token token2)
        {
            return !(token1 == token2);
        }

        public bool Equals(Token token)
        {
            return !Equals(token, null) &&
                   (Server.Guid.Equals(token.Server.Guid) && ResourceOwner.Name.Equals(token.ResourceOwner.Name));
        }

        public override bool Equals(object obj)
        {
            var token1IsNull = object.ReferenceEquals(this, null);
            var token2IsNull = object.ReferenceEquals(obj, null);
            if (token1IsNull && token2IsNull)
                return true;
            if (token1IsNull || token2IsNull)
                return false;
            if (obj is Token)
                return Equals((Token) obj);
            return false;
        }

        public XElement ToXElement()
        {
            var element = new XElement("Token");
            element.Add(new XElement("Server", Server.Guid));
            element.Add(new XElement("ResourceOwner", ResourceOwner.Name));
            element.Add(new XElement("AuthorizationCode", AuthorizationCode));
            element.Add(new XElement("AccessToken", AccessToken));
            element.Add(new XElement("RefreshToken", RefreshToken));
            element.Add(new XElement("Expires", Expires.ToString()));
            if (RedirectUri != null)
                element.Add(new XElement("RedirectUri", RedirectUri.ToString()));

            return element;
        }

        public static Token FromXElement(XElement element)
        {
            if (element.Element("Server") == null)
                throw new RequiredElementMissingException("Server", element);
            if (element.Element("ResourceOwner") == null)
                throw new RequiredElementMissingException("ResourceOwner", element);

            var server =
                ServersWithAuthorizationCode.GetServerWithAuthorizationCode(new Guid(element.Element("Server").Value));
            var resourceOwner = ResourceOwners.GetResourceOwner(element.Element("ResourceOwner").Value);
            var token = new Token(server, resourceOwner);

            if (element.Element("Expires") != null)
                token.Expires = DateTime.Parse(element.Element("Expires").Value);
            if (element.Element("AccessToken") != null)
                token.AccessToken = element.Element("AccessToken").Value;
            if (element.Element("RefreshToken") != null)
                token.RefreshToken = element.Element("RefreshToken").Value;
            if (element.Element("AuthorizationCode") != null)
                token.AuthorizationCode = element.Element("AuthorizationCode").Value;
            if (element.Element("RedirectUri") != null)
                token.RedirectUri = new Uri(element.Element("RedirectUri").Value);

            return token;
        }

        internal void GetAccessAndRefreshToken()
        {
            var webRequest = GetWebRequestForAccessTokenRequest();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                if (null == response)
                {
                    throw;
                }
            }
            SetAccessToken(response);
        }

        internal HttpWebRequest GetWebRequestForAccessTokenRequest()
        {
            var requestUri = Server.AccessTokenRequestUri;
            
            var webRequest = (HttpWebRequest) WebRequest.Create(requestUri);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            webRequest.SetBasicAuthenticationFor(Server);

            var text = "grant_type=authorization_code";

            if (Server.Version == Server.OAuthVersion.v2_12 || Server.Version == Server.OAuthVersion.v2_16) //Ab der 17 wird BasicAuth verwendet
                text += "&client_id=" + Server.ClientId
                        + "&client_secret=" + Server.ClientSharedSecret;
            text += "&code=" + AuthorizationCode +
                    "&redirect_uri=" + HttpUtility.UrlEncode(RedirectUri.ToString());


            var enc = new System.Text.UTF8Encoding();
            var buffer = enc.GetBytes(text);
            var requestStream = webRequest.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            return webRequest;
        }

        private void SetAccessToken(HttpWebResponse response)
        {
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);
            var responseText = reader.ReadToEnd();
            
            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidStatusCodeException(response.StatusCode, response, responseText);

            if (responseText == "")
                throw new AccessTokenRequestFailedException("empty responseStream", response);

            var values = GetValuesFromResponse(response, responseText);

            GetAccessToken(response, values);
            GetExpires(response, values);
            GetRefreshToken(values);
        }

        private void GetRefreshToken(Dictionary<string, string> values)
        {
            if (values.ContainsKey("refresh_token"))
                RefreshToken = values["refresh_token"];
        }

        private void GetAccessToken(HttpWebResponse response, Dictionary<string, string> values)
        {
            if (!values.ContainsKey("access_token"))
                throw new AccessTokenRequestFailedException("access_token is missing in responseStream", response);
            AccessToken = values["access_token"];
        }

        private void GetExpires(HttpWebResponse response, Dictionary<string, string> values)
        {
            if (Server.Version == Server.OAuthVersion.v2_12)
            {
                Expires = !values.ContainsKey("expires") ? new DateTime(2099, 12, 31, 23, 59, 59) : DateTime.Now.AddSeconds(int.Parse(values["expires"]));
            }
            else
            {
                Expires = !values.ContainsKey("expires_in") ? new DateTime(2099,12,31,23,59,59) : DateTime.Now.AddSeconds(int.Parse(values["expires_in"]));
            }
            
        }

        private Dictionary<string, string> GetValuesFromResponse(HttpWebResponse response, string responseText)
        {
            Dictionary<string, string> values;
            if (Server.Version == Server.OAuthVersion.v2_12)
            {
                try
                {


                    var pairs = responseText.Split('&');
                    values = new Dictionary<string, string>();
                    foreach (var pair in pairs)
                    {
                        var keyvaluepair = pair.Split('=');
                        values.Add(keyvaluepair[0], keyvaluepair[1]);
                    }
                }
                catch (Exception ex)
                {
                    throw new AccessTokenRequestFailedException("no valid String in Response", response, ex);
                }

            }
            else
            {
                
                try
                {
                    values = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);
                }
                catch (JsonReaderException ex)
                {
                    throw new AccessTokenRequestFailedException("no JSON in Response", response, ex);
                }
                
            }
            return values;
        }
    }
}
