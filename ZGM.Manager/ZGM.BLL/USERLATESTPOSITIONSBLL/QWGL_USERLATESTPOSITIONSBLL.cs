using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.USERLATESTPOSITIONSBLL
{
   public class QWGL_USERLATESTPOSITIONSBLL
    {
        /// <summary>
        /// 添加定位信息
        /// </summary>
        /// <returns></returns>
       public static void AddUserLatestPosititons(QWGL_USERLATESTPOSITIONS model)
        {
            Entities db = new Entities();
            db.QWGL_USERLATESTPOSITIONS.Add(model);
            db.SaveChanges();
        }
       /// <summary>
       /// 修改定位表
       /// </summary>
       /// <param name="_user">用户</param>
       public static void InstreUserLatestPosititons(QWGL_USERLATESTPOSITIONS model, int userId)
       {
           Entities db = new Entities();
           QWGL_USERLATESTPOSITIONS UserLatestPosititons = db.QWGL_USERLATESTPOSITIONS.SingleOrDefault(t => t.USERID == userId);

           UserLatestPosititons.X84 = model.X84;
           UserLatestPosititons.Y84 = model.Y84;
           UserLatestPosititons.X2000 = model.X2000;
           UserLatestPosititons.Y2000 = model.Y2000;
           UserLatestPosititons.LASTLOGINTIME = model.LASTLOGINTIME;
           UserLatestPosititons.POSITIONTIME = DateTime.Now;
           UserLatestPosititons.IMEICODE = model.IMEICODE;
           db.SaveChanges();
       }
       /// <summary>
       /// 根据id查询定位表
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static IQueryable<QWGL_USERLATESTPOSITIONS> GetUserLatestPosititons(int userId)
       {
           Entities db = new Entities();
           IQueryable<QWGL_USERLATESTPOSITIONS> results = db.QWGL_USERLATESTPOSITIONS
               .Where(t => t.USERID == userId);
           return results;
       }
    }
}
