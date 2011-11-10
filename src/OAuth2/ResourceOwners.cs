using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNS.Authentication.OAuth2.Exceptions;

namespace NNS.Authentication.OAuth2
{
    public class ResourceOwners : List<ResourceOwner>
    {
        private static ResourceOwners _resourceOwners;
        
        private ResourceOwners(Boolean cleanInstanceForTests = false)
        {
            if (cleanInstanceForTests == false) {
                //TODO: Load from IsoStore
            }
        }

        internal static void CleanUpForTests()
        {
            _resourceOwners = new ResourceOwners();
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
