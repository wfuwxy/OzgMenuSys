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
        public static WebSocket getConnInstance(Form form)
        {
            if (Connection == null)            
                Connection = new WebSocket(AppConfig.SERV, AppConfig.PROTOCOL);
            
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

        
        
    }
}
