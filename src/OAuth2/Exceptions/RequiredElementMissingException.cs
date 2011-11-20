using System;
using System.Xml.Linq;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class RequiredElementMissingException : Exception
    {
        public String RequiredElement { get; private set; }
        public XElement Element { get; private set; }

        public RequiredElementMissingException(string requiredElement, XElement element)
        {
            RequiredElement = requiredElement;
            Element = element;
        }
    }
}