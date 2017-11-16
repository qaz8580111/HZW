using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    public class ZFSJSourceBLL
    {
        /// <summary>
        /// 获取所有的事件来源
        /// </summary>
        /// <returns></returns>
        public static IQueryable<ZFSJSOURCE> GetZFSJSourceList() 
        {
            PLEEntities db = new PLEEntities();
            IQueryable<ZFSJSOURCE> results = db.ZFSJSOURCES;
            return results;
        }

        public static ZFSJSOURCE GetSourceByID(decimal ID)
        {
            PLEEntities db = new PLEEntities();
            ZFSJSOURCE result = db.ZFSJSOURCES.Single<ZFSJSOURCE>(t=>t.ID==ID);
            return result;
        }
    }
}
