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
        public List<String> Scopes;
        public String ClientId { get; internal set; }
        public String ClientSharedSecret { get; internal set; }
        public Uri AccessTokenRequestUri { get; internal set; }
        public OAuthVersion Version;

        public enum OAuthVersion
        {
            v2_22,
            v2_16,
            v2_12
        }
    }
}
