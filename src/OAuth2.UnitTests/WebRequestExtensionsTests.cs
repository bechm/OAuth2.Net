using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using FluentAssertions;
using Moq;
using NNS.Authentication.OAuth2.Extensions;
using NUnit.Framework;

namespace NNS.Authentication.OAuth2.UnitTests
{
    [TestFixture]
    class WebRequestExtensionsTests
    {
        [Test]
        public void GetCredentialsFromAuthorizationRedirectTest()
        {
            var resourceOwner = ResourceOwners.Add("testusercredetials1");

            var authorizationRequestUri = new Uri("http://example.com/TokenTest/AuthRequest");
            var redirectUri = new Uri("http://example.com/TokenTest/Redirect");
            var server = ServersWithAuthorizationCode.Add("testclienid", authorizationRequestUri, redirectUri);

            Mock<IWebOperationContext> mockContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            var context = mockContext.Object;
            var tuple = context.IncomingRequest.GetCredentialsFromAuthorizationRedirect();

            tuple.Item1.AuthorizationRequestUri.ToString().Should().Be(server.AuthorizationRequestUri.ToString());
            tuple.Item1.RedirectionUri.ToString().Should().Be(server.RedirectionUri.ToString());
            tuple.Item1.ClientId.Should().Be(server.ClientId);

            tuple.Item2.Name.Should().Be(resourceOwner.Name);
            tuple.Item2.Guid.Should().Be(resourceOwner.Guid);

            Assert.Fail("überprüfen ob Token (AuthorizationCode) richtig gesetzt wurde");
        }
    }
}
