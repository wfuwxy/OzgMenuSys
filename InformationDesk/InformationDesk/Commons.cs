using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InformationDesk
{
    public class Commons
    {
        private static Regex RegIPAddress = new Regex(@"^(\d+)\.(\d+)\.(\d+)\.(\d+)");

        public static DateTime UnixTimeFrom(long timeStamp)
        {
            //return DateTime.Parse("1970-01-01 00:00:00").AddSeconds(timeStamp);

            timeStamp *= 1000;
            long timeTricks = new DateTime(1970, 1, 1).Ticks + timeStamp * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new DateTime(timeTricks);
        }

        public static long UnixTimeTo(DateTime dateTime)
        {
            return (dateTime.Ticks - DateTime.Parse("1970-01-01 00:00:00").Ticks) / 10000000;
        }

        public static bool IsIPAddress(string inputData)
        {
            Match m = RegIPAddress.Match(inputData);
            return m.Success;
        }

    }
}
