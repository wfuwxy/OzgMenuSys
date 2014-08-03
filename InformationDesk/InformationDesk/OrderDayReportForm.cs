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
    public partial class OrderDayReportForm : BaseForm
    {
        public OrderDayReportForm(WebSocket Connection)
        {
            this.Connection = Connection;

            InitializeComponent();
        }

        private void OrderDayReportForm_Load(object sender, EventArgs e)
        {
            this.Text = Strings.OrderDayReportFormTitle;

            //设置行高
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 25);
            this.ReportList.SmallImageList = imageList;

            this.ReportList.Columns.Add("", 0);
            this.ReportList.Columns.Add(Strings.OrderDayReportFormListTitle1, 160).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ReportList.Columns.Add(Strings.OrderDayReportFormListTitle2, 130).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ReportList.Columns.Add(Strings.OrderDayReportFormListTitle3, 130).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ReportList.Columns.Add(Strings.OrderDayReportFormListTitle4, 130).TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            TimeToDateTimePicker.Text = DateTime.Now.ToString();

        }

        public void ShowData(string jsonStr)
        {
            this.ReportList.Items.Clear();

            JsonTextParser parser = new JsonTextParser();
            JsonObjectCollection jsonData = (JsonObjectCollection)parser.Parse(jsonStr);

            JsonArrayCollection data = (JsonArrayCollection)jsonData["data"];
            for (int i = 0; i < data.Count; i++)
            {
                JsonObjectCollection itemData = (JsonObjectCollection)data[i];
                if (itemData["total_price"].GetValue() == null)
                    continue;

                ListViewItem item = new ListViewItem(new string[] { "", Commons.UnixTimeFrom((long)((JsonNumericValue)itemData["time"]).Value).ToString("yyyy-MM-dd"), ((int)((JsonNumericValue)itemData["total"]).Value).ToString(), ((int)((JsonNumericValue)itemData["total_quantity"]).Value).ToString(), ((float)((JsonNumericValue)itemData["total_price"]).Value).ToString("f1") });
                this.ReportList.Items.Add(item);
            }

        }

        private void BtnGetData_Click(object sender, EventArgs e)
        {
            long timeFrom = Commons.UnixTimeTo(DateTime.Parse(TimeFromDateTimePicker.Text));
            long timeTo = Commons.UnixTimeTo(DateTime.Parse(TimeToDateTimePicker.Text));

            JsonObjectCollection data = new JsonObjectCollection();
            data.Add(new JsonNumericValue("time_from", timeFrom));
            data.Add(new JsonNumericValue("time_to", timeTo));

            JsonObjectCollection jsonData = new JsonObjectCollection();
            jsonData.Add(new JsonStringValue("cmd", AppConfig.SERV_REPORT_DAY));
            jsonData.Add(new JsonObjectCollection("data", data));
            ConnHelper.SendString(jsonData.ToString());

        }

    }
}
