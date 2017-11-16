using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.CasePhoneBLLs
{
    public class CasePhoneBLLs
    {
        /// <summary>
        /// 添加事假发送信息
        /// </summary>
        /// <param name="casephonesms"></param>
        /// <returns></returns>
        public static int CreateCasePhone(CASEPHONESMS casephonesms)
        {
            PLEEntities db = new PLEEntities();
            db.CASEPHONESMSES.Add(casephonesms);
            return db.SaveChanges();
        }

        /// <summary>
        /// 获取所有的发送短信
        /// </summary>
        /// <returns></returns>
        public static IQueryable<CASEPHONESMS> GetList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<CASEPHONESMS> list = db.CASEPHONESMSES;
            return list;
        }
    }
}
