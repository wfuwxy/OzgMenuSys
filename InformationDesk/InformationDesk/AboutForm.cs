using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InformationDesk
{
    public partial class AboutForm : BaseForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.AboutFormTitle;
            this.LabContent.Text = String.Format(Strings.AboutFormLabContent, Application.ProductVersion);
            this.LabGitHub.Text = Strings.AboutFormLabGitHub; 

        }

        private void LabGitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start(((LinkLabel)sender).Text);
        }

    }
}
