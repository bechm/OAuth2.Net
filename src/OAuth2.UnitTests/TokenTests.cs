using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace NNS.Authentication.OAuth2.UnitTests
{
    [TestFixture]
    class TokenTests
    {
        [Test]
        public void TokenComperator()
        {
            var server1 = new ServerWithAuthorizationCode("test",
                                                          new Uri("http://example.org/foo"),
                                                          new Uri("http://example.org/test2"));
            var server2 = new ServerWithAuthorizationCode("test2",
                                                          new Uri("http://example.org/fo32o"),
                                                          new Uri("http://example.org/tesdst2"));
            var resourceOwner1 = new ResourceOwner("testmann1");
            var resourceOwner2 = new ResourceOwner("testmann2");

            var token1 = new Token(server1,resourceOwner1);
            var token2 = new Token(server1,resourceOwner2);
            var token3 = new Token(server2,resourceOwner1);
            var token4 = new Token(server2,resourceOwner2);
            var token5 = new Token(server1,resourceOwner1);

            token1.Equals(token1).Should().BeTrue();
            token1.Equals(token2).Should().BeFalse();
            token1.Equals(token3).Should().BeFalse();
            token1.Equals(token4).Should().BeFalse();
            token1.Equals(token5).Should().BeTrue();

            (token1 == token5).Should().BeTrue();
            (token1 == token2).Should().BeFalse();
            (token1 != token2).Should().BeTrue();

            token1.Equals(42).Should().BeFalse();
            token1.Equals(null).Should().BeFalse();

        }

        [Test]
        public void GetToken()
        {
            var server1 = new ServerWithAuthorizationCode("test",
                                                          new Uri("http://example.org/foo"),
                                                          new Uri("http://example.org/test2"));
            var resourceOwner1 = new ResourceOwner("testmann1");
            Tokens.CleanUpForTests();
            var token1 = Tokens.GetToken(server1, resourceOwner1);

            token1.ResourceOwner.Should().Be(resourceOwner1);
            token1.Server.Should().Be(server1);
            token1.AuthorizationCode.Should().Be("");
            token1.AccessToken.Should().Be("");
            token1.RefreshToken.Should().Be("");

            token1.AuthorizationCode = "AuthorizationCode";
            token1.AccessToken = "AccessToken";
            token1.RefreshToken = "RefreshToken";

            var token2 = Tokens.GetToken(server1, resourceOwner1);

            token2.AuthorizationCode.Should().Be("AuthorizationCode");
            token2.AccessToken.Should().Be("AccessToken");
            token2.RefreshToken.Should().Be("RefreshToken");
            DateTime.Now.Subtract(token2.Expires).Should().BeLessOrEqualTo(new TimeSpan(0, 0, 1, 0));

        }

        [Test]
        public void TokenToXElement()
        {
            var server1 = new ServerWithAuthorizationCode("test",
                                                          new Uri("http://example.org/foo"),
                                                          new Uri("http://example.org/test2"));
            var resourceOwner1 = new ResourceOwner("testmann1");

            var token = new Token(server1, resourceOwner1);
            token.AuthorizationCode = "auth1";
            token.AccessToken = "access1";
            token.RefreshToken = "refresht";
            
            var element1 = token.ToXElement();

            element1.Name.ToString().Should().Be("Token");
            element1.Element("Server").Value.Should().Be(server1.Guid.ToString());
            element1.Element("ResourceOwner").Value.Should().Be(resourceOwner1.Name);
            element1.Element("AuthorizationCode").Value.Should().Be("auth1");
            element1.Element("AccessToken").Value.Should().Be("access1");
            element1.Element("RefreshToken").Value.Should().Be("refresht");
            

        }

        [Test]
        public void TokenFromXElement()
        {
            var element = new XElement("Token");
            var server = ServersWithAuthorizationCode.Add("testclient", new Uri("http://example.com/uri1"),
                                                          new Uri("http://example.com/uri2"));
            var resourceOwner = ResourceOwners.Add("testuser");

            element.Add(new XElement("Server", server.Guid.ToString()));
            element.Add(new XElement("ResourceOwner", resourceOwner.Name));
            element.Add(new XElement("AuthorizationCode", "foobar4"));
            element.Add(new XElement("AccessToken", "foobar1"));
            element.Add(new XElement("RefreshToken", "foobar2"));
            element.Add(new XElement("Expires", DateTime.Today.ToString()));

            var token = Token.FromXElement(element);

            token.Server.Should().NotBeNull();
            token.ResourceOwner.Should().Be(resourceOwner);
            token.AuthorizationCode.Should().Be("foobar4");
            token.AccessToken.Should().Be("foobar1");
            token.RefreshToken.Should().Be("foobar2");
        }

        [Test]
        public void DisposeAndLoad()
        {
            Tokens.CleanUpForTests();
            var server1 = ServersWithAuthorizationCode.Add("testclient", new Uri("http://example.com/uri1"),
                                                          new Uri("http://example.com/uri2"));
            var resourceOwner1 = ResourceOwners.Add("testuser");

            Token token = Tokens.Add(server1, resourceOwner1);
            token.Expires = DateTime.Now;
            token.AccessToken = "token1";
            token.AuthorizationCode = "token2";
            token.RefreshToken = "token3";

            Tokens.SaveToIsoStore();
            Tokens.LoadFromIsoStore();

            var tokenAfter = Tokens.GetToken(server1,resourceOwner1);
            tokenAfter.Should().NotBeNull();
            tokenAfter.Expires.Should().Be(token.Expires);
            tokenAfter.AccessToken.Should().Be(token.AccessToken);
            tokenAfter.AuthorizationCode.Should().Be(token.AuthorizationCode);
            tokenAfter.RefreshToken.Should().Be(token.RefreshToken);

        }

    }
}
