using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
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
            var scopes = txtScope.Text.Split(',');
            var scopeList = scopes.Select(scope => scope.Trim()).ToList();

            _server = ServersWithAuthorizationCode.Add(txtServerClientId.Text,
                                                       txtClientSharedSecret.Text,
                                                       new Uri(txtServerAuthorizationUri.Text),
                                                       new Uri(txtServerAccessUri.Text),
                                                       new Uri(txtServerRedirectionUri.Text),
                                                       scopeList);
            _server.Version = (Server.OAuthVersion)Enum.Parse(typeof(Server.OAuthVersion), cbVersion.Text);
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
            var mock = new Mock<IIncomingWebRequestContext> {DefaultValue = DefaultValue.Mock};
            var incommingRequest = mock.Object;
            mock.SetupAllProperties();

            incommingRequest.UriTemplateMatch.RequestUri = webBrowser1.Url;

            var tuple = incommingRequest.GetCredentialsFromAuthorizationRedirect();
            _token = Tokens.GetToken(tuple.Item1, tuple.Item2);

            lblAuthorizationCode.Text = _token.AuthorizationCode;

        }

        private void cmdGetToken_Click(object sender, EventArgs e)
        {
            _token.GetAccessAndRefreshToken();
            lblAccessToken.Text = _token.AccessToken;
            lblRefreshToken.Text = _token.RefreshToken;
            lblExpires.Text = _token.Expires.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "XML-Datei (.xml) |*.xml";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            var root = new XElement("root");
            var fileStream = saveFileDialog1.OpenFile();

            foreach (
                var control in
                    Controls.Cast<object>().Where(
                        control => control.GetType().ToString() == "System.Windows.Forms.GroupBox").Cast
                        <GroupBox>()
                        .SelectMany(
                            groupBox =>
                            groupBox.Controls.Cast<Object>().Where(
                                control =>
                                    control.GetType().ToString() == "System.Windows.Forms.TextBox" ||
                                    control.GetType().ToString() == "System.Windows.Forms.ComboBox")))
            {
                switch (control.GetType().ToString())
                {
                    case "System.Windows.Forms.TextBox":
                        {
                            var textBox = (TextBox) control;
                            root.Add(new XElement("TextBox", new XElement("Name", textBox.Name), new XElement("Text", textBox.Text)));
                        }
                        break;
                    case "System.Windows.Forms.ComboBox":
                        {
                            var comboBox = (ComboBox)control;
                            root.Add(new XElement("TextBox", new XElement("Name", comboBox.Name), new XElement("SelectedItem", comboBox.SelectedItem)));
                        }
                        break;
                }
            }
            var write = new StreamWriter(fileStream);
            write.Write(root.ToString());
            write.Close();
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XML-Datei (.xml) |*.xml";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileStream = openFileDialog1.OpenFile();

            var document = XDocument.Load(fileStream);
            var textBoxElements = document.Root.Elements("TextBox");
            foreach (
                var control in
                    Controls.Cast<object>().Where(
                        control => control.GetType().ToString() == "System.Windows.Forms.GroupBox").Cast<GroupBox>()
                        .SelectMany(
                            groupBox =>
                            groupBox.Controls.Cast<Object>().Where(
                                control =>
                                control.GetType().ToString() == "System.Windows.Forms.TextBox" ||
                                control.GetType().ToString() == "System.Windows.Forms.ComboBox")))
            {

                switch (control.GetType().ToString())
                {
                    case "System.Windows.Forms.TextBox":
                        {
                            var textBox = (TextBox) control;
                            foreach (var element in textBoxElements)
                            {
                                if (textBox.Name == element.Element("Name").Value)
                                    textBox.Text = element.Element("Text").Value;
                            }
                        }
                        break;
                    case "System.Windows.Forms.ComboBox":
                        {
                            var comboBox = (ComboBox) control;
                            foreach (var element in textBoxElements)
                            {
                                if (comboBox.Name == element.Element("Name").Value)
                                {
                                    var n = 0;
                                    var selectedIndex = 0;
                                    foreach (var item in comboBox.Items)
                                    {
                                        if(item.ToString() == element.Element("SelectedItem").Value.ToString())
                                            selectedIndex = n;
                                        n++;
                                    }
                                    comboBox.SelectedIndex = selectedIndex;
                                }
                            }
                        }
                        break;
                }

            }
            fileStream.Close();
        }

        private void OAuthServerWithAuthorizationCode_Load(object sender, EventArgs e)
        {
            cbVersion.Items.Clear();
            foreach (var versionNumber in Enum.GetValues(typeof (Server.OAuthVersion)))
                cbVersion.Items.Add(versionNumber);
            cbVersion.SelectedIndex = 0;
        }
    }

}
