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
    }
}
