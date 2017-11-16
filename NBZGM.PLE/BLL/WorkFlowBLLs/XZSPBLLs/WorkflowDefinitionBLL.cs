using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class WorkflowDefinitionBLL
    {
        /// <summary>
        /// 获取所有的行政审批工作流定义
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPWFDEF> Query()
        {
            PLEEntities db = new PLEEntities();
            return db.XZSPWFDEFS;
        }
    }
}
