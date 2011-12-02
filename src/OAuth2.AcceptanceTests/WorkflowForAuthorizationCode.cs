using System;
using System.Net;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel.Web;
using FluentAssertions;
using Moq;
using NNS.Authentication.OAuth2.Extensions;
using NUnit.Framework;
using NUnit.Mocks;

namespace NNS.Authentication.OAuth2.AcceptanceTests
{
    public class WorkflowForAuthorizationCode
    {
        private Uri _authorizationRequestUri;
        private Uri _redirectionUri;
        private string _resourceOwnerName ;
        private string _clientId;

        [SetUp]
        public void SetUp()
        {
            _resourceOwnerName = "stoeren";
            if (!ResourceOwners.ResourceOwnerExists(_resourceOwnerName))
                ResourceOwners.Add(_resourceOwnerName);

            _clientId = "268852326492238";
            _authorizationRequestUri = new Uri("http://example.com/AuthorizationRequest");
            _redirectionUri = new Uri("http://example.com/RedirectionUri");
            if (!ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(_clientId, _authorizationRequestUri, _redirectionUri))
                ServersWithAuthorizationCode.Add(_clientId, _authorizationRequestUri, _redirectionUri);
        }

        [Test]
        public void CreateServerAndUsersAndGetCorrectRedirectToAuthorizationRequest()
        {
            // Spec v2-22 4.1.1

            var resourceOwner = ResourceOwners.GetResourceOwner(_resourceOwnerName);
            var server = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(_clientId, _authorizationRequestUri, _redirectionUri);


            Mock<IWebOperationContext> mockContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };

            resourceOwner.HasValidTokenFor(server).Should().BeFalse();
           
            var context = mockContext.Object;
            context.RedirectToAuthorization(server, resourceOwner);

            context.OutgoingResponse.StatusCode.ToString().Should().Be("303 See Other");

            var expectedRedirectionUri = "http://example.com/AuthorizationRequest?response_type=code&client_id=" + 
                server.ClientId + 
                "&state=" + resourceOwner.Guid +
                "&redirect_uri=http%3A%2F%2Fexample%2Ecom%2FRedirectionUri";
            context.OutgoingResponse.Headers.Get("Location").Should().Be(expectedRedirectionUri);
        }

        [Test]
        public void GetAuthorizationCodeViaUserAgentAndRequestProtectedResource()
        {
            //TODO: webrequest mocken
            // diesen dann mit "Pseudo"-Auth-Code ausstatten, die SetToken(server, incommingRequest) => resoruceOwner
            // und die WebRequest.Authorize(server, resourceOwner) anschubsen
            // dabei müssen die UserCredentials richtig gesetzt sein


            var resourceOwner = ResourceOwners.GetResourceOwner(_resourceOwnerName);
            var server = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(_clientId, _authorizationRequestUri, _redirectionUri);

            var webRequest = (HttpWebRequest) WebRequest.Create("http://example.com/ProtectedResource");
            webRequest.SignRequest(server,resourceOwner);

            //Test ob WebRequest richtig unterschrieben wurde

            Assert.Fail("Test is not completed yet");
        }
    }
}
