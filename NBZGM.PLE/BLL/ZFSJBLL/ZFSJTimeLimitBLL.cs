using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
   public class ZFSJTimeLimitBLL
    {
       //获取超期时间
       public static string  GetZfsjTimeLimit(decimal ADID) 
       {
           PLEEntities db = new PLEEntities();
           return db.ZFSJTIMELIMITS.FirstOrDefault(t => t.ADID == ADID).TIMELIMIT.ToString();
       }


    }
}
