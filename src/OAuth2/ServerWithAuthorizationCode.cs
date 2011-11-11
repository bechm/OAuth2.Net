using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.Authentication.OAuth2
{
    public class ServerWithAuthorizationCode : Server
    {
        public String ClientId { get; private set; }
        public Uri RedirectionUri { get; private set; }

        internal ServerWithAuthorizationCode(string clientId, Uri authorizationRequestUri, Uri redirectionUri)
        {
            ClientId = clientId;
            AuthorizationRequestUri = authorizationRequestUri;
            RedirectionUri = redirectionUri;
            Guid = Guid.NewGuid();
        }
    }
}
