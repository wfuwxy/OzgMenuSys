using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace InformationDesk
{
    public partial class MenuBigImgViewForm : BaseForm
    {
        public string BigImagePath = null;        
        public int MenuID = 0;

        private Image BigImg;
        private MemoryStream BigImgMemoryStream;

        public MenuBigImgViewForm(WebSocket Connection)
        {
            this.Connection = Connection;
            this.BigImg = null;
            this.BigImgMemoryStream = null;

            InitializeComponent();
        }

        ~MenuBigImgViewForm()
        {
            if (this.BigImg != null)
            {
                this.BigImg.Dispose();
                this.BigImg = null;
            }

            if (this.BigImgMemoryStream != null)
            {
                this.BigImgMemoryStream.Close();
                this.BigImgMemoryStream = null;
            }

        }
                
        private void MenuBigImgViewForm_Load(object sender, EventArgs e)
        {
            if(this.MenuID == 0)
            {
                //本地路径
                BigImagePictureBox.Image = new Bitmap(this.BigImagePath);
            }
            else
            {
                //远程路径
                JsonObjectCollection jsonData = new JsonObjectCollection();
                jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_BIG_IMAGE));
                jsonData.Add(new JsonNumericValue("data", this.MenuID));
                ConnHelper.SendString(jsonData.ToString());
            }

        }

        public void ShowData(string jsonStr)
        {
            JsonTextParser parser = new JsonTextParser();
            JsonObjectCollection jsonData = (JsonObjectCollection)parser.Parse(jsonStr);

            if (((JsonNumericValue)jsonData["ok"]).Value == 1)
            {
                JsonObjectCollection data = (JsonObjectCollection)jsonData["data"];

                string imgBase64Str = ((JsonStringValue)data["img_base64str"]).Value;
                byte[] imageBytes = Convert.FromBase64String(imgBase64Str);

                this.BigImgMemoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                this.BigImgMemoryStream.Write(imageBytes, 0, imageBytes.Length);
                this.BigImg = Image.FromStream(this.BigImgMemoryStream);
                BigImagePictureBox.Image = this.BigImg;                                
            }
        }

    }
}
