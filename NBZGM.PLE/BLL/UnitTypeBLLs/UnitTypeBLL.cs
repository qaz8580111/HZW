using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.UnitTypeBLLs
{
    public class UnitTypeBLL
    {
        /// <summary>
        /// 获取所有的行政单位
        /// </summary>
        /// <returns>行政单位列表</returns>
        public static IQueryable<UNITTYPE> GetAllUnitType() 
        {
            PLEEntities db = new PLEEntities();

            IQueryable<UNITTYPE> result = db.UNITTYPES.OrderBy(t=>t.UNITTYPEID);

            return result;
        } 
    }
}
