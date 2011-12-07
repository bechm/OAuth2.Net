using System;
using System.Collections.Specialized;
using System.Net;
using System.ServiceModel.Web;
using System.Windows.Forms;
using Moq;
using NNS.Authentication.OAuth2;
using NNS.Authentication.OAuth2.Extensions;

namespace NNS.Authentication.OAuth2.TestClient
{
    public partial class OAuthServerWithAuthorizationCode : UserControl
    {
        private Token _token;
        private ServerWithAuthorizationCode _server;
        private ResourceOwner _resourceOwner;

        public OAuthServerWithAuthorizationCode()
        {
            InitializeComponent();
        }

        private void CmdServerCreateClick(object sender, EventArgs e)
        {
            _server = ServersWithAuthorizationCode.Add(txtServerClientId.Text,
                                                       new Uri(txtServerAuthorizationUri.Text),
                                                       new Uri(txtServerRedirectionUri.Text));
            lblServerGUID.Text = _server.Guid.ToString();
        }

        private void CmdResourceOwnerCreateClick(object sender, EventArgs e)
        {
            _resourceOwner = ResourceOwners.Add(txtResourceOwnerName.Text);
            lblResourceOwnerGUID.Text = _resourceOwner.Guid.ToString();
        }

        private void CmdAuthorizationCodeRedirectClick(object sender, EventArgs e)
        {
            var mock = new Mock<IOutgoingWebResponseContext>() {};
            var outgoingResponse = mock.Object;
            mock.SetupAllProperties();

            outgoingResponse.RedirectToAuthorization(_server, _resourceOwner);

            if (outgoingResponse.StatusCode == HttpStatusCode.Redirect)
            {
                webBrowser1.Navigate(outgoingResponse.Location);
            }
            else
            {
                MessageBox.Show("an error occured, no redirect");
            }

        }

        private void CmdGetAuthorizationCodeClick(object sender, EventArgs e)
        {
            var mock = new Mock<IIncomingWebRequestContext> { DefaultValue = DefaultValue.Mock };
            var incommingRequest = mock.Object;
            mock.SetupAllProperties();

            incommingRequest.UriTemplateMatch.RequestUri = webBrowser1.Url;

            var tuple = incommingRequest.GetCredentialsFromAuthorizationRedirect();
            var token = Tokens.GetToken(tuple.Item1, tuple.Item2);

            lblAuthorizationCode.Text = token.AuthorizationCode;

        }
    }
}
