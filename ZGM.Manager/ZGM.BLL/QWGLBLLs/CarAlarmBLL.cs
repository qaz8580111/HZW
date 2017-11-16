using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.QWGLBLLs
{
  public class CarAlarmBLL
    {
        /// <summary>
        /// 获取所有的报警记录
        /// </summary>
        /// <returns></returns>
      public static IQueryable<QWGL_CARALARMMEMORYDATA> GetAllLiat()
        {
            Entities db = new Entities();
            IQueryable<QWGL_CARALARMMEMORYDATA> list = db.QWGL_CARALARMMEMORYDATA.OrderByDescending(t => t.GPSTIME);
            return list;
        }

    }
}
