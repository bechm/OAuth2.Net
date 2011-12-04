using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using FluentAssertions;
using Moq;
using NNS.Authentication.OAuth2.Exceptions;
using NNS.Authentication.OAuth2.Extensions;
using NUnit.Framework;

namespace NNS.Authentication.OAuth2.UnitTests
{
    [TestFixture]
    class WebRequestExtensionsTests
    {

        [TestFixtureSetUp]
        public void SetUp()
        {
            ResourceOwners.CleanUpForTests();
            ServersWithAuthorizationCode.CleanUpForTests();
            Tokens.CleanUpForTests();
        }

        [Test]
        public void GetCredentialsFromAuthorizationRedirectTest()
        {

            var resourceOwner = ResourceOwners.Add("testusercredetials1");

            var authorizationRequestUri = new Uri("http://example.com/TokenTest/AuthRequest");
            var redirectUri = new Uri("http://example.com/TokenTest/Redirect");
            var server = ServersWithAuthorizationCode.Add("testclienid", authorizationRequestUri, redirectUri);

            Mock<IWebOperationContext> mockContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            var context = mockContext.Object;
            context.IncomingRequest.UriTemplateMatch.RequestUri = new Uri("http://example.org/TokenTest/Redirect");
            context.IncomingRequest.UriTemplateMatch.QueryParameters.Add("code","Splx10BeZQQYbYS6WxSbIA");
            context.IncomingRequest.UriTemplateMatch.QueryParameters.Add("state",server.Guid.ToString() + "_" + resourceOwner.Guid.ToString());
            var tuple = context.IncomingRequest.GetCredentialsFromAuthorizationRedirect();

            tuple.Item1.AuthorizationRequestUri.ToString().Should().Be(server.AuthorizationRequestUri.ToString());
            tuple.Item1.RedirectionUri.ToString().Should().Be(server.RedirectionUri.ToString());
            tuple.Item1.ClientId.Should().Be(server.ClientId);

            tuple.Item2.Name.Should().Be(resourceOwner.Name);
            tuple.Item2.Guid.Should().Be(resourceOwner.Guid);

            var token = Tokens.GetToken(tuple.Item1, tuple.Item2);
            token.AuthorizationCode.Should().Be("Splx10BeZQQYbYS6WxSbIA");

        }

        [Test]
        [ExpectedException(typeof(InvalidAuthorizationRequestException))]
        public void GetCredentialsFromAuthorizationRedirectTestInvalidCode()
        {

            Mock<IWebOperationContext> mockContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            var context = mockContext.Object;
            context.IncomingRequest.UriTemplateMatch.RequestUri = new Uri("http://example.org/TokenTest/Redirect");
            context.IncomingRequest.UriTemplateMatch.QueryParameters.Add("state", "foobar");
            var tuple = context.IncomingRequest.GetCredentialsFromAuthorizationRedirect();

        }

        [Test]
        [ExpectedException(typeof(InvalidAuthorizationRequestException))]
        public void GetCredentialsFromAuthorizationRedirectTestInvalidState1()
        {

            Mock<IWebOperationContext> mockContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            var context = mockContext.Object;
            context.IncomingRequest.UriTemplateMatch.RequestUri = new Uri("http://example.org/TokenTest/Redirect");
            context.IncomingRequest.UriTemplateMatch.QueryParameters.Add("code", "Splx10BeZQQYbYS6WxSbIA");
            
            var tuple = context.IncomingRequest.GetCredentialsFromAuthorizationRedirect();

        }

        [Test]
        [ExpectedException(typeof(InvalidAuthorizationRequestException))]
        public void GetCredentialsFromAuthorizationRedirectTestInvalidState2()
        {

            Mock<IWebOperationContext> mockContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            var context = mockContext.Object;
            context.IncomingRequest.UriTemplateMatch.RequestUri = new Uri("http://example.org/TokenTest/Redirect");
            context.IncomingRequest.UriTemplateMatch.QueryParameters.Add("code", "Splx10BeZQQYbYS6WxSbIA");
            context.IncomingRequest.UriTemplateMatch.QueryParameters.Add("state", "foobar");

            var tuple = context.IncomingRequest.GetCredentialsFromAuthorizationRedirect();

        }

        [Test]
        public void GetCredentialsFromAuthorizationRedirectTestSecurityAsserts()
        {
            Assert.Fail("need to look up after the right Referer for Security Asserts");
        }
    }
}
