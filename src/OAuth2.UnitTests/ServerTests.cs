using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNS.Authentication.OAuth2.Exceptions;
using NUnit.Framework;

namespace NNS.Authentication.OAuth2.UnitTests
{
    class ServerTests
    {
        [Test]
        public void CreateServer()
        {
            ServersWithAuthorizationCode.CleanUpForTests();

            var serverWithAuth = ServersWithAuthorizationCode.Add("myfunnyid", new Uri("http://example.com/AuthorizationRequest"), new Uri("http://example.com/RedirectionUri"));
            Assert.IsNotNull(serverWithAuth);

            Assert.AreEqual("myfunnyid",serverWithAuth.ClientId);
            Assert.AreEqual("http://example.com/AuthorizationRequest", serverWithAuth.AuthorizationRequestUri.ToString());
            Assert.AreEqual("http://example.com/RedirectionUri", serverWithAuth.RedirectionUri.ToString());
            Assert.IsNotNull(serverWithAuth.Guid);
            Assert.AreNotEqual(new Guid().ToString(), serverWithAuth.Guid.ToString());
        }

        [Test]
        [ExpectedException(typeof(ServerWithAuthorizationCodeAlredyExistsException))]
        public void CreateServerDouble()
        {
            ServersWithAuthorizationCode.CleanUpForTests();
            
            var serverWithAuth = ServersWithAuthorizationCode.Add("myfunnyid", new Uri("http://example.com/AuthorizationRequest"), new Uri("http://example.com/RedirectionUri"));
            Assert.IsNotNull(serverWithAuth);
            Assert.AreEqual("myfunnyid", serverWithAuth.ClientId);

            ServersWithAuthorizationCode.Add("myfunnyid", new Uri("http://example.com/AuthorizationRequest"), new Uri("http://example.com/RedirectionUri"));
            
        }

        [Test]
        public void GetServer()
        {
            ServersWithAuthorizationCode.CleanUpForTests();
            var server1 = ServersWithAuthorizationCode.Add("myfunnyid", new Uri("http://example.com/AuthorizationRequest"), new Uri("http://example.com/RedirectionUri"));
            var server2 = ServersWithAuthorizationCode.Add("myfunnyid2", new Uri("http://example.com/AuthorizationRequest2"), new Uri("http://example.com/RedirectionUri2"));


            var server1Result = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(server1.Guid);
            Assert.AreEqual(server1, server1Result);
            Assert.IsTrue(ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(server1.Guid));

            var server2Result = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(server2.ClientId,server2.AuthorizationRequestUri, server2.RedirectionUri);
            Assert.AreEqual(server2, server2Result);
            Assert.IsTrue(ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(server2.ClientId, server2.AuthorizationRequestUri, server2.RedirectionUri));

            var resourceOwnerNull = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(Guid.NewGuid());
            Assert.IsNull(resourceOwnerNull);
            Assert.IsFalse(ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(Guid.NewGuid()));

        }
    }
}
