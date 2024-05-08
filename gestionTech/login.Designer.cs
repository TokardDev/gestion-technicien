using System.ComponentModel;

namespace projetSlamTest
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.pwTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "matricule";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // loginTB
            // 
            this.loginTB.Location = new System.Drawing.Point(12, 35);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(170, 20);
            this.loginTB.TabIndex = 1;
            this.loginTB.Text = "FLST012";
            // 
            // pwTB
            // 
            this.pwTB.Location = new System.Drawing.Point(12, 84);
            this.pwTB.Name = "pwTB";
            this.pwTB.PasswordChar = '*';
            this.pwTB.Size = new System.Drawing.Size(170, 20);
            this.pwTB.TabIndex = 3;
            this.pwTB.Text = "MotDePasseDeFou";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "¨mot de passe";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(12, 121);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(170, 36);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "se connecter";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 177);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.pwTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loginTB);
            this.Controls.Add(this.label1);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button loginButton;

        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.TextBox pwTB;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}