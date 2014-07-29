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
    public partial class OrderReportForm : Form
    {
        public OrderReportForm()
        {
            InitializeComponent();
        }

        private void OrderReportForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.OrderReportFormTitle;

        }
    }
}
