using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.GCGLBLLs
{
    public class BP_GCZDBLL
    {
        /// <summary>
        /// 获取所有字典值
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BP_GCZD> GetGCZDSourceList()
        {
            Entities db = new Entities();

            string sql = string.Format(@"select ZDID, TYPEID, ZDNAME, STATUS, REMARK from BP_GCZD order by TO_NUMBER(ZDID, '9G999D99')");
            IEnumerable<BP_GCZD> results = db.Database.SqlQuery<BP_GCZD>(sql);
            // IQueryable<BP_GCZD> results = db.BP_GCZD.OrderBy(a=>decimal.Parse(a.ZDID));
            return results;
        }

        /// <summary>
        /// 获取一个新的工程ID
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewGCID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_GC_ID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

    }
}
