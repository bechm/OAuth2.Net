using System;
using System.Collections.Generic;
using System.IO;
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
            var accessTokenUri = new Uri("http://example.org/GetAccessAndRefreshTokenTest/Access");
            var redirectionUri = new Uri("http://example.org/GetAccessAndRefreshTokenTest/redirectionUri");
            _server = ServersWithAuthorizationCode.Add("123456789", "testsecret", authorizationRequestUri,accessTokenUri, redirectionUri);
            _resourceOwner = ResourceOwners.Add("Test");
            _token = Tokens.GetToken(_server, _resourceOwner);
            _token.RedirectUri = _server.RedirectionUri;
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

            webRequest.RequestUri.AbsoluteUri.Should().Be("http://example.org/GetAccessAndRefreshTokenTest/Access");
            webRequest.Headers.Get("Authorization").Should().Be("Basic MTIzNDU2Nzg5OnRlc3RzZWNyZXQ=");


            var expectedInnerText = "grant_type=authorization_code" +
                           "&code=" + _token.AuthorizationCode +
                           "&redirect=http%3a%2f%2fexample%2eorg%2fredirectionUri";
            var stream = webRequest.GetRequestStream();
            var reader = new StreamReader(stream);
            var innertext = reader.ReadToEnd();
            innertext.Should().Be(expectedInnerText);
        }
    }
}
