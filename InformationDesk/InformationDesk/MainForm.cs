using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Json;
using WebSocket4Net;

namespace InformationDesk
{
    public partial class MainForm : Form
    {
        private WebSocket Connection;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.MainFormTitle;

            this.Connection = ConnHelper.getConnInstance(this);

            //设置行高
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 25);
            this.OnlineList.SmallImageList = imageList;

            this.OnlineList.Columns.Add("", 0);
            this.OnlineList.Columns.Add("名称", 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OnlineList.Columns.Add("IP", 160).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;            
            this.OnlineList.Columns.Add("类别", 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OnlineList.Columns.Add("状态", 160).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();

        }

        private void MenuClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuClassForm menuClassForm = new MenuClassForm();
            menuClassForm.ShowDialog();

        }

        private void MenuDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.ShowDialog();

        }

        private void ClientListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientForm clientForm = new ClientForm();
            clientForm.ShowDialog();

        }

        private void OrderReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderReportForm orderReportForm = new OrderReportForm();
            orderReportForm.ShowDialog();

        }

        public void WebSocket_Opened(object sender, EventArgs e)
        {
            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_CHK_CLIENT));
            this.Connection.Send(jsonData.ToString());
        }

        public void WebSocket_Closed(object sender, EventArgs e)
        {

        }

        public void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            JsonTextParser parser = new JsonTextParser();
            JsonObjectCollection jsonData = (JsonObjectCollection)parser.Parse(e.Message);
            if (((JsonNumericValue)jsonData["ok"]).Value == 1)
            {
                if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_TOMAIN))
                {
                    //发送获取在线列表的请求
                    jsonData = new JsonObjectCollection();
                    jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_ONLINE_LIST));
                    this.Connection.Send(jsonData.ToString());
                }
                else if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_ONLINE_LIST))
                { 
                    //显示在线列表
                    JsonArrayCollection data = (JsonArrayCollection)jsonData["data"];
                    for (int i = 0; i < data.Count; i++)
                    {
                        JsonObjectCollection itemData = (JsonObjectCollection)data[i];

                        if (((JsonNumericValue)itemData["is_admin"]).Value == 1)
                        {
                            //服务台
                            ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonStringValue)itemData["ip"]).Value, "服务台", "运行中" });
                            this.OnlineList.Items.Add(item);
                        }
                        else
                        {
                            //一般
                            string status = "正在空闲";

                            if (itemData["o_status"].GetValue() != null)
                            {
                                if (((JsonNumericValue)itemData["o_status"]).Value == 0)
                                    status = "已消费" + ((JsonNumericValue)itemData["total_price"]).Value.ToString("f1") + "元";
                                else if (((JsonNumericValue)itemData["o_status"]).Value == 1)
                                    status = "结账中，金额为" + ((JsonNumericValue)itemData["total_price"]).Value.ToString("f1") + "元";                            
                            }
                            
                            ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonStringValue)itemData["ip"]).Value, "一般", status });
                            this.OnlineList.Items.Add(item);
                        }                        
                        
                    }
                    
                }
            }
            else
                MessageBox.Show(((JsonStringValue)jsonData["message"]).Value);

        }

        public void WebSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

    }
}
