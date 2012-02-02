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

            var serverWithAuth = ServersWithAuthorizationCode.Add("myfunnyid", "myfunnysecret", new Uri("http://example.com/AuthorizationRequest"), new Uri("http://example.com/access"), new Uri("http://example.com/RedirectionUri"));
            serverWithAuth.Should().NotBeNull();

            serverWithAuth.ClientId.Should().Be("myfunnyid");
            serverWithAuth.ClientSharedSecret.Should().Be("myfunnysecret");

            serverWithAuth.AuthorizationRequestUri.ToString().Should().Be("http://example.com/AuthorizationRequest");
            serverWithAuth.RedirectionUri.ToString().Should().Be("http://example.com/RedirectionUri");
            serverWithAuth.Guid.Should().NotBeEmpty();
            serverWithAuth.Guid.ToString().Should().NotBe(new Guid().ToString());
            serverWithAuth.Scopes.Should().NotBeNull();
        }

        [Test]
        [ExpectedException(typeof(ServerWithAuthorizationCodeAlredyExistsException))]
        public void CreateServerDouble()
        {
            ServersWithAuthorizationCode.CleanUpForTests();

            var serverWithAuth = ServersWithAuthorizationCode.Add("myfunnyid", "myfunnysecret", new Uri("http://example.com/AuthorizationRequest"), new Uri("http://example.com/access"), new Uri("http://example.com/RedirectionUri"));
            Assert.IsNotNull(serverWithAuth);
            Assert.AreEqual("myfunnyid", serverWithAuth.ClientId);

            ServersWithAuthorizationCode.Add("myfunnyid", "myfunnysecret", new Uri("http://example.com/AuthorizationRequest"), new Uri("http://example.com/access"), new Uri("http://example.com/RedirectionUri"));
            
        }

        [Test]
        public void GetServer()
        {
            ServersWithAuthorizationCode.CleanUpForTests();
            var server1 = ServersWithAuthorizationCode.Add("myfunnyid",
                                                           "myfunnysecret",
                                                           new Uri("http://example.com/AuthorizationRequest"),
                                                           new Uri("http://example.com/AccessRequest"),
                                                           new Uri("http://example.com/RedirectionUri"));
            var server2 = ServersWithAuthorizationCode.Add("myfunnyid2",
                                                           "myfunnysecret",
                                                           new Uri("http://example.com/AuthorizationRequest2"),
                                                           new Uri("http://example.com/AccessRequest2"),
                                                           new Uri("http://example.com/RedirectionUri2"));
            server2.Version = Server.OAuthVersion.v2_22;


            var server1Result = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(server1.Guid);
            Assert.AreEqual(server1, server1Result);
            Assert.IsTrue(ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(server1.Guid));

            var server2Result = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(server2.ClientId,server2.AuthorizationRequestUri, server2.AccessTokenRequestUri, server2.RedirectionUri);
            Assert.AreEqual(server2, server2Result);
            Assert.IsTrue(ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(server2.ClientId, server2.AuthorizationRequestUri, server2.AccessTokenRequestUri, server2.RedirectionUri));

            var resourceOwnerNull = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(Guid.NewGuid());
            Assert.IsNull(resourceOwnerNull);
            Assert.IsFalse(ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(Guid.NewGuid()));

        }

        [Test]
        public void ServerToXElement()
        {
            var authorizationuri = new Uri("http://example.com/auth");
            var accessUri = new Uri("http://example.com/auth/access");
            var redirectionUri = new Uri("http://example.com/redirect");
            var server = new ServerWithAuthorizationCode("clientid123", "myfunnysecret", authorizationuri, accessUri, redirectionUri, new List<String>(){"funnyscope"});
            var element = server.ToXElement();

            element.Should().NotBeNull();
            element.Name.ToString().Should().Be("Server");

            element.Attribute("type").Should().NotBeNull();
            element.Attribute("type").Value.Should().Be("AuthorizationCode");

            element.Element("Guid").Should().NotBeNull();
            element.Element("Guid").Value.Should().Be(server.Guid.ToString());

            element.Element("ClientId").Should().NotBeNull();
            element.Element("ClientId").Value.Should().Be("clientid123");

            element.Element("ClientSharedSecret").Should().NotBeNull();
            element.Element("ClientSharedSecret").Value.Should().Be("myfunnysecret");

            var scopes = element.Element("Scopes");
            scopes.Should().NotBeNull();
            scopes.Element("Scope").Value.Should().NotBeNull();
            scopes.Element("Scope").Value.Should().Be("funnyscope");

            element.Element("AuthorizationUri").Should().NotBeNull();
            element.Element("AuthorizationUri").Value.Should().Be(authorizationuri.ToString());

            element.Element("AccessTokenUri").Should().NotBeNull();
            element.Element("AccessTokenUri").Value.Should().Be(accessUri.ToString());

            element.Element("RedirectionUri").Should().NotBeNull();
            element.Element("RedirectionUri").Value.Should().Be(redirectionUri.ToString());
        }

        [Test]
        public void ServerFromXElement()
        {
            var element = new XElement("Server");
            element.Add(new XAttribute("type", "AuthorizationCode"));
            element.Add(new XElement("Guid", "f1287c12-1cf3-45b3-ac29-5bfce34b2145"));
            element.Add(new XElement("ClientId", "myspecialClientId"));
            element.Add(new XElement("ClientSharedSecret", "acsecret123"));
            element.Add(new XElement("Scopes", new XElement("Scope", "foobar")));
            element.Add(new XElement("AuthorizationUri", "http://example.com/anotherfunnyUri"));
            element.Add(new XElement("AccessTokenUri", "http://example.com/anotherfunnyUri2"));
            element.Add(new XElement("RedirectionUri", "http://example.com/behappy"));

            var server = ServerWithAuthorizationCode.FromXElement(element);

            server.ClientId.Should().Be("myspecialClientId");
            server.ClientSharedSecret.Should().Be("acsecret123");
            server.Guid.ToString().Should().Be("f1287c12-1cf3-45b3-ac29-5bfce34b2145");
            server.AuthorizationRequestUri.ToString().Should().Be("http://example.com/anotherfunnyUri");
            server.AccessTokenRequestUri.ToString().Should().Be("http://example.com/anotherfunnyUri2");
            server.RedirectionUri.ToString().Should().Be("http://example.com/behappy");
            server.Scopes.FirstOrDefault(item => item == "foobar").Should().NotBeNull();
        }

        [Test]
        [ExpectedException(typeof(InvalidTypeException))]
        public void ServerFromXElementInvalid()
        {
            var element = new XElement("Server");
            element.Add(new XAttribute("type", "incorrectType"));
            element.Add(new XElement("Guid", "f1287c12-1cf3-45b3-ac29-5bfce34b2145"));
            element.Add(new XElement("ClientId", "myspecialClientId"));
            element.Add(new XElement("ClientSharedSecret", "acsecret123"));
            element.Add(new XElement("Scopes", new XElement("Scope", "foobar")));
            element.Add(new XElement("AuthorizationUri", "http://example.com/anotherfunnyUri"));
            element.Add(new XElement("AccessTokenUri", "http://example.com/anotherfunnyUri2"));
            element.Add(new XElement("RedirectionUri", "http://example.com/behappy"));

            var server = ServerWithAuthorizationCode.FromXElement(element);

        }

        [Test]
        public void DisposeAndLoad()
        {
            ServersWithAuthorizationCode.CleanUpForTests();
            var server1 = ServersWithAuthorizationCode.Add("server1", "afunnysecret", new Uri("http://example.org/uri1"), new Uri("http://example.org/uri2"), new Uri("http://example.org/uri3"), new List<String>() { "scopedmaskl", "scope2" });
            ServersWithAuthorizationCode.Add("server2", "afunnysecret", new Uri("http://example.org/uri4"), new Uri("http://example.org/uri5"), new Uri("http://example.org/uri6"));

            ServersWithAuthorizationCode.SaveToIsoStore();
            ServersWithAuthorizationCode.LoadFromIsoStore();

            var server = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(server1.Guid);
            server.Should().NotBeNull();
            server.ClientId.Should().Be("server1");
            server.AuthorizationRequestUri.ToString().Should().Be("http://example.org/uri1");
            server.AccessTokenRequestUri.ToString().Should().Be("http://example.org/uri2");
            server.RedirectionUri.ToString().Should().Be("http://example.org/uri3");
            server.Scopes.FirstOrDefault(item => item == "scopedmaskl").Should().NotBeNull();
            server.Scopes.FirstOrDefault(item => item == "scope2").Should().NotBeNull();

            var serverNull = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(Guid.NewGuid());
            serverNull.Should().BeNull();

        }

    }
}
