using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.USERHISTORYPOSITIONSBLL
{
   public class QWGL_USERHISTORYPOSITIONSBLL
    {
        /// <summary>
        /// 添加定位信息
        /// </summary>
        /// <returns></returns>
       public static void AddUserHistoryPosition(QWGL_USERHISTORYPOSITIONS model)
        {
            Entities db = new Entities();
            db.QWGL_USERHISTORYPOSITIONS.Add(model);
            db.SaveChanges();
        }
       /// <summary>
       /// 获取过去时间段内的定位数据
       /// </summary>
       /// <param name="duration">时间区段（分钟）</param>
       /// <returns></returns>
       public static List<QWGL_USERHISTORYPOSITIONS> GetHistoryPositionsByDuration(int duration)
       {
           var StartTime = DateTime.Now.AddMinutes(0 - duration);
           Entities db = new Entities();
           var position = db.QWGL_USERHISTORYPOSITIONS.Where(t => t.POSITIONTIME >= StartTime).OrderBy(t => t.POSITIONTIME).ToList();
           return position;
       }
    }
}
