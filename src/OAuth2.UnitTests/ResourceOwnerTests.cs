using System.Xml.Linq;
using Moq;
using NNS.Authentication.OAuth2.Exceptions;
using NUnit.Framework;

namespace NNS.Authentication.OAuth2.UnitTests
{
    public class ResourceOwnerTests
    {
        [Test]
        public void TestCreateResourceOwner()
        {
            ResourceOwners.CleanUpForTests();
            var resourceOwner = ResourceOwners.Add("user1");
            Assert.IsNotNull(resourceOwner);
            Assert.AreEqual("user1", resourceOwner.Name);

        }

        [Test]
        [ExpectedException(typeof(UserAlredyExistsException))]
        public void TestCreateResourceOwnerDouble()
        {
            ResourceOwners.CleanUpForTests();
            var resourceOwner = ResourceOwners.Add("user1");
            Assert.IsNotNull(resourceOwner);
            Assert.AreEqual("user1", resourceOwner.Name);

            var resourceOwner2 = ResourceOwners.Add("user1");
        }

        [Test]
        public void TestGetResourceOwner()
        {
            ResourceOwners.CleanUpForTests();
            ResourceOwners.Add("user1");
            ResourceOwners.Add("user2");

            var resourceOwner = ResourceOwners.GetResourceOwner("user1");
            Assert.AreEqual("user1", resourceOwner.Name);

            var resourceOwnerNull = ResourceOwners.GetResourceOwner("foo");
            Assert.IsNull(resourceOwnerNull);

        }

        [Test]
        public void TestDisposeAndLoad()
        {
            ResourceOwners.CleanUpForTests();
            ResourceOwners.Add("user1");
            ResourceOwners.Add("user2");

            ResourceOwners.SaveToIsoStore();
            ResourceOwners.LoadFromIsoStore();

            var resourceOwner = ResourceOwners.GetResourceOwner("user1");
            Assert.IsNotNull(resourceOwner);
            Assert.AreEqual("user1", resourceOwner.Name);

            var resourceOwnerNull = ResourceOwners.GetResourceOwner("foo");
            Assert.IsNull(resourceOwnerNull);

        }

        [Test]
        public void ResourceOwnerToXElement()
        {
            var resourceOwner = new ResourceOwner("user1");
            var element = resourceOwner.ToXElement();

            Assert.IsNotNull(element);
            Assert.AreEqual("user1", element.Element("name").Value);
        }

        [Test]
        public void ResourceOwnerFromXElement()
        {
            var element = new XElement("ResourceOwner", new XElement("name", "user1"));
            var resourceOwner = ResourceOwner.FromXElement(element);

            Assert.IsNotNull(resourceOwner);
            Assert.AreEqual("user1",resourceOwner.Name);
        }
    }
}
