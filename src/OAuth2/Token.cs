using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using NNS.Authentication.OAuth2.Exceptions;

namespace NNS.Authentication.OAuth2
{
    public class Token
    {
        internal Server Server { get; private set; }
        internal ResourceOwner ResourceOwner { get; private set; }
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
            return token1.Equals(token2);
        }

        public static bool operator !=(Token token1, Token token2)
        {
            return !(token1 == token2);
        }

        public bool Equals(Token token)
        {
            return !Equals(token, null) && (Server.Guid.Equals(token.Server.Guid) && ResourceOwner.Name.Equals(token.ResourceOwner.Name));
        }

        public override bool Equals(object obj)
        {
            if (object.Equals(this, null) && obj == null)
                return true;
            if (obj == null)
                return false;
            if(obj is Token)
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

            return element;
        }

        public static Token FromXElement(XElement element)
        {
            if (element.Element("Server") == null)
                throw new RequiredElementMissingException("Server", element);
            if (element.Element("ResourceOwner") == null)
                throw new RequiredElementMissingException("ResourceOwner", element);

            var server = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(new Guid(element.Element("Server").Value));
            var resourceOwner = ResourceOwners.GetResourceOwner(element.Element("ResourceOwner").Value);
            var token = new Token(server, resourceOwner);

            if(element.Element("Expires") != null)
                token.Expires = DateTime.Parse(element.Element("Expires").Value);
            if (element.Element("AccessToken") != null)
                token.AccessToken = element.Element("AccessToken").Value;
            if (element.Element("RefreshToken") != null)
                token.RefreshToken = element.Element("RefreshToken").Value;
            if (element.Element("AuthorizationCode") != null)
                token.AuthorizationCode = element.Element("AuthorizationCode").Value;

            return token;
        }
    }
}
