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
    public partial class MenuClassForm : Form
    {
        public MenuClassForm()
        {
            InitializeComponent();
        }

        private void MenuClassForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.MenuClassFormTitle;
            this.BtnAdd.Text = Strings.MenuClassFormBtnAdd;
            this.BtnUpdate.Text = Strings.MenuClassFormBtnUpdate;

        }
    }
}
