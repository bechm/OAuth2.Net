using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NNS.Authentication.OAuth2.Exceptions;

namespace NNS.Authentication.OAuth2
{
    public class ServerWithAuthorizationCode : Server
    {
        public Uri RedirectionUri { get; set; }

        internal ServerWithAuthorizationCode(string clientId, string clientSharedSecret, Uri authorizationRequestUri, Uri accessTokenRequestUri, Uri redirectionUri, List<String> scopes)
        {
            ClientId = clientId;
            ClientSharedSecret = clientSharedSecret;
            AuthorizationRequestUri = authorizationRequestUri;
            AccessTokenRequestUri = accessTokenRequestUri;
            RedirectionUri = redirectionUri;
            Version = OAuthVersion.v2_22;

            Scopes = scopes;
            if(scopes == null)
                Scopes = new List<string>();
            
            Guid = Guid.NewGuid();
        }

        internal ServerWithAuthorizationCode(string clientId, string clientSharedSecret, Uri authorizationRequestUri, Uri accessTokenRequestUri, Uri redirectionUri)
        {
            ClientId = clientId;
            ClientSharedSecret = clientSharedSecret;
            AuthorizationRequestUri = authorizationRequestUri;
            AccessTokenRequestUri = accessTokenRequestUri;
            RedirectionUri = redirectionUri;
            Version = OAuthVersion.v2_22;

            Scopes = new List<string>();
            Guid = Guid.NewGuid();
        }

        internal XElement ToXElement()
        {
            var element = new XElement("Server");
            element.Add(new XAttribute("type","AuthorizationCode"));
            element.Add(new XElement("Guid",Guid.ToString()));
            element.Add(new XElement("ClientId",ClientId));
            element.Add(new XElement("ClientSharedSecret",ClientSharedSecret));

            var scopes = new XElement("Scopes");
            foreach(var scope in Scopes)
                scopes.Add(new XElement("Scope", scope));
            element.Add(scopes);

            element.Add(new XElement("AuthorizationUri",AuthorizationRequestUri.ToString()));
            element.Add(new XElement("AccessTokenUri", AccessTokenRequestUri.ToString()));
            element.Add(new XElement("RedirectionUri", RedirectionUri.ToString()));
            return element;
        }

        public static ServerWithAuthorizationCode FromXElement(XElement element)
        {
            if (element.Attribute("type").Value != "AuthorizationCode")
                throw new InvalidTypeException("AuthorizationCode", element.Attribute("type").Value, element);
            var server = new ServerWithAuthorizationCode(
                element.Element("ClientId").Value,
                element.Element("ClientSharedSecret").Value,
                new Uri(element.Element("AuthorizationUri").Value),
                new Uri(element.Element("AccessTokenUri").Value),
                new Uri(element.Element("RedirectionUri").Value));
            if(element.Element("Guid") != null)
                server.Guid = new Guid(element.Element("Guid").Value);

            var scopesElement = element.Element("Scopes");
            if(scopesElement != null)
            {
                foreach(var scopeElement in scopesElement.Elements("Scope"))
                    server.Scopes.Add(scopeElement.Value);
            }

            return server;

        }
    }
}
