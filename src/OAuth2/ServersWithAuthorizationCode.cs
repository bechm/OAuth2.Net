using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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
            if (setting == NewServersWithAuthorizationCodeSetting.LoadInstanceFromIsoStore)
            {
                LoadFromIsoStore();
            }
        }

        internal static void SaveToIsoStore()
        {
            _servers.Dispose();
        }

        internal static void CleanUpForTests()
        {
            _servers = new ServersWithAuthorizationCode();
        }

        internal static void LoadFromIsoStore()
        {
            using (IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                _servers = new ServersWithAuthorizationCode(NewServersWithAuthorizationCodeSetting.CleanEmptyInstance);

                if (!storageFile.FileExists(FileName))
                    return;

                using (var fileStream = new IsolatedStorageFileStream(FileName, FileMode.Open, FileAccess.Read, storageFile))
                {
                    XDocument document = XDocument.Load(fileStream);
                    foreach (var element in document.Root.Elements("Server"))
                    {
                        _servers.Add(ServerWithAuthorizationCode.FromXElement(element));
                    }
                }
            }
        }

        ~ServersWithAuthorizationCode()
        {
            if (!_disposed)
                Dispose();
        }

        public void Dispose()
        {
            var document = new XDocument(ToXElement());
            using (var storageFile = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                using (
                    var fileStream = new IsolatedStorageFileStream(FileName, FileMode.Create, FileAccess.Write,
                                                                   storageFile))
                {
                    document.Save(fileStream);
                }
            }
            _disposed = true;
        }

        private XElement ToXElement()
        {
            var root = new XElement("Servers");
            foreach (var server in _servers)
            {
                root.Add(server.ToXElement());
            }
            return root;
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
