using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace InformationDesk
{
    public class ConnHelper
    {
        public static WebSocket Connection = null;

        public static WebSocket GetConnInstance(Form form)
        {
            if (Connection == null)
            {
                Connection = new WebSocket(AppConfig.SERV, AppConfig.PROTOCOL);
                Connection.ReceiveBufferSize = 99999;                
            }
            if (form is MainForm)
            {
                Connection.Opened += ((MainForm)form).WebSocket_Opened;
                Connection.MessageReceived += ((MainForm)form).WebSocket_MessageReceived;
                Connection.Closed += ((MainForm)form).WebSocket_Closed;
                Connection.Error += ((MainForm)form).WebSocket_Error;
            }

            if (Connection.State != WebSocketState.Connecting)
                Connection.Open();

            return Connection;
        }

        public static void SendString(string str)
        {
            try
            {
                Connection.Send(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Reconnect()
        {
            System.Timers.Timer reconnectTimer = new System.Timers.Timer();
            reconnectTimer.Enabled = true;
            reconnectTimer.AutoReset = false;
            reconnectTimer.Interval = AppConfig.RECONNECT_TIME;
            reconnectTimer.Elapsed += ReconnectTimer_Elapsed;
            reconnectTimer.Start();
        }

        private static void ReconnectTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //重新链接
            if (Connection.State != WebSocketState.Connecting)
                Connection.Open();

        }

    }
}
