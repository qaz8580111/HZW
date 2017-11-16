using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using System.Web;

namespace Taizhou.PLE.BLL.ZFRYBLL
{
    public class ZFRYBLL
    {
        /// <summary>
        /// 获得执法人员当月每天的条数
        /// </summary>
        /// <returns></returns>
        public static int GetZFGKUSERLATESTPOSITIONSByMum()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            PLEEntities db = new PLEEntities();
            int a = db.ZFGKUSERLATESTPOSITIONS.Where(t => t.POSITIONTIME >= dt && t.LON != -1 && t.LAT != -1).Count();
            return a;
        }
    }
}
