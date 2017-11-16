using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ZFSJTIMELIMIT
{
   public  class AppComin
    {
       public static string WorkStart = ConfigurationSettings.AppSettings["WorkStart"].ToString();
       public static string WorkEnd = ConfigurationSettings.AppSettings["WorkEnd"].ToString();
       public static string XXWeek = ConfigurationSettings.AppSettings["XXWeek"].ToString();
       public static string XXDay = ConfigurationSettings.AppSettings["XXDay"].ToString();
    }
}
