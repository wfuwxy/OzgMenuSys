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
        public static int RECONNECT_TIME = 3000;

        public static string CLIENT_WANT_TOMAIN = "CLIENT_WANT_TOMAIN";
        public static string CLIENT_GET_ONLINE_LIST = "CLIENT_GET_ONLINE_LIST";
        public static string CLIENT_WANT_ONLINE_LIST = "CLIENT_WANT_ONLINE_LIST";
        public static string CLIENT_WANT_REPORT_DAY = "CLIENT_WANT_REPORT_DAY";
        public static string CLIENT_WANT_CLIENT_LIST = "CLIENT_WANT_CLIENT_LIST";
        public static string CLIENT_WANT_MENU_CLASS = "CLIENT_WANT_MENU_CLASS";
        public static string CLIENT_WANT_MENU = "CLIENT_WANT_MENU";
        public static string CLIENT_WANT_BIG_IMAGE = "CLIENT_WANT_BIG_IMAGE";
        public static string CLIENT_WANT_SELECTED_MENU_CLASS = "CLIENT_WANT_SELECTED_MENU_CLASS";

        public static string SERV_CHK_CLIENT = "SERV_CHK_CLIENT";
        public static string SERV_ONLINE_LIST = "SERV_ONLINE_LIST";
        public static string SERV_REPORT_DAY = "SERV_REPORT_DAY";
        public static string SERV_CLIENT_LIST = "SERV_CLIENT_LIST";
        public static string SERV_CLIENT_ADD = "SERV_CLIENT_ADD";
        public static string SERV_CLIENT_DELETE = "SERV_CLIENT_DELETE";  
        public static string SERV_OPEN_CLIENT = "SERV_OPEN_CLIENT";
        public static string SERV_CLOSE_CLIENT = "SERV_CLOSE_CLIENT";
        public static string SERV_MENU_CLASS_LIST = "SERV_MENU_CLASS_LIST";
        public static string SERV_MENU_CLASS_ADD = "SERV_MENU_CLASS_ADD";
        public static string SERV_MENU_CLASS_DELETE = "SERV_MENU_CLASS_DELETE";
        public static string SERV_MENU_LIST = "SERV_MENU_LIST";
        public static string SERV_MENU_ADD = "SERV_MENU_ADD";
        public static string SERV_MENU_DELETE = "SERV_MENU_DELETE";
        public static string SERV_BIG_IMAGE = "SERV_BIG_IMAGE";   
                
    }
}
