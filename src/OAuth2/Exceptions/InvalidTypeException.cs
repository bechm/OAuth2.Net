using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NNS.Authentication.OAuth2.Exceptions
{
    class InvalidTypeException : Exception
    {
        public String ExcpectedType { get; private set; }
        public String ActualType { get; private set; }
        public XElement Element { get; set; }

        public InvalidTypeException(String expectedType, String actualType, XElement element)
        {
            ExcpectedType = expectedType;
            ActualType = actualType;
            Element = element;
        }

    }
}
