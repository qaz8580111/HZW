using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.ZonesBLL
{
   public class SYS_ZONESBLL
    {
        /// <summary>
        /// 获取所有的片区
        /// </summary>
        /// <returns></returns>
       public static IQueryable<SYS_ZONES> GetzfsjZone()
        {
            Entities db = new Entities();
            IQueryable<SYS_ZONES> results = db.SYS_ZONES.OrderBy(t => t.ZONEID);
            return results;
        }
       /// <summary>
       ///根据ID获取名称
       /// </summary>
       /// <param name="ID">主键ID</param>
       /// <returns></returns>
       public static string GetzfsjZoneName(decimal ID)
       {
           Entities db = new Entities();
           SYS_ZONES model = db.SYS_ZONES.FirstOrDefault(t => t.ZONEID == ID);
           string CLASSNAME = "";
           if (model!=null)
           {
               CLASSNAME = model.ZONENAME;
           }
           return CLASSNAME;
       }
    }
}
