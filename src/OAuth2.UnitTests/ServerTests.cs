using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NNS.Authentication.OAuth2.Exceptions;
using NUnit.Framework;
using FluentAssertions;

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

        [Test]
        public void ServerToXElement()
        {
            var authorizationuri = new Uri("http://example.com/auth");
            var redirectionUri = new Uri("http://example.com/redirect");
            var server = new ServerWithAuthorizationCode("clientid123",authorizationuri,redirectionUri );
            var element = server.ToXElement();

            element.Should().NotBeNull();
            element.Name.ToString().Should().Be("Server");

            element.Attribute("type").Should().NotBeNull();
            element.Attribute("type").Value.Should().Be("AuthorizationCode");

            element.Element("ClientId").Should().NotBeNull();
            element.Element("ClientId").Value.Should().Be("clientid123");

            element.Element("AuthorizationUri").Should().NotBeNull();
            element.Element("AuthorizationUri").Value.Should().Be(authorizationuri.ToString());

            element.Element("RedirectionUri").Should().NotBeNull();
            element.Element("RedirectionUri").Value.Should().Be(redirectionUri.ToString());
        }

        [Test]
        public void ServerFromXElement()
        {
            var element = new XElement("Server");
            element.Add(new XAttribute("type", "AuthorizationCode"));
            element.Add(new XElement("ClientId", "myspecialClientId"));
            element.Add(new XElement("AuthorizationUri", "http://example.com/anotherfunnyUri"));
            element.Add(new XElement("RedirectionUri", "http://example.com/behappy"));

            var server = ServerWithAuthorizationCode.FromXElement(element);

            server.ClientId.Should().Be("myspecialClientId");
            server.AuthorizationRequestUri.ToString().Should().Be("http://example.com/anotherfunnyUri");
            server.RedirectionUri.ToString().Should().Be("http://example.com/behappy");
        }

        [Test]
        [ExpectedException(typeof(InvalidTypeException))]
        public void ServerFromXElementInvalid()
        {
            var element = new XElement("Server");
            element.Add(new XAttribute("type", "incorrectType"));
            element.Add(new XElement("ClientId", "myspecialClientId"));
            element.Add(new XElement("AuthorizationUri", "http://example.com/anotherfunnyUri"));
            element.Add(new XElement("RedirectionUri", "http://example.com/behappy"));

            var server = ServerWithAuthorizationCode.FromXElement(element);

            Assert.Fail();
        }

    }
}
