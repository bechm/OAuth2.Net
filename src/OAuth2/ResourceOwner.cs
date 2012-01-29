using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using NNS.Authentication.OAuth2.Exceptions;

namespace NNS.Authentication.OAuth2
{
    public class ResourceOwner
    {
        public String Name { get; private set; }
        public Guid Guid { get; private set; }

        internal ResourceOwner(String name)
        {
            Name = name;
            Guid = Guid.NewGuid();
        }

        internal static ResourceOwner FromXElement(XElement element)
        {
            var resourceOwner = new ResourceOwner(element.Element("name").Value);
            if(element.Element("guid") != null)
                resourceOwner.Guid = Guid.Parse(element.Element("guid").Value);
            return resourceOwner;
        }

        internal XElement ToXElement()
        {
            return new XElement("ResourceOwner", 
                new XElement("name", Name), 
                new XElement("guid", Guid));
        }

        public Boolean AuthorizesMeToAccessTo(ServerWithAuthorizationCode server)
        {
            return !string.IsNullOrEmpty(Tokens.GetToken(server, this).AuthorizationCode);
        }

        public HttpWebRequest GetSignedRequestFor(ServerWithAuthorizationCode server, String location)
        {
            var token = Tokens.GetToken(server, this);
            if (token.AccessToken == "" || token.Expires < DateTime.Now)
                if (token.AuthorizationCode == "")
                    throw new NoAuthorizationCodeException(server, this);
                else
                    token.GetAccessAndRefreshToken();

            var uriForTest = new Uri(location);
            if (uriForTest.Query != "")
                location += "&";
            else
                location += "?";

            location += "access_token=" + token.AccessToken;

            var webRequest = (HttpWebRequest)WebRequest.Create(location);
            return webRequest;
        }

        public void ResetAuthorizationForServer(ServerWithAuthorizationCode server)
        {
            var token = Tokens.GetToken(server, this);
            token.AuthorizationCode = "";
        }
    }
}
