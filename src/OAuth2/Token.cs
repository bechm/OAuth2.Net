using System;
using System.Net;
using System.Xml.Linq;
using NNS.Authentication.OAuth2.Exceptions;

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
            var response = (HttpWebResponse) webRequest.GetResponse();
            SetAccessToken(response);
        }

        internal HttpWebRequest GetWebRequestForAccessTokenRequest()
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(this.Server.AuthorizationRequestUri);

            return webRequest;
        }

        private void SetAccessToken(HttpWebResponse response)
        {
            throw new NotImplementedException();
        }
    }
}
