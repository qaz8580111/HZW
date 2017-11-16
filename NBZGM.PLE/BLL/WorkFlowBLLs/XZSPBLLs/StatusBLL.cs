using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class StatusBLL
    {
        /// <summary>
        /// 获取所有的行政审批流程与活动状态
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPSTU> Query()
        {
            PLEEntities db = new PLEEntities();
            return db.XZSPSTUS;
        }
    }
}
