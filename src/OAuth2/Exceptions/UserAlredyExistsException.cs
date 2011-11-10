using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.Authentication.OAuth2.Exceptions
{
    public class UserAlredyExistsException : Exception
    {
        public String Name { get; private set; }

        internal UserAlredyExistsException(String name)
        {
            Name = name;
        }
    }
}
