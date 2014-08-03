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
    public partial class MenuClassForm : BaseForm
    {
        private List<int> MenuClassIDList;
        private int SelectedID = 0;
        private int PrevSelectedIndex = 0;

        public MenuClassForm(WebSocket Connection)
        {
            this.Connection = Connection;

            InitializeComponent();
        }

        private void MenuClassForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.MenuClassFormTitle;
            this.BtnAdd.Text = Strings.MenuClassFormBtnAdd;
            this.BtnDelete.Text = Strings.MenuClassFormBtnDelete;

            //获取分类数据
            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_CLASS_LIST));
            ConnHelper.SendString(jsonData.ToString());
        }

        public void ShowData(string jsonStr)
        {
            this.MenuClassList.Items.Clear();
            this.MenuClassList.Items.Add(Strings.MenuClassFormListFirst);
            this.MenuClassList.SelectedIndex = 0;
            
            this.MenuClassIDList = new List<int>();
            this.MenuClassIDList.Add(0);

            JsonTextParser parser = new JsonTextParser();
            JsonObjectCollection jsonData = (JsonObjectCollection)parser.Parse(jsonStr);
                        
            JsonArrayCollection data = (JsonArrayCollection)jsonData["data"];
            for (int i = 0; i < data.Count; i++)
            {
                JsonObjectCollection itemData = (JsonObjectCollection)data[i];

                this.MenuClassList.Items.Add(((JsonStringValue)itemData["name"]).Value);
                this.MenuClassIDList.Add((int)((JsonNumericValue)itemData["id"]).Value);
            }

            //选定上一次所选定的索引
            this.MenuClassList.SelectedIndex = this.PrevSelectedIndex;
        }

        private void MenuClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MenuClassList.SelectedIndex == 0)
            {
                TextName.Text = "";
                BtnAdd.Text = Strings.MenuClassFormBtnAdd;
                this.SelectedID = 0;
                return;
            }

            TextName.Text = this.MenuClassList.SelectedItem.ToString();
            BtnAdd.Text = Strings.MenuClassFormBtnUpdate;
            this.SelectedID = this.MenuClassIDList[this.MenuClassList.SelectedIndex];

            //临时保存上一次选择的索引，以便执行新增或更新后选定该项
            this.PrevSelectedIndex = this.MenuClassList.SelectedIndex;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (this.SelectedID == 0)
                this.PrevSelectedIndex = 1; //新增的话，选定第二项

            JsonObjectCollection data = new JsonObjectCollection();
            data.Add(new JsonNumericValue("id", this.SelectedID));
            data.Add(new JsonStringValue("name", TextName.Text));

            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_CLASS_ADD));
            jsonData.Add(new JsonObjectCollection("data", data));
            ConnHelper.SendString(jsonData.ToString());
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(Strings.MenuClassFormMsg1, Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
            if (res == DialogResult.Cancel)
                return;

            this.SelectedID = this.MenuClassIDList[this.PrevSelectedIndex];

            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_CLASS_DELETE));
            jsonData.Add(new JsonNumericValue("data", this.SelectedID));
            ConnHelper.SendString(jsonData.ToString());

            this.SelectedID = 0;
            this.PrevSelectedIndex = 0;
            TextName.Text = "";
        }

    }
}
