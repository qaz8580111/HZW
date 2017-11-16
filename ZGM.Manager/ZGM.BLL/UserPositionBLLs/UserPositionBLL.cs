using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;

namespace ZGM.BLL.UserPositionBLLs
{
    public class UserPositionBLL
    {
        /// <summary>
        /// 获取职务列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SYS_USERPOSITIONS> GetAllPositon()
        {
            Entities db = new Entities();

            IQueryable<SYS_USERPOSITIONS> results = db.SYS_USERPOSITIONS.OrderBy(t => t.USERPOSITIONID);

            return results;
        }

        /// <summary>
        /// 获取职务列表
        /// </summary>
        /// <returns></returns>
        public static decimal? GetPositonIdByPositionName(string PositionName)
        {
            Entities db = new Entities();

            decimal? positionid = db.SYS_USERPOSITIONS.FirstOrDefault(t => t.USERPOSITIONNAME == PositionName).USERPOSITIONID;

            return positionid;
        }

    }
}
