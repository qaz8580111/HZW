using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.CARQWGLBLL
{
   public class QWGL_CARHISTORYPOSITIONSBLL
    {
        /// <summary>
        /// 添加定位信息
        /// </summary>
        /// <returns></returns>
       public static int AddCarLatestPosititons(QWGL_CARHISTORYPOSITIONS model)
        {
            Entities db = new Entities();
            db.QWGL_CARHISTORYPOSITIONS.Add(model);
            return db.SaveChanges();
        }
    }
}
