using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    public class ZFSJProcessWayBLL
    {
        /// <summary>
        /// 获取处理方式列表
        /// </summary>
        /// <returns></returns>
        public static List<ZFSJPROCESSWAY> GetProcessWayList() 
        {
            PLEEntities db = new PLEEntities();
            List<ZFSJPROCESSWAY> results = db.ZFSJPROCESSWAYs.ToList();
            return results;
        }

        public static ZFSJPROCESSWAY GetProcessWayByID(decimal ID)
        {
             string questionID = ID.ToString();
             ZFSJPROCESSWAY result = null;
             if (questionID != "0")
             {
                 PLEEntities db = new PLEEntities();
                 result = db.ZFSJPROCESSWAYs.Single<ZFSJPROCESSWAY>(t => t.ID == ID);
                 return result;
             }
             else
             {
                 return result;
             }
           
        }
    }
}
