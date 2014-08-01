using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationDesk
{
    public class AppConfig
    {
        public static string SERV = "ws://192.168.1.10:8765";
        public static string PROTOCOL = "echo-protocol";

        public static string CLIENT_WANT_TOMAIN = "CLIENT_WANT_TOMAIN";
        public static string CLIENT_GET_ONLINE_LIST = "CLIENT_GET_ONLINE_LIST";
        public static string CLIENT_WANT_ONLINE_LIST = "CLIENT_WANT_ONLINE_LIST";

        public static string SERV_CHK_CLIENT = "SERV_CHK_CLIENT";
        public static string SERV_ONLINE_LIST = "SERV_ONLINE_LIST";
        public static string SERV_OPEN_CLIENT = "SERV_OPEN_CLIENT";
        public static string SERV_CLOSE_CLIENT = "SERV_CLOSE_CLIENT";
        
    }
}
