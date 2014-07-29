namespace InformationDesk
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.LabContent = new System.Windows.Forms.Label();
            this.LabGitLink = new System.Windows.Forms.LinkLabel();
            this.LabGitHub = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabContent
            // 
            this.LabContent.AutoSize = true;
            this.LabContent.Location = new System.Drawing.Point(42, 30);
            this.LabContent.Name = "LabContent";
            this.LabContent.Size = new System.Drawing.Size(41, 12);
            this.LabContent.TabIndex = 0;
            this.LabContent.Text = "label1";
            // 
            // LabGitLink
            // 
            this.LabGitLink.AutoSize = true;
            this.LabGitLink.Location = new System.Drawing.Point(89, 65);
            this.LabGitLink.Name = "LabGitLink";
            this.LabGitLink.Size = new System.Drawing.Size(239, 12);
            this.LabGitLink.TabIndex = 1;
            this.LabGitLink.TabStop = true;
            this.LabGitLink.Text = "https://github.com/ouzhigang/OzgMenuSys";
            this.LabGitLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LabGitLink_LinkClicked);
            // 
            // LabGitHub
            // 
            this.LabGitHub.AutoSize = true;
            this.LabGitHub.Location = new System.Drawing.Point(42, 65);
            this.LabGitHub.Name = "LabGitHub";
            this.LabGitHub.Size = new System.Drawing.Size(41, 12);
            this.LabGitHub.TabIndex = 2;
            this.LabGitHub.Text = "label1";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 106);
            this.Controls.Add(this.LabGitHub);
            this.Controls.Add(this.LabGitLink);
            this.Controls.Add(this.LabContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "AboutForm";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabContent;
        private System.Windows.Forms.LinkLabel LabGitLink;
        private System.Windows.Forms.Label LabGitHub;
    }
}