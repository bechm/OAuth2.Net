namespace NNS.Authentication.OAuth2.TestClient
{
    partial class OAuthServerWithAuthorizationCode
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbServer = new System.Windows.Forms.GroupBox();
            this.lblServerGUID = new System.Windows.Forms.Label();
            this.cmdServerCreate = new System.Windows.Forms.Button();
            this.txtServerAuthorizationUri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerRedirectionUri = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerClientId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbResourceOwner = new System.Windows.Forms.GroupBox();
            this.lblResourceOwnerGUID = new System.Windows.Forms.Label();
            this.cmdResourceOwnerCreate = new System.Windows.Forms.Button();
            this.txtResourceOwnerName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbWorkflow = new System.Windows.Forms.GroupBox();
            this.cmdGetAuthorizationCode = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lblAuthorizationCode = new System.Windows.Forms.Label();
            this.cmdAuthorizationCodeRedirect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblExpires = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRefreshToken = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAccessToken = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdGetToken = new System.Windows.Forms.Button();
            this.txtClientSharedSecret = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbServer.SuspendLayout();
            this.gbResourceOwner.SuspendLayout();
            this.gbWorkflow.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbServer
            // 
            this.gbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbServer.Controls.Add(this.txtClientSharedSecret);
            this.gbServer.Controls.Add(this.label6);
            this.gbServer.Controls.Add(this.lblServerGUID);
            this.gbServer.Controls.Add(this.cmdServerCreate);
            this.gbServer.Controls.Add(this.txtServerAuthorizationUri);
            this.gbServer.Controls.Add(this.label3);
            this.gbServer.Controls.Add(this.txtServerRedirectionUri);
            this.gbServer.Controls.Add(this.label2);
            this.gbServer.Controls.Add(this.txtServerClientId);
            this.gbServer.Controls.Add(this.label1);
            this.gbServer.Location = new System.Drawing.Point(3, 3);
            this.gbServer.Name = "gbServer";
            this.gbServer.Size = new System.Drawing.Size(635, 162);
            this.gbServer.TabIndex = 0;
            this.gbServer.TabStop = false;
            this.gbServer.Text = "Server";
            // 
            // lblServerGUID
            // 
            this.lblServerGUID.AutoSize = true;
            this.lblServerGUID.Location = new System.Drawing.Point(125, 133);
            this.lblServerGUID.Name = "lblServerGUID";
            this.lblServerGUID.Size = new System.Drawing.Size(25, 13);
            this.lblServerGUID.TabIndex = 7;
            this.lblServerGUID.Text = "???";
            // 
            // cmdServerCreate
            // 
            this.cmdServerCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdServerCreate.Location = new System.Drawing.Point(554, 133);
            this.cmdServerCreate.Name = "cmdServerCreate";
            this.cmdServerCreate.Size = new System.Drawing.Size(75, 23);
            this.cmdServerCreate.TabIndex = 6;
            this.cmdServerCreate.Text = "create";
            this.cmdServerCreate.UseVisualStyleBackColor = true;
            this.cmdServerCreate.Click += new System.EventHandler(this.CmdServerCreateClick);
            // 
            // txtServerAuthorizationUri
            // 
            this.txtServerAuthorizationUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerAuthorizationUri.Location = new System.Drawing.Point(128, 102);
            this.txtServerAuthorizationUri.Name = "txtServerAuthorizationUri";
            this.txtServerAuthorizationUri.Size = new System.Drawing.Size(501, 20);
            this.txtServerAuthorizationUri.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "AuthorizationRequestUri";
            // 
            // txtServerRedirectionUri
            // 
            this.txtServerRedirectionUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerRedirectionUri.Location = new System.Drawing.Point(128, 76);
            this.txtServerRedirectionUri.Name = "txtServerRedirectionUri";
            this.txtServerRedirectionUri.Size = new System.Drawing.Size(501, 20);
            this.txtServerRedirectionUri.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "RedirectionUri";
            // 
            // txtServerClientId
            // 
            this.txtServerClientId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerClientId.Location = new System.Drawing.Point(128, 17);
            this.txtServerClientId.Name = "txtServerClientId";
            this.txtServerClientId.Size = new System.Drawing.Size(501, 20);
            this.txtServerClientId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ClientID";
            // 
            // gbResourceOwner
            // 
            this.gbResourceOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResourceOwner.Controls.Add(this.lblResourceOwnerGUID);
            this.gbResourceOwner.Controls.Add(this.cmdResourceOwnerCreate);
            this.gbResourceOwner.Controls.Add(this.txtResourceOwnerName);
            this.gbResourceOwner.Controls.Add(this.label4);
            this.gbResourceOwner.Location = new System.Drawing.Point(4, 171);
            this.gbResourceOwner.Name = "gbResourceOwner";
            this.gbResourceOwner.Size = new System.Drawing.Size(634, 70);
            this.gbResourceOwner.TabIndex = 1;
            this.gbResourceOwner.TabStop = false;
            this.gbResourceOwner.Text = "ResourceOwner";
            // 
            // lblResourceOwnerGUID
            // 
            this.lblResourceOwnerGUID.AutoSize = true;
            this.lblResourceOwnerGUID.Location = new System.Drawing.Point(124, 44);
            this.lblResourceOwnerGUID.Name = "lblResourceOwnerGUID";
            this.lblResourceOwnerGUID.Size = new System.Drawing.Size(25, 13);
            this.lblResourceOwnerGUID.TabIndex = 8;
            this.lblResourceOwnerGUID.Text = "???";
            // 
            // cmdResourceOwnerCreate
            // 
            this.cmdResourceOwnerCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdResourceOwnerCreate.Location = new System.Drawing.Point(553, 39);
            this.cmdResourceOwnerCreate.Name = "cmdResourceOwnerCreate";
            this.cmdResourceOwnerCreate.Size = new System.Drawing.Size(75, 23);
            this.cmdResourceOwnerCreate.TabIndex = 7;
            this.cmdResourceOwnerCreate.Text = "create";
            this.cmdResourceOwnerCreate.UseVisualStyleBackColor = true;
            this.cmdResourceOwnerCreate.Click += new System.EventHandler(this.CmdResourceOwnerCreateClick);
            // 
            // txtResourceOwnerName
            // 
            this.txtResourceOwnerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResourceOwnerName.Location = new System.Drawing.Point(127, 13);
            this.txtResourceOwnerName.Name = "txtResourceOwnerName";
            this.txtResourceOwnerName.Size = new System.Drawing.Size(501, 20);
            this.txtResourceOwnerName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "ResourceOwnerName";
            // 
            // gbWorkflow
            // 
            this.gbWorkflow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWorkflow.Controls.Add(this.cmdGetAuthorizationCode);
            this.gbWorkflow.Controls.Add(this.webBrowser1);
            this.gbWorkflow.Controls.Add(this.lblAuthorizationCode);
            this.gbWorkflow.Controls.Add(this.cmdAuthorizationCodeRedirect);
            this.gbWorkflow.Location = new System.Drawing.Point(4, 247);
            this.gbWorkflow.Name = "gbWorkflow";
            this.gbWorkflow.Size = new System.Drawing.Size(634, 266);
            this.gbWorkflow.TabIndex = 2;
            this.gbWorkflow.TabStop = false;
            this.gbWorkflow.Text = "Workflow";
            // 
            // cmdGetAuthorizationCode
            // 
            this.cmdGetAuthorizationCode.Location = new System.Drawing.Point(188, 20);
            this.cmdGetAuthorizationCode.Name = "cmdGetAuthorizationCode";
            this.cmdGetAuthorizationCode.Size = new System.Drawing.Size(170, 23);
            this.cmdGetAuthorizationCode.TabIndex = 3;
            this.cmdGetAuthorizationCode.Text = "AuthorizationCode Read";
            this.cmdGetAuthorizationCode.UseVisualStyleBackColor = true;
            this.cmdGetAuthorizationCode.Click += new System.EventHandler(this.CmdGetAuthorizationCodeClick);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(9, 62);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(619, 198);
            this.webBrowser1.TabIndex = 2;
            // 
            // lblAuthorizationCode
            // 
            this.lblAuthorizationCode.AutoSize = true;
            this.lblAuthorizationCode.Location = new System.Drawing.Point(16, 46);
            this.lblAuthorizationCode.Name = "lblAuthorizationCode";
            this.lblAuthorizationCode.Size = new System.Drawing.Size(25, 13);
            this.lblAuthorizationCode.TabIndex = 1;
            this.lblAuthorizationCode.Text = "???";
            // 
            // cmdAuthorizationCodeRedirect
            // 
            this.cmdAuthorizationCodeRedirect.Location = new System.Drawing.Point(9, 20);
            this.cmdAuthorizationCodeRedirect.Name = "cmdAuthorizationCodeRedirect";
            this.cmdAuthorizationCodeRedirect.Size = new System.Drawing.Size(170, 23);
            this.cmdAuthorizationCodeRedirect.TabIndex = 0;
            this.cmdAuthorizationCodeRedirect.Text = "AuthorizationCode Redirect";
            this.cmdAuthorizationCodeRedirect.UseVisualStyleBackColor = true;
            this.cmdAuthorizationCodeRedirect.Click += new System.EventHandler(this.CmdAuthorizationCodeRedirectClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblExpires);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblRefreshToken);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblAccessToken);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cmdGetToken);
            this.groupBox1.Location = new System.Drawing.Point(4, 519);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 79);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // lblExpires
            // 
            this.lblExpires.AutoSize = true;
            this.lblExpires.Location = new System.Drawing.Point(185, 55);
            this.lblExpires.Name = "lblExpires";
            this.lblExpires.Size = new System.Drawing.Size(25, 13);
            this.lblExpires.TabIndex = 11;
            this.lblExpires.Text = "???";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(106, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Expires";
            // 
            // lblRefreshToken
            // 
            this.lblRefreshToken.AutoSize = true;
            this.lblRefreshToken.Location = new System.Drawing.Point(185, 37);
            this.lblRefreshToken.Name = "lblRefreshToken";
            this.lblRefreshToken.Size = new System.Drawing.Size(25, 13);
            this.lblRefreshToken.TabIndex = 9;
            this.lblRefreshToken.Text = "???";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "RefreshToken";
            // 
            // lblAccessToken
            // 
            this.lblAccessToken.AutoSize = true;
            this.lblAccessToken.Location = new System.Drawing.Point(185, 19);
            this.lblAccessToken.Name = "lblAccessToken";
            this.lblAccessToken.Size = new System.Drawing.Size(25, 13);
            this.lblAccessToken.TabIndex = 7;
            this.lblAccessToken.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "AccessToken";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Refresh Token";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmdGetToken
            // 
            this.cmdGetToken.Location = new System.Drawing.Point(9, 19);
            this.cmdGetToken.Name = "cmdGetToken";
            this.cmdGetToken.Size = new System.Drawing.Size(91, 23);
            this.cmdGetToken.TabIndex = 4;
            this.cmdGetToken.Text = "Get Token";
            this.cmdGetToken.UseVisualStyleBackColor = true;
            // 
            // txtClientSharedSecret
            // 
            this.txtClientSharedSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientSharedSecret.Location = new System.Drawing.Point(128, 43);
            this.txtClientSharedSecret.Name = "txtClientSharedSecret";
            this.txtClientSharedSecret.Size = new System.Drawing.Size(501, 20);
            this.txtClientSharedSecret.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "ClientSharedSecret";
            // 
            // OAuthServerWithAuthorizationCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbWorkflow);
            this.Controls.Add(this.gbResourceOwner);
            this.Controls.Add(this.gbServer);
            this.Name = "OAuthServerWithAuthorizationCode";
            this.Size = new System.Drawing.Size(641, 601);
            this.gbServer.ResumeLayout(false);
            this.gbServer.PerformLayout();
            this.gbResourceOwner.ResumeLayout(false);
            this.gbResourceOwner.PerformLayout();
            this.gbWorkflow.ResumeLayout(false);
            this.gbWorkflow.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerAuthorizationUri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerRedirectionUri;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerClientId;
        private System.Windows.Forms.Button cmdServerCreate;
        private System.Windows.Forms.GroupBox gbResourceOwner;
        private System.Windows.Forms.TextBox txtResourceOwnerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdResourceOwnerCreate;
        private System.Windows.Forms.GroupBox gbWorkflow;
        private System.Windows.Forms.Label lblAuthorizationCode;
        private System.Windows.Forms.Button cmdAuthorizationCodeRedirect;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblExpires;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRefreshToken;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAccessToken;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdGetToken;
        private System.Windows.Forms.Button cmdGetAuthorizationCode;
        private System.Windows.Forms.Label lblServerGUID;
        private System.Windows.Forms.Label lblResourceOwnerGUID;
        private System.Windows.Forms.TextBox txtClientSharedSecret;
        private System.Windows.Forms.Label label6;
    }
}
