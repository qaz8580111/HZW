using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.QWGLBLLs
{
    public class PatrolAreaBLL
    {
        /// <summary>
        /// 根据登陆用户标识及所有者类型标识获取该用户所属中队下的巡查区域列表
        /// </summary>
        /// <param name="userID">登陆用户标识</param>
        /// <param name="typeID">所有者类型标识</param>
        /// <returns>该用户所属中队下的巡查区域列表</returns>
        public static IQueryable<QWGL_AREAS> GetPatrolAreas(decimal typeID)
        {
            Entities db = new Entities();
            IQueryable<QWGL_AREAS> areas = db.QWGL_AREAS.Where(t => t.AREAOWNERTYPE == typeID && t.STATE == 1);
              
            return areas;
        }
        /// <summary>
        /// 根据登陆用户标识及所有者类型标识获取该用户所属中队下的巡查区域列表
        /// </summary>
        /// <param name="userID">登陆用户标识</param>
        /// <param name="typeID">所有者类型标识</param>
        /// <returns>该用户所属中队下的巡查区域列表</returns>
        public static IQueryable<QWGL_AREAS> GetPatrolAreas(decimal typeID, decimal unitPId)
        {
            Entities db = new Entities();
            string path = "\\" + 1 + "\\" + unitPId + "\\";

            IQueryable<QWGL_AREAS> areas =
                from r in db.QWGL_AREAS
                from c in db.SYS_USERS
                from u in db.SYS_UNITS
                where r.AREAOWNERTYPE == typeID
                && r.CREATEUSERID == c.USERID
                && c.UNITID == u.UNITID
                && u.PATH.Contains(path)
                select r;

            return areas;
        }
        /// <summary>
        /// 根据区域标识获取区域对象
        /// </summary>
        /// <param name="areaID">区域标识</param>
        /// <returns>区域对象</returns>
        public static QWGL_AREAS GetQWGLAreaByAreaID(decimal areaID)
        {
            Entities db = new Entities();

            QWGL_AREAS area = db.QWGL_AREAS
                .SingleOrDefault(t => t.AREAID == areaID);

            return area;
        }
      
    }
}
