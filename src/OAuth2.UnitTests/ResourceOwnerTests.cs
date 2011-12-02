using System.Xml.Linq;
using FluentAssertions;
using Moq;
using NNS.Authentication.OAuth2.Exceptions;
using NUnit.Framework;

namespace NNS.Authentication.OAuth2.UnitTests
{
    public class ResourceOwnerTests
    {
        [Test]
        public void CreateResourceOwner()
        {
            ResourceOwners.CleanUpForTests();
            var resourceOwner = ResourceOwners.Add("user1");
            Assert.IsNotNull(resourceOwner);
            Assert.AreEqual("user1", resourceOwner.Name);

        }

        [Test]
        [ExpectedException(typeof(UserAlredyExistsException))]
        public void CreateResourceOwnerDouble()
        {
            ResourceOwners.CleanUpForTests();
            var resourceOwner = ResourceOwners.Add("user1");
            Assert.IsNotNull(resourceOwner);
            Assert.AreEqual("user1", resourceOwner.Name);

            var resourceOwner2 = ResourceOwners.Add("user1");
        }

        [Test]
        public void GetResourceOwner()
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
        public void DisposeAndLoad()
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

            element.Should().NotBeNull();
            element.Element("name").Should().NotBeNull();
            element.Element("name").Value.Should().Be("user1");
            element.Element("guid").Should().NotBeNull();
            element.Element("guid").Value.Should().Be(resourceOwner.Guid.ToString());
        }

        [Test]
        public void ResourceOwnerFromXElement()
        {
            var element = new XElement("ResourceOwner", new XElement("name", "user1"), new XElement("guid", "99c33d15-5fc1-417c-ae4e-0df51621c874"));
            var resourceOwner = ResourceOwner.FromXElement(element);

            resourceOwner.Should().NotBeNull();
            resourceOwner.Name.Should().Be("user1");
            resourceOwner.Guid.ToString().Should().Be("99c33d15-5fc1-417c-ae4e-0df51621c874");
        }
    }
}
