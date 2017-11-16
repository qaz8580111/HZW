using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XZZFBLLs
{
    public class XZZFCLASSBLL
    {
        /// <summary>
        /// 获取所有的小类
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZZFQUESTIONCLASS> GetXZZFQuestion()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZZFQUESTIONCLASS> results = db.XZZFQUESTIONCLASSES
                .Where(t => t.TYPEID == 2);
            return results;
        }

        /// <summary>
        /// 获取所有的大类
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZZFQUESTIONCLASS> GetXZZFQuestionDL()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZZFQUESTIONCLASS> results = db.XZZFQUESTIONCLASSES
                .Where(t => t.TYPEID == 1);
            return results;
        }
    }
}
