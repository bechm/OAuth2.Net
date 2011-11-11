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

        internal ResourceOwner(String name)
        {
            Name = name;
        }

        internal static ResourceOwner FromXElement(XElement element)
        {
            return new ResourceOwner(element.Element("name").Value);
        }

        internal XElement ToXElement()
        {
            return new XElement("ResourceOwner", new XElement("name", Name));
        }
    }
}
