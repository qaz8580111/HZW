using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.ZHCGBLL
{
   public  class XTGL_ZHCGLOGSBLL
    {
       public static void AddZHCGLogs(XTGL_ZHCGLOGS model) {
           Entities db = new Entities();
           db.XTGL_ZHCGLOGS.Add(model);
           db.SaveChanges();
       }
    }
}
