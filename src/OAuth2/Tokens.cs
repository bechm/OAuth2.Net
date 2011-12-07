using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NNS.Authentication.OAuth2
{
    internal class Tokens : List<Token>, IDisposable
    {
        private static Tokens _tokens;
        private const String FileName = "Tokens.xml";
        private Boolean _disposed = false;

        private enum NewTokensSetting
        {
            LoadInstanceFromIsoStore,
            CleanEmptyInstance
        }

        private Tokens(NewTokensSetting setting = NewTokensSetting.LoadInstanceFromIsoStore)
        {
            if (setting == NewTokensSetting.LoadInstanceFromIsoStore)
            {
                LoadFromIsoStore();
            }
        }

        internal static void CleanUpForTests()
        {
            _tokens = new Tokens(NewTokensSetting.CleanEmptyInstance);
        }

        internal static Token GetToken(ServerWithAuthorizationCode server, ResourceOwner resourceOwner)
        {
            GetTokensInstance();
            var token =
                _tokens.FirstOrDefault(t => (t.Server.Guid == server.Guid && t.ResourceOwner.Name == resourceOwner.Name));
            if(token == null)
            {
                token = new Token(server,resourceOwner);
                _tokens.Add(token);
            }
            return token;
        }

        internal static void AddToken(Token token)
        {
            GetTokensInstance();
            _tokens.Add(token);
        }

        private static void GetTokensInstance()
        {
            if (_tokens == null)
                _tokens = new Tokens();
        }

        public static void SaveToIsoStore()
        {
            _tokens.Dispose();
        }

        public static void LoadFromIsoStore()
        {
            using (IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                _tokens = new Tokens(NewTokensSetting.CleanEmptyInstance);

                if (!storageFile.FileExists(FileName))
                    return;

                using (var fileStream = new IsolatedStorageFileStream(FileName, FileMode.Open, FileAccess.Read, storageFile))
                {
                    XDocument document = XDocument.Load(fileStream);
                    foreach (var element in document.Root.Elements("Token"))
                    {
                        _tokens.Add(Token.FromXElement(element));
                    }
                }
            }
        }

        ~Tokens()
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

        private static XElement ToXElement()
        {
            var root = new XElement("Tokens");
            GetTokensInstance();
            foreach (var token in _tokens)
            {
                root.Add(token.ToXElement());
            }
            return root;
        }
    }
}
