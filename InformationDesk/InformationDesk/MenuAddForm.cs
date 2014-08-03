using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace InformationDesk
{
    public partial class MenuAddForm : BaseForm
    {
        public int MenuID = 0;
        public string MenuName;
        public int MenuClassID = 0;
        public string MenuClassName;
        public int MenuClassSelectedIndex = 0;
        public float MenuPrice;
        public string MenuBigImage;
        public JsonArrayCollection MenuClassData = null;

        public Form SubForm = null;

        private bool IsSelectedImage = false;
        
        public MenuAddForm(WebSocket Connection)
        {
            this.Connection = Connection;

            InitializeComponent();
        }

        private void MenuAddForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.MenuAddFormTitle;

            if (this.MenuClassData != null)
            {
                this.MenuClassList.Items.Clear();
                this.MenuClassList.Items.Add(Strings.MenuAddFormClassListFirst);

                for (int i = 0; i < this.MenuClassData.Count; i++)
                {
                    JsonObjectCollection itemData = (JsonObjectCollection)this.MenuClassData[i];

                    this.MenuClassList.Items.Add(((JsonStringValue)itemData["name"]).Value);
                }

                this.MenuClassList.SelectedIndex = this.MenuClassSelectedIndex;
            }

            TextPrice.Text = "0.0";

            if (this.MenuID > 0)
            {
                //编辑
                TextName.Text = this.MenuName;
                TextPrice.Text = this.MenuPrice.ToString("f1");
                TextBigImage.Text = this.MenuBigImage;
                BtnAdd.Text = Strings.MenuAddFormBtnUpdate;

            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (this.MenuClassList.SelectedIndex == 0)
            {
                MessageBox.Show(Strings.MenuAddFormMsg2);
                return;
            }

            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_MENU_ADD));

            JsonObjectCollection data = new JsonObjectCollection();

            JsonObjectCollection menuData = new JsonObjectCollection();
            menuData.Add(new JsonNumericValue("id", this.MenuID));
            menuData.Add(new JsonStringValue("name", TextName.Text));
            menuData.Add(new JsonNumericValue("price", float.Parse(TextPrice.Text)));

            JsonObjectCollection itemData = (JsonObjectCollection)this.MenuClassData[this.MenuClassList.SelectedIndex -1];
            menuData.Add(new JsonNumericValue("class_id", ((JsonNumericValue)itemData["id"]).Value));
            
            data.Add(new JsonObjectCollection("menu_data", menuData));

            if (this.IsSelectedImage && TextBigImage.Text != "" && File.Exists(TextBigImage.Text))
            {
                FileStream fs = File.OpenRead(TextBigImage.Text);
                BinaryReader br = new BinaryReader(fs);
                byte[] bt = br.ReadBytes(Convert.ToInt32(fs.Length));
                string imgBase64Str = Convert.ToBase64String(bt);
                br.Close();
                fs.Close();

                data.Add(new JsonStringValue("img_base64str", imgBase64Str));
            }
            
            jsonData.Add(new JsonObjectCollection("data", data));
            ConnHelper.SendString(jsonData.ToString());
            this.Close();
        }

        private void BtnViewBigImage_Click(object sender, EventArgs e)
        {
            if (TextBigImage.Text == "")
            {
                MessageBox.Show(Strings.MenuAddFormMsg1);
                return;
            }

            this.SubForm = new MenuBigImgViewForm(this.Connection);

            if (this.MenuID == 0)
                ((MenuBigImgViewForm)this.SubForm).BigImagePath = TextBigImage.Text;
            else
                ((MenuBigImgViewForm)this.SubForm).MenuID = this.MenuID;

            this.SubForm.ShowDialog();

        }

        private void BtnSelectBigImage_Click(object sender, EventArgs e)
        {
            this.BigImageOpenFileDialog.ShowDialog();

        }

        private void BigImageOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            this.IsSelectedImage = true;
            TextBigImage.Text = BigImageOpenFileDialog.FileName;
        }

    }
}
