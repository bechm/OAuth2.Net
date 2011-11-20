using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNS.Authentication.OAuth2
{
    public class Tokens
    {
        private static List<Token> _tokens;

        internal static void CleanUpForTests()
        {
            throw new NotImplementedException();
        }

        internal static Token GetToken(ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }

        public static Token Add(ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            throw new NotImplementedException();
        }

        public static void SaveToIsoStore()
        {
            throw new NotImplementedException();
        }

        public static void LoadFromIsoStore()
        {
            throw new NotImplementedException();
        }
    }
}
