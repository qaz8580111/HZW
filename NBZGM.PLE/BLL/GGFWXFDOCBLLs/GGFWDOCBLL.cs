using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.GGFWXFDOCBLLs
{
    public class GGFWDOCBLL
    {
        /// <summary>
        /// 创建一个信访文书
        /// </summary>
        /// <param name="ggfwxfdoc"></param>
        /// <returns></returns>
        public static int CreateGGFWDOC(GGFWXFDOC ggfwxfdoc)
        {
            PLEEntities db = new PLEEntities();
            db.GGFWXFDOCS.Add(ggfwxfdoc);
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据信访编号查询文书
        /// </summary>
        /// <param name="ggfwxfdoc"></param>
        /// <returns></returns>
        public static IQueryable<GGFWXFDOC> GetGGFWXFDOCByEventID(decimal EventID)
        {
            PLEEntities db = new PLEEntities();
            return db.GGFWXFDOCS.Where(t => t.EVETID == EventID);
        }
    }
}
