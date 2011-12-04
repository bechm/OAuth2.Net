using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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

        public Boolean HasValidTokenFor(ServerWithAuthorizationCode server)
        {
            return Tokens.GetToken(server, this) != null;
        }
    }
}
