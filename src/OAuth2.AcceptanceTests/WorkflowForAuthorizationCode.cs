using System;
using System.Net;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel.Web;
using Moq;
using NNS.Authentication.OAuth2.Extensions;
using NUnit.Framework;
using NUnit.Mocks;
using WebOperationContext = System.ServiceModel.Web.MockedWebOperationContext;

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
        public void CreateServerAndUsersAndGetCorrectRedirect()
        {
            Mock<IWebOperationContext> mockContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            using (new MockedWebOperationContext(mockContext.Object))
            {
                var outgoingRespose = WebOperationContext.Current.OutgoingResponse;
                outgoingRespose.SetStatusAsNotFound("notfound");
            }

            var resourceOwner = ResourceOwners.GetResourceOwner(_resourceOwnerName);
            var server = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(_clientId, _authorizationRequestUri, _redirectionUri);

            //Da soll es mal hin gehen:
            //if (!resourceOwner.HasValidTokenFor(server))
            //{
            //    context.RedirectToAuthorization(server, resourceOwner);
            //}
            
            //TODO: WebRequest Mocken und überpüfen
            // wir haben irgend einen gemockten WebRequest, 
            // den wir auf die LoginSeite redirecten müssen und 
            // dabei überprüfen ob die Parameter alle richtig gesetzt wurden

            Assert.Fail("Test is not completed yet");
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
