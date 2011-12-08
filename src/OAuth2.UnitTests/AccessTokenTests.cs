using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace NNS.Authentication.OAuth2.UnitTests
{
    class AccessTokenTests
    {
        private ServerWithAuthorizationCode _server;
        private ResourceOwner _resourceOwner;
        private Token _token;

        [TestFixtureSetUp]
        public void SetUp()
        {
            Tokens.CleanUpForTests();
            ResourceOwners.CleanUpForTests();
            ServersWithAuthorizationCode.CleanUpForTests();

            var authorizationRequestUri = new Uri("http://example.org/GetAccessAndRefreshTokenTest/Authorization");
            var redirectionUri = new Uri("http://example.org/GetAccessAndRefreshTokenTest/redirectionUri");
            _server = ServersWithAuthorizationCode.Add("123456789", "testsecret", authorizationRequestUri, redirectionUri);
            _resourceOwner = ResourceOwners.Add("Test");
            _token = Tokens.GetToken(_server, _resourceOwner);
            _token.AuthorizationCode = "Aplx10BeZQQYbYS6WxSbIA";
        }


        [Test]
        public void GetAccessAndRefreshTokenTest()
        {
            

            _token.GetAccessAndRefreshToken();

            _token.AccessToken.Should().NotBeEmpty();
            _token.RefreshToken.Should().NotBeEmpty();
        }

        [Test]
        public void GetWebRequestForAccessTokenRequestTest()
        {
            _token.RedirectUri = new Uri("http://example.org/redirectionUri");

            var webRequest = _token.GetWebRequestForAccessTokenRequest();

            webRequest.Host.Should().Be("example.org");
            webRequest.Headers.Get("Authorization").Should().Be("Basic czZCaGRSa3FOMzpnWDFmQmFOM2JW");

            var expecedtUri = "http://example.org/GetAccessAndRefreshTokenTest/Authorization" +
                                       "?grant_type=authorization_code" +
                                       "&code=" + _token.AuthorizationCode +
                                       "&redirect=http:%2f%2fexample%2eorg%2fredirectionUri";
            webRequest.RequestUri.ToString().Should().Be(expecedtUri);
        }
    }
}
