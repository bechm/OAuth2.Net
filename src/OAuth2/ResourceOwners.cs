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
    public class ResourceOwners : List<ResourceOwner>, IDisposable
    {
        private static ResourceOwners _resourceOwners;
        private const String FileName = "ResourceOwners.xml";
        private Boolean _disposed = false;
        
        private enum NewResourceOwnerSetting
        {
            LoadInstanceFromIsoStore,
            CleanEmptyInstance
        }

        private ResourceOwners(NewResourceOwnerSetting setting = NewResourceOwnerSetting.LoadInstanceFromIsoStore)
        {
            if (setting == NewResourceOwnerSetting.LoadInstanceFromIsoStore)
            {
                LoadFromIsoStore();
            }
        }

        internal static void LoadFromIsoStore()
        {
            using (IsolatedStorageFile storageFile = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                _resourceOwners = new ResourceOwners(NewResourceOwnerSetting.CleanEmptyInstance);
                
                if (!storageFile.FileExists(FileName))
                    return;

                using (var fileStream = new IsolatedStorageFileStream(FileName, FileMode.Open, FileAccess.Read, storageFile))
                {
                    XDocument document = XDocument.Load(fileStream);
                    foreach (var element in document.Root.Elements("ResourceOwner"))
                    {
                        _resourceOwners.Add(ResourceOwner.FromXElement(element));
                    }
                }
            }
        }


        ~ResourceOwners()
        {
            if (!_disposed)
                Dispose();
        }

        internal static void SaveToIsoStore()
        {
            _resourceOwners.Dispose();
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
            var root = new XElement("ResourceOwners");
            foreach (var resourceOwner in _resourceOwners)
            {
                root.Add(resourceOwner.ToXElement());
            }
            return root;
        }


        internal static void CleanUpForTests()
        {
            _resourceOwners = new ResourceOwners(NewResourceOwnerSetting.CleanEmptyInstance);
        }

        public static ResourceOwner Add(String name)
        {
            GetInstanceOfResourceOwner();

            if(ResourceOwnerExists(name) == true)
                throw new UserAlredyExistsException(name);

            var resourceOwner = new ResourceOwner(name);
            _resourceOwners.Add(resourceOwner);
            return resourceOwner;
        }

        public static ResourceOwner GetResourceOwner(String name)
        {
            GetInstanceOfResourceOwner();

            return _resourceOwners.FirstOrDefault(item => item.Name == name);
        }

        public static Boolean ResourceOwnerExists(String name)
        {
            GetInstanceOfResourceOwner();
            return (_resourceOwners.FirstOrDefault(item => item.Name == name) != null);
        }

        private static void GetInstanceOfResourceOwner()
        {
            if (_resourceOwners == null)
                _resourceOwners = new ResourceOwners();
        }

    }
}
