using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.XTBGBLL
{
   public class OA_ATTRACHSBLL
    {
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="model"></param>
       public static void AddATTRACHS(OA_ATTRACHS model)
        {
            Entities db = new Entities();
            model.ATTRACHID = GetNewId();
            db.OA_ATTRACHS.Add(model);
            db.SaveChanges();
        }

       /// <summary>
       /// 获取的编号
       /// </summary>
       private static string GetNewId()
       {
           return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(10000, 99999);
       }
    }
}
