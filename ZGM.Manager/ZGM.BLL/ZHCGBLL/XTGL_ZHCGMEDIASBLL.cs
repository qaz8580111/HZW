using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.ZHCGBLL
{
   public class XTGL_ZHCGMEDIASBLL
    {
        /// <summary>
        /// 查询智慧城管同步附件表
        /// </summary>
        /// <returns></returns>
       public static IQueryable<XTGL_ZHCGMEDIAS> GetZHCGListsFile()
        {
            Entities db = new Entities();
            IQueryable<XTGL_ZHCGMEDIAS> results = db.XTGL_ZHCGMEDIAS;
            return results;
        }
       /// <summary>
       /// 查询智慧城管同步附件表
       /// </summary>
       /// <returns></returns>
       public static IQueryable<XTGL_ZHCGMEDIAS> GetZHCGFlie(string TASKNUM)
       {
           Entities db = new Entities();
           IQueryable<XTGL_ZHCGMEDIAS> results = db.XTGL_ZHCGMEDIAS.Where(a => a.TASKNUM == TASKNUM);
           return results;
       }
    }
}
