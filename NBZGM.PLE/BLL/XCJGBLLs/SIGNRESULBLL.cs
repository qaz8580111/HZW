using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    public class SIGNRESULBLL
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        public void AddSIGNRESUL(SIGNRESUL model)
        {
            PLEEntities db = new PLEEntities();
            db.SIGNRESULS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<SIGNRESUL> GetSIGNRESUL()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<SIGNRESUL> result = db.SIGNRESULS;
            return result;
        }
    }
}
