using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;

namespace ZGM.BLL.UnitTypeBLLs
{
    public class UnitTypeBLL
    {
        /// <summary>
        /// 获取所有的行政单位
        /// </summary>
        /// <returns>行政单位列表</returns>
        public static IQueryable<SYS_UNITTYPES> GetAllUnitType()
        {
            Entities db = new Entities();

            IQueryable<SYS_UNITTYPES> result = db.SYS_UNITTYPES.OrderBy(t => t.UNITTYPEID);

            return result;
        }
    }
}
