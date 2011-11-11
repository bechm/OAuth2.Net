using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNS.Authentication.OAuth2.Exceptions;

namespace NNS.Authentication.OAuth2
{
    public class ServersWithAuthorizationCode : List<ServerWithAuthorizationCode>, IDisposable
    {
        private static ServersWithAuthorizationCode _servers;
        private const String FileName = "ServersWithAuthorizationCode.xml";
        private Boolean _disposed = false;

        private enum NewServersWithAuthorizationCodeSetting
        {
            LoadInstanceFromIsoStore,
            CleanEmptyInstance
        }

        private ServersWithAuthorizationCode(NewServersWithAuthorizationCodeSetting setting = NewServersWithAuthorizationCodeSetting.LoadInstanceFromIsoStore)
        {
            //TODO: implement
        }

        internal static void CleanUpForTests()
        {
            _servers = new ServersWithAuthorizationCode();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public static ServerWithAuthorizationCode Add(string clientId, Uri authorizationRequestUri, Uri redirectionUri)
        {
            GetInstanceOfServers();

            if (ServerWithAuthorizationCodeExists(clientId, authorizationRequestUri, redirectionUri) == true)
                throw new ServerWithAuthorizationCodeAlredyExistsException(clientId, authorizationRequestUri, redirectionUri);

            var server = new ServerWithAuthorizationCode(clientId, authorizationRequestUri, redirectionUri);
            _servers.Add(server);
            return server;
        }

        public static ServerWithAuthorizationCode GetServerWithAuthorizationCode(Guid guid)
        {
            GetInstanceOfServers();
            return _servers.FirstOrDefault(item => item.Guid == guid);
        }

        public static ServerWithAuthorizationCode GetServerWithAuthorizationCode(string clientId, Uri authorizationRequestUri, Uri redirectionUri)
        {
            GetInstanceOfServers();
            return _servers.FirstOrDefault(item => item.ClientId == clientId &&
                                                   item.AuthorizationRequestUri == authorizationRequestUri &&
                                                   item.RedirectionUri == redirectionUri);
        }

        public static Boolean ServerWithAuthorizationCodeExists(Guid guid)
        {
            GetInstanceOfServers();
            return (_servers.FirstOrDefault(item => item.Guid == guid) != null);
        }

        public static Boolean ServerWithAuthorizationCodeExists(string clientId, Uri authorizationRequestUri, Uri redirectionUri)
        {
            GetInstanceOfServers();
            return (_servers.FirstOrDefault(item => item.ClientId == clientId &&
                                                    item.AuthorizationRequestUri == authorizationRequestUri &&
                                                    item.RedirectionUri == redirectionUri) != null);
        }

        private static void GetInstanceOfServers()
        {
            if (_servers == null)
                _servers = new ServersWithAuthorizationCode();
        }
    }
}
