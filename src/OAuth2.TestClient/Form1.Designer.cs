namespace NNS.Authentication.OAuth2.TestClient
{
    partial class Form1
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.oAuthServerWithAuthorizationCode1 = new OAuthServerWithAuthorizationCode();
            this.SuspendLayout();
            // 
            // oAuthServerWithAuthorizationCode1
            // 
            this.oAuthServerWithAuthorizationCode1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oAuthServerWithAuthorizationCode1.Location = new System.Drawing.Point(0, 0);
            this.oAuthServerWithAuthorizationCode1.Name = "oAuthServerWithAuthorizationCode1";
            this.oAuthServerWithAuthorizationCode1.Size = new System.Drawing.Size(729, 606);
            this.oAuthServerWithAuthorizationCode1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 606);
            this.Controls.Add(this.oAuthServerWithAuthorizationCode1);
            this.Name = "Form1";
            this.Text = "NNS OAuth2.0 TestClient";
            this.ResumeLayout(false);

        }

        #endregion

        private OAuthServerWithAuthorizationCode oAuthServerWithAuthorizationCode1;
    }
}

