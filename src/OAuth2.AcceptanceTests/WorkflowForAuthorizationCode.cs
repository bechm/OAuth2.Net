using System;
using System.Net;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel.Web;
using NUnit.Framework;
using NUnit.Mocks;

namespace NNS.Authentication.OAuth2.AcceptanceTests
{
    public class WorkflowForAuthorizationCode
    {
        [Test]
        public void CreateServerAndUsersAndGetCorrectRedirect()
        {
            //TODO: WebRequest Mocken

            const string resourceOwnerName = "krik";
            if(!ResourceOwners.ResourceOwnerExists(resourceOwnerName))
                ResourceOwners.Add(resourceOwnerName);
            var resourceOwner = ResourceOwners.GetResourceOwner(resourceOwnerName);

            const string clientId = "268852326492238";
            var authorizationRequestUri = new Uri("http://example.com/AuthorizationRequest");
            var redirectionUri = new Uri("http://example.com/RedirectionUri");
            if(!ServersWithAuthorizationCode.ServerWithAuthorizationCodeExists(clientId, authorizationRequestUri, redirectionUri))
                ServersWithAuthorizationCode.Add(clientId, authorizationRequestUri, redirectionUri);
            var server = ServersWithAuthorizationCode.GetServerWithAuthorizationCode(clientId, authorizationRequestUri, redirectionUri);

            //Da soll es mal hin gehen:
            //if (!resourceOwner.HasValidTokenFor(server))
            //{
            //    context.RedirectToAuthorization(server, resourceOwner)
            //    //incomming Request muss mit Redirect auf LoginSeite versehen werden
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
            // diesen dann mit "Pseudo"-Auth-Code ausstatten, die SetToken(server, token) 
            // und die WebRequest.Autorise(server, resourceOwner) anschubsen
            // dabei müssen die UserCredentials richtig gesetzt sein

            Assert.Fail("Test is not completed yet");
        }
    }
}
