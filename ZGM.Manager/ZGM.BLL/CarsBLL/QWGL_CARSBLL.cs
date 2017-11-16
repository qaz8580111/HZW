using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Common.Enums;
using ZGM.Model;

namespace ZGM.BLL.CarsBLL
{
   public class QWGL_CARSBLL
    {
        /// <summary>
        /// 获取所有用户车辆列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<QWGL_CARS> GetAllCars()
        {
            Entities db = new Entities();

            IQueryable<QWGL_CARS> results = db.QWGL_CARS
                .OrderBy(t => t.CARID);

            return results;
        }

       /// <summary>
       /// 获取终端编号。用','分割
       /// </summary>
       /// <returns></returns>
        public static string GetAllIMEI() 
        {
            Entities db = new Entities();

            IQueryable<QWGL_CARS> results = db.QWGL_CARS.OrderBy(t => t.CARID);
            string imeis = "";
            if (results!=null)
            {
                foreach (var item in results)
                {
                    imeis += item.CARTEL + ",";
                }
                imeis = imeis.Substring(0, imeis.Length - 1);
            }
            
            return imeis;
        }
    }
}
