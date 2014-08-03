using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace InformationDesk
{
    public partial class ClientForm : BaseForm
    {
        private List<int> ClientIDList;
        private int SelectedID = 0;

        public ClientForm(WebSocket Connection)
        {
            this.Connection = Connection;

            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.ClientFormTitle;

            //设置行高
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 25);
            this.ClientList.SmallImageList = imageList;

            this.ClientList.Columns.Add("", 0);
            this.ClientList.Columns.Add(Strings.ClientFormListTitle1, 150).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ClientList.Columns.Add(Strings.ClientFormListTitle2, 180).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ClientList.Columns.Add(Strings.ClientFormListTitle3, 150).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            //获取客户端列表数据
            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_CLIENT_LIST));
            ConnHelper.SendString(jsonData.ToString());
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!Commons.IsIPAddress(TextIP.Text))
            {
                MessageBox.Show(Strings.ClientFormMsg1);
                return;
            }

            if (this.SelectedID > 0)
            {
                DialogResult res = MessageBox.Show(Strings.ClientFormMsg2, Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                    return;
            }

            JsonObjectCollection data = new JsonObjectCollection();
            data.Add(new JsonNumericValue("id", this.SelectedID));
            data.Add(new JsonStringValue("name", TextName.Text));
            data.Add(new JsonStringValue("ip", TextIP.Text));

            if(ChkIsAdmin1.Checked)
                data.Add(new JsonNumericValue("is_admin", 1));
            else
                data.Add(new JsonNumericValue("is_admin", 0));

            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_CLIENT_ADD));
            jsonData.Add(new JsonObjectCollection("data", data));
            ConnHelper.SendString(jsonData.ToString());
        }

        public void ShowData(string jsonStr)
        {
            this.ClientList.Items.Clear();
            this.ClientIDList = new List<int>();

            JsonTextParser parser = new JsonTextParser();
            JsonObjectCollection jsonData = (JsonObjectCollection)parser.Parse(jsonStr);

            JsonArrayCollection data = (JsonArrayCollection)jsonData["data"];
            for (int i = 0; i < data.Count; i++)
            {
                JsonObjectCollection itemData = (JsonObjectCollection)data[i];

                string isAdmin = Strings.CommonsClientIsAdmin0;
                if (((JsonNumericValue)itemData["is_admin"]).Value == 1)
                    isAdmin = Strings.CommonsClientIsAdmin1;

                ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonStringValue)itemData["ip"]).Value, isAdmin });
                this.ClientList.Items.Add(item);

                this.ClientIDList.Add((int)((JsonNumericValue)itemData["id"]).Value);
            }
        }

        private void ClientList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.ClientList.SelectedItems.Count >= 1)
                {
                    //修改状态
                    TextName.Text = this.ClientList.SelectedItems[0].SubItems[1].Text;
                    TextIP.Text = this.ClientList.SelectedItems[0].SubItems[2].Text;

                    if (this.ClientList.SelectedItems[0].SubItems[3].Text.Equals(Strings.CommonsClientIsAdmin0))
                        ChkIsAdmin0.Checked = true;
                    else
                        ChkIsAdmin1.Checked = true;

                    this.SelectedID = this.ClientIDList[this.ClientList.SelectedItems[0].Index];

                    BtnAdd.Text = Strings.ClientFormBtnUpdate;
                }
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ClientList.SelectedIndices.Count > 0)
            {
                DialogResult res = MessageBox.Show(Strings.ClientFormMsg3, Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                    return;

                this.SelectedID = this.ClientIDList[this.ClientList.SelectedItems[0].Index];

                JsonObjectCollection jsonData = new JsonObjectCollection();
                jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_CLIENT_DELETE));
                jsonData.Add(new JsonNumericValue("data", this.SelectedID));
                ConnHelper.SendString(jsonData.ToString());

                this.SelectedID = 0;
                TextName.Text = "";
                TextIP.Text = "";
                ChkIsAdmin0.Checked = true;
                BtnAdd.Text = Strings.ClientFormBtnAdd;
            }
        }

    }
}
