using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.Authentication.OAuth2
{
    public abstract class Server
    {
        public Guid Guid;
        public Uri AuthorizationRequestUri;
        public String Scope;

    }
}
