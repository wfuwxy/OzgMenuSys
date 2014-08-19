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
    public partial class MainForm : BaseForm
    {
        private List<int> InformationDeskIndices; //服务台的索引
        private Dictionary<int, string> ClientNameList; //一般客户端的名称列表
        private Dictionary<int, int> ClientOrderStatusList; //一般客户端的订单状态列表
        private Dictionary<int, int> ClientStatusList; //一般客户端的状态列表
        private int ClientListSelectedIndex = -1; //保存选定项的索引

        private List<int> OrderDetailIDList; //下单明细列表的id
        private List<int> OrderDetailStatusList; //下单明细列表的状态值
        private List<string> OrderDetailNameList; //下单明细列表的名称
        private int OrderDetailListSelectedIndex = -1; //保存选定项的索引

        private Form SubForm = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.MainFormTitle;

            this.Connection = ConnHelper.GetConnInstance(this);

            //设置行高
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 25);
            this.OnlineList.SmallImageList = imageList;

            this.OnlineList.Columns.Add("", 0);
            this.OnlineList.Columns.Add(Strings.MainFormOnlineListTitle1, 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OnlineList.Columns.Add(Strings.MainFormOnlineListTitle2, 160).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OnlineList.Columns.Add(Strings.MainFormOnlineListTitle3, 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OnlineList.Columns.Add(Strings.MainFormOnlineListTitle4, 160).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            this.OrderDetailList.SmallImageList = imageList;

            this.OrderDetailList.Columns.Add("", 0);
            this.OrderDetailList.Columns.Add(Strings.MainFormOrderDetailListTitle1, 100).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OrderDetailList.Columns.Add(Strings.MainFormOrderDetailListTitle2, 120).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OrderDetailList.Columns.Add(Strings.MainFormOrderDetailListTitle3, 70).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OrderDetailList.Columns.Add(Strings.MainFormOrderDetailListTitle4, 70).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OrderDetailList.Columns.Add(Strings.MainFormOrderDetailListTitle5, 100).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OrderDetailList.Columns.Add(Strings.MainFormOrderDetailListTitle6, 60).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SubForm = new AboutForm();
            this.SubForm.ShowDialog();
            this.SubForm = null;
        }

        private void MenuClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SubForm = new MenuClassForm(this.Connection);
            this.SubForm.ShowDialog();
            this.SubForm = null;
        }

        private void MenuDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SubForm = new MenuForm(this.Connection);
            this.SubForm.ShowDialog();
            this.SubForm = null;
        }

        private void ClientListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SubForm = new ClientForm(this.Connection);
            this.SubForm.ShowDialog();
            this.SubForm = null;
        }

        private void OrderDayReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SubForm = new OrderDayReportForm(this.Connection);
            this.SubForm.ShowDialog();
            this.SubForm = null;
        }

        private void OrderMonthReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SubForm = new OrderMonthReportForm(this.Connection);
            this.SubForm.ShowDialog();
            this.SubForm = null;
        }

        public void WebSocket_Opened(object sender, EventArgs e)
        {
            this.Text = Strings.MainFormConnectedTitle;

            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_CHK_CLIENT));
            ConnHelper.SendString(jsonData.ToString());
        }

        public void WebSocket_Closed(object sender, EventArgs e)
        {
            //发生错误或链接不上都会触发这里，直接关闭程序不会触发这里
            this.Text = Strings.MainFormNotConnectTitle;
            this.OnlineList.Items.Clear();

            ConnHelper.Reconnect();
        }
        
        public void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            JsonTextParser parser = new JsonTextParser();
            JsonObjectCollection jsonData = (JsonObjectCollection)parser.Parse(e.Message); 
            if (((JsonNumericValue)jsonData["ok"]).Value == 1)
            {
                //子窗口相关
                if (this.SubForm != null)
                {
                    if (this.SubForm is OrderDayReportForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_REPORT_DAY))                    
                        ((OrderDayReportForm)this.SubForm).ShowData(e.Message); //订单日报表
                    else if (this.SubForm is OrderMonthReportForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_REPORT_MONTH))
                        ((OrderMonthReportForm)this.SubForm).ShowData(e.Message); //订单月报表
                    else if (this.SubForm is ClientForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_CLIENT_LIST))
                        ((ClientForm)this.SubForm).ShowData(e.Message); //客户端管理
                    else if (this.SubForm is MenuClassForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_MENU_CLASS))
                        ((MenuClassForm)this.SubForm).ShowData(e.Message); //菜单分类
                    else if (this.SubForm is MenuForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_MENU_CLASS))
                        ((MenuForm)this.SubForm).ShowData(e.Message); //菜单列表的分类部分
                    else if (this.SubForm is MenuForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_MENU))
                        ((MenuForm)this.SubForm).ShowData(e.Message); //菜单列表的数据部分
                    else if (this.SubForm is MenuForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_BIG_IMAGE))
                    {
                        if (((MenuForm)this.SubForm).SubForm != null && ((MenuAddForm)((MenuForm)this.SubForm).SubForm).SubForm != null)
                            ((MenuBigImgViewForm)((MenuAddForm)((MenuForm)this.SubForm).SubForm).SubForm).ShowData(e.Message); //菜单显示图片
                    }
                    else if (this.SubForm is MenuForm && ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_SELECTED_MENU_CLASS))                    
                        ((MenuForm)this.SubForm).MenuClassListSelectedWithID((int)((JsonNumericValue)jsonData["data"]).Value); //添加修改删除菜单数据后更新菜单数据列表
                    
                }

                if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_TOMAIN) || ((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_GET_ONLINE_LIST))
                {
                    { 
                        //发送获取在线列表的请求
                        jsonData = new JsonObjectCollection();
                        jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_ONLINE_LIST));
                        ConnHelper.SendString(jsonData.ToString());
                    }
                    
                    {
                        //获取下单明细列表
                        jsonData = new JsonObjectCollection();
                        jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_ORDER_DETAIL));
                        ConnHelper.SendString(jsonData.ToString());
                    }
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
                            ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonStringValue)itemData["ip"]).Value, Strings.MainFormInformationDeskName, Strings.MainFormInformationDeskStatus });
                            this.OnlineList.Items.Add(item);

                            this.InformationDeskIndices.Add(i);
                        }
                        else
                        {
                            //一般
                            string status = Strings.MainFormClientStatus;

                            if (itemData["o_status"].GetValue() != null)
                            {
                                float totalPrice = 0.0f;
                                if (itemData["total_price"].GetValue() != null)
                                    totalPrice = (float)((JsonNumericValue)itemData["total_price"]).Value;

                                if (((JsonNumericValue)itemData["o_status"]).Value == 0)
                                    status = string.Format(Strings.MainFormClientStatus0, totalPrice.ToString("f1"));
                                else if (((JsonNumericValue)itemData["o_status"]).Value == 1)
                                    status = string.Format(Strings.MainFormClientStatus1, totalPrice.ToString("f1"));

                                this.ClientOrderStatusList.Add(i, (int)((JsonNumericValue)itemData["o_status"]).Value);                                
                            }

                            this.ClientNameList.Add(i, ((JsonStringValue)itemData["name"]).Value);
                            this.ClientStatusList.Add(i, (int)((JsonNumericValue)itemData["status"]).Value);

                            ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["name"]).Value, ((JsonStringValue)itemData["ip"]).Value, Strings.MainFormClientClassName, status });
                            this.OnlineList.Items.Add(item);
                            
                        }                        
                        
                    }

                    if (this.ClientListSelectedIndex > -1)
                        this.OnlineList.Items[this.ClientListSelectedIndex].Selected = true;

                }
                else if (((JsonStringValue)jsonData["cmd"]).Value.Equals(AppConfig.CLIENT_WANT_ORDER_DETAIL))
                {
                    //下单明细列表

                    this.OrderDetailIDList = new List<int>();
                    this.OrderDetailStatusList = new List<int>();
                    this.OrderDetailNameList = new List<string>();

                    this.OrderDetailList.Items.Clear();
                    JsonArrayCollection data = (JsonArrayCollection)jsonData["data"];

                    for (int i = 0; i < data.Count; i++)
                    {
                        JsonObjectCollection itemData = (JsonObjectCollection)data[i];

                        string status = Strings.MainFormOrderDetailStatus0;
                        if((int)((JsonNumericValue)itemData["status"]).Value == 1)
                            status = Strings.MainFormOrderDetailStatus1;

                        this.OrderDetailIDList.Add((int)((JsonNumericValue)itemData["id"]).Value);
                        this.OrderDetailStatusList.Add((int)((JsonNumericValue)itemData["status"]).Value);
                        this.OrderDetailNameList.Add(((JsonStringValue)itemData["menu_name"]).Value);

                        ListViewItem item = new ListViewItem(new string[] { "", ((JsonStringValue)itemData["c_name"]).Value, ((JsonStringValue)itemData["menu_name"]).Value, ((float)((JsonNumericValue)itemData["price"]).Value).ToString("f1"), ((int)((JsonNumericValue)itemData["quantity"]).Value).ToString(), Commons.UnixTimeFrom((long)((JsonNumericValue)itemData["add_time"]).Value).ToString("HH:mm:ss"), status });
                        this.OrderDetailList.Items.Add(item);
                    }

                    if (this.OrderDetailListSelectedIndex > -1)
                        this.OrderDetailList.Items[this.OrderDetailListSelectedIndex].Selected = true;

                }

            }
            else
                MessageBox.Show(((JsonStringValue)jsonData["message"]).Value);

        }

        public void WebSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            //MessageBox.Show(e.Exception.Message);
                        
        }

        private void OnlineList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.OnlineList.SelectedItems.Count == 1)
                {
                    //确保只选中一个

                    if (!this.InformationDeskIndices.Contains(this.OnlineList.SelectedItems[0].Index))
                    {
                        this.ClientListSelectedIndex = this.OnlineList.SelectedItems[0].Index;

                        //客户端状态：0为空闲中，1为开通中
                        if (this.ClientStatusList[this.ClientListSelectedIndex] == 0)
                        {
                            //弹出开通确认框
                            DialogResult res = MessageBox.Show(string.Format(Strings.MainFormDialogOpenClientMsg1, this.ClientNameList[this.ClientListSelectedIndex]), Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                            if (res == DialogResult.OK)
                            {
                                JsonObjectCollection jsonData = new JsonObjectCollection();
                                jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_OPEN_CLIENT));
                                jsonData.Add(new JsonStringValue("data", this.OnlineList.SelectedItems[0].SubItems[2].Text));
                                ConnHelper.SendString(jsonData.ToString());
                            }
                        }
                        else
                        {
                            //订单状态：0为消费中，1为结账中，2为已归档
                            if (this.ClientOrderStatusList[this.ClientListSelectedIndex] == 1)
                            {
                                //已结账中的时候，弹出归档确认框
                                DialogResult res = MessageBox.Show(string.Format(Strings.MainFormDialogOpenClientMsg2, this.ClientNameList[this.ClientListSelectedIndex]), Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                                if (res == DialogResult.OK)
                                {
                                    JsonObjectCollection jsonData = new JsonObjectCollection();
                                    jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_CLOSE_CLIENT));
                                    jsonData.Add(new JsonStringValue("data", this.OnlineList.SelectedItems[0].SubItems[2].Text));
                                    ConnHelper.SendString(jsonData.ToString());
                                }
                            }
                        }

                    }
                }
            }
                        
        }

        private void OrderDetailList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.OrderDetailList.SelectedItems.Count == 1)
                {
                    this.OrderDetailListSelectedIndex = this.OrderDetailList.SelectedItems[0].Index;
                    if (this.OrderDetailStatusList[this.OrderDetailListSelectedIndex] == 0)
                    {
                        //弹出开通确认框
                        DialogResult res = MessageBox.Show(string.Format(Strings.MainFormOrderDetailDialogMsg, this.OrderDetailNameList[this.OrderDetailListSelectedIndex]), Strings.CommonsDialogTitle, MessageBoxButtons.OKCancel);
                        if (res == DialogResult.OK)
                        {
                            JsonObjectCollection jsonData = new JsonObjectCollection();
                            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_ORDER_DETAIL_CHANGE_STATUS));
                            jsonData.Add(new JsonNumericValue("data", this.OrderDetailIDList[this.OrderDetailListSelectedIndex]));
                            ConnHelper.SendString(jsonData.ToString());
                        }
                    }

                }
            }

        }

    }
}
