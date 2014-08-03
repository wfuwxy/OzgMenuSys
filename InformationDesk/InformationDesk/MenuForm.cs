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
    public partial class MenuForm : BaseForm
    {
        public Form SubForm = null;

        private JsonArrayCollection MenuData;
        private JsonArrayCollection MenuClassData;
        private List<int> MenuIDList = null;
        
        //选定上一次所选定的索引
        //private int PrevSelectedIndex = 0;

        public MenuForm(WebSocket Connection)
        {
            this.Connection = Connection;

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
            this.MenuDataList.Columns.Add(Strings.MenuFormListTitle1, 180).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MenuDataList.Columns.Add(Strings.MenuFormListTitle2, 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MenuDataList.Columns.Add(Strings.MenuFormListTitle3, 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MenuDataList.Columns.Add(Strings.MenuFormListTitle4, 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            
            //获取分类数据
            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_CLASS_LIST));
            ConnHelper.SendString(jsonData.ToString());
        }

        public void ShowData(string jsonStr)
        {
            JsonTextParser parser = new JsonTextParser();
            JsonObjectCollection jsonData = (JsonObjectCollection)parser.Parse(jsonStr);

            if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_MENU_CLASS))
            {
                this.MenuClassData = (JsonArrayCollection)jsonData["data"];

                this.MenuClassList.Items.Clear();
                this.MenuClassList.Items.Add(Strings.MenuFormListFirst);
                this.MenuClassList.SelectedIndex = 0;

                this.MenuIDList = new List<int>();
                this.MenuIDList.Add(0);

                for (int i = 0; i < this.MenuClassData.Count; i++)
                {
                    JsonObjectCollection itemData = (JsonObjectCollection)this.MenuClassData[i];

                    this.MenuClassList.Items.Add(((JsonStringValue)itemData["name"]).Value);
                    this.MenuIDList.Add((int)((JsonNumericValue)itemData["id"]).Value);
                }

                //选定上一次所选定的索引
                //this.MenuClassList.SelectedIndex = this.PrevSelectedIndex;                                
            }
            else if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_MENU))
            {
                this.MenuData = (JsonArrayCollection)jsonData["data"];

                this.MenuDataList.Items.Clear();
                for (int i = 0; i < this.MenuData.Count; i++)
                {
                    JsonObjectCollection itemData = (JsonObjectCollection)this.MenuData[i];

                    ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonNumericValue)itemData["price"]).Value.ToString("f1"), ((JsonStringValue)itemData["mc_name"]).Value, Commons.UnixTimeFrom((long)((JsonNumericValue)itemData["add_time"]).Value).ToString("yyyy-MM-dd") });
                    this.MenuDataList.Items.Add(item);
                }
            }

        }

        public void MenuClassListSelectedWithID(int menuClassID)
        {
            for (int i = 0; i < this.MenuIDList.Count; i++)
            {
                if (this.MenuIDList[i] == menuClassID)
                {
                    this.MenuClassList.SelectedIndex = i;

                    JsonObjectCollection jsonData = new JsonObjectCollection();
                    jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_LIST));
                    jsonData.Add(new JsonNumericValue("data", this.MenuIDList[this.MenuClassList.SelectedIndex]));
                    ConnHelper.SendString(jsonData.ToString());
                    break;
                }
            }
        }

        private void MenuClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MenuIDList != null)
            {
                //发出菜单数据的请求
                if (this.MenuClassList.SelectedIndex > 0)
                {
                    JsonObjectCollection jsonData = new JsonObjectCollection();
                    jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_LIST));
                    jsonData.Add(new JsonNumericValue("data", this.MenuIDList[this.MenuClassList.SelectedIndex]));
                    ConnHelper.SendString(jsonData.ToString());
                }
            }
            
        }

        private void MenuDataList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.MenuDataList.SelectedItems.Count >= 1)
                {
                    JsonObjectCollection itemData = (JsonObjectCollection)this.MenuData[this.MenuDataList.SelectedItems[0].Index];
                    this.SubForm = new MenuAddForm(this.Connection);
                    ((MenuAddForm)this.SubForm).MenuID = (int)((JsonNumericValue)itemData["id"]).Value;
                    ((MenuAddForm)this.SubForm).MenuName = ((JsonStringValue)itemData["name"]).Value;
                    ((MenuAddForm)this.SubForm).MenuClassID = (int)((JsonNumericValue)itemData["class_id"]).Value;
                    ((MenuAddForm)this.SubForm).MenuClassName = ((JsonStringValue)itemData["mc_name"]).Value;
                    ((MenuAddForm)this.SubForm).MenuPrice = (float)((JsonNumericValue)itemData["price"]).Value;
                    ((MenuAddForm)this.SubForm).MenuBigImage = ((JsonStringValue)itemData["big_img"]).Value;
                    ((MenuAddForm)this.SubForm).MenuClassData = this.MenuClassData;

                    for (int i = 0; i < this.MenuClassData.Count; i++)
                    {
                        JsonObjectCollection menuClassDataItem = (JsonObjectCollection)this.MenuClassData[i];
                       
                        if (((JsonStringValue)menuClassDataItem["name"]).Value.Equals(this.MenuDataList.SelectedItems[0].SubItems[3].Text))
                        {
                            ((MenuAddForm)this.SubForm).MenuClassSelectedIndex = i + 1; //+1是因为显示列表已经存在了第一个的默认项
                            break;
                        }
                    }

                    this.SubForm.ShowDialog();
                }                
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            this.SubForm = new MenuAddForm(this.Connection);
            ((MenuAddForm)this.SubForm).MenuClassData = this.MenuClassData;
            ((MenuAddForm)this.SubForm).MenuClassSelectedIndex = this.MenuClassList.SelectedIndex;
            this.SubForm.ShowDialog();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MenuDataList.SelectedIndices.Count > 0)
            {
                DialogResult res = MessageBox.Show(Strings.ClientFormMsg3, Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                    return;

                JsonObjectCollection itemData = (JsonObjectCollection)this.MenuData[this.MenuDataList.SelectedItems[0].Index];

                JsonObjectCollection jsonData = new JsonObjectCollection();
                jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_DELETE));
                jsonData.Add(new JsonNumericValue("data", (int)((JsonNumericValue)itemData["id"]).Value));
                ConnHelper.SendString(jsonData.ToString());
            }
        }

    }
}
