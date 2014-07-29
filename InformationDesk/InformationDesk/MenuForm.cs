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
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.MenuFormTitle;
            this.LabMenuClass.Text = Strings.MenuFormLabMenuClass;
            this.BtnAdd.Text = Strings.MenuFormBtnAdd;

            //设置行高
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 25);
            this.MenuDataList.SmallImageList = imageList;

            this.MenuDataList.Columns.Add("", 0);
            this.MenuDataList.Columns.Add("名称", 100).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MenuDataList.Columns.Add("价格", 100).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MenuDataList.Columns.Add("类别", 100).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MenuDataList.Columns.Add("时间", 100).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            //test
            ListViewItem[] p = new ListViewItem[4];
            p[0] = new ListViewItem(new string[] { "", "测试菜式1", "5.0元", "分类1", "2014-01-01" });
            p[1] = new ListViewItem(new string[] { "", "测试菜式2", "5.0元", "分类1", "2014-01-01" });
            p[2] = new ListViewItem(new string[] { "", "测试菜式3", "5.0元", "分类1", "2014-01-01" });
            p[3] = new ListViewItem(new string[] { "", "测试菜式4", "5.0元", "分类1", "2014-01-01" });
            this.MenuDataList.Items.AddRange(p);

        }
    }
}
