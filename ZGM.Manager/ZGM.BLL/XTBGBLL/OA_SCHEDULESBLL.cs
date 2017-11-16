using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.XTBGBLL
{
   public class OA_SCHEDULESBLL
    {
        /// <summary>
        /// 添加会议
        /// </summary>
        /// <param name="model"></param>
       public static void AddSCHEDULES(OA_SCHEDULES model)
        {
            Entities db = new Entities();
            db.OA_SCHEDULES.Add(model);
            db.SaveChanges();
        }

    }
}
