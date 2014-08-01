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

        private List<int> InformationDeskIndices; //服务台的索引
        private Dictionary<int, string> ClientNameList; //一般客户端的名称列表
        private Dictionary<int, int> ClientOrderStatusList; //一般客户端的订单状态列表
        private Dictionary<int, int> ClientStatusList; //一般客户端的状态列表
        private int ListSelectedIndex = -1; //保存选定项的索引

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
                if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_TOMAIN) || ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_GET_ONLINE_LIST))
                {
                    //发送获取在线列表的请求
                    jsonData = new JsonObjectCollection();
                    jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_ONLINE_LIST));
                    this.Connection.Send(jsonData.ToString());
                }
                else if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_ONLINE_LIST))
                { 
                    //显示在线列表

                    this.InformationDeskIndices = new List<int>();
                    this.ClientOrderStatusList = new Dictionary<int, int>();
                    this.ClientStatusList = new Dictionary<int, int>();
                    this.ClientNameList = new Dictionary<int, string>();
                    this.OnlineList.Items.Clear();

                    JsonArrayCollection data = (JsonArrayCollection)jsonData["data"];
                    for (int i = 0; i < data.Count; i++)
                    {
                        JsonObjectCollection itemData = (JsonObjectCollection)data[i];

                        if (((JsonNumericValue)itemData["is_admin"]).Value == 1)
                        {
                            //服务台
                            ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonStringValue)itemData["ip"]).Value, "服务台", "运行中" });
                            this.OnlineList.Items.Add(item);

                            this.InformationDeskIndices.Add(i);
                        }
                        else
                        {
                            //一般
                            string status = "正在空闲";

                            if (itemData["o_status"].GetValue() != null)
                            {
                                float totalPrice = 0.0f;
                                if (itemData["total_price"].GetValue() != null)
                                    totalPrice = (float)((JsonNumericValue)itemData["total_price"]).Value;

                                if (((JsonNumericValue)itemData["o_status"]).Value == 0)
                                    status = "已消费" + totalPrice.ToString("f1") + "元";
                                else if (((JsonNumericValue)itemData["o_status"]).Value == 1)
                                    status = "结账中，金额为" + totalPrice.ToString("f1") + "元";

                                this.ClientOrderStatusList.Add(i, (int)((JsonNumericValue)itemData["o_status"]).Value);                                
                            }

                            this.ClientNameList.Add(i, ((JsonStringValue)itemData["name"]).Value);
                            this.ClientStatusList.Add(i, (int)((JsonNumericValue)itemData["status"]).Value);

                            ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonStringValue)itemData["ip"]).Value, "一般", status });
                            this.OnlineList.Items.Add(item);
                            
                        }                        
                        
                    }

                    if (this.ListSelectedIndex > -1)
                        this.OnlineList.Items[this.ListSelectedIndex].Selected = true;

                }
            }
            else
                MessageBox.Show(((JsonStringValue)jsonData["message"]).Value);

        }

        public void WebSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void OnlineList_Click(object sender, EventArgs e)
        {
            if (this.OnlineList.SelectedItems.Count == 1)
            { 
                //确保只选中一个
                if (!this.InformationDeskIndices.Contains(this.OnlineList.SelectedItems[0].Index))
                {
                    this.ListSelectedIndex = this.OnlineList.SelectedItems[0].Index;

                    //客户端状态：0为空闲中，1为开通中
                    if (this.ClientStatusList[this.ListSelectedIndex] == 0)
                    {
                        //弹出开通确认框
                        DialogResult res = MessageBox.Show(string.Format(Strings.MainFormDialogOpenClientMsg1, this.ClientNameList[this.ListSelectedIndex]), Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                        if (res == DialogResult.OK)
                        {
                            JsonObjectCollection jsonData = new JsonObjectCollection();
                            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_OPEN_CLIENT));
                            jsonData.Add(new JsonStringValue("data", this.OnlineList.SelectedItems[0].SubItems[2].Text));
                            this.Connection.Send(jsonData.ToString());
                        }
                    }
                    else
                    {
                        //订单状态：0为消费中，1为结账中，2为已归档
                        if (this.ClientOrderStatusList[this.ListSelectedIndex] == 1)
                        {
                            //已结账中的时候，弹出归档确认框
                            DialogResult res = MessageBox.Show(string.Format(Strings.MainFormDialogOpenClientMsg2, this.ClientNameList[this.ListSelectedIndex]), Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                            if (res == DialogResult.OK)
                            {
                                JsonObjectCollection jsonData = new JsonObjectCollection();
                                jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_CLOSE_CLIENT));
                                jsonData.Add(new JsonStringValue("data", this.OnlineList.SelectedItems[0].SubItems[2].Text));
                                this.Connection.Send(jsonData.ToString());
                            }
                        }
                    }
                                        
                }
            }
            
        }

    }
}
