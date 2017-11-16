using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums.XCJGEnums;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    /// <summary>
    /// 巡查监管巡查区域逻辑层
    /// </summary>
    public class PatrolAreaBLL
    {
        /// <summary>
        /// 根据区域标识获取区域对象
        /// </summary>
        /// <param name="areaID">区域标识</param>
        /// <returns>区域对象</returns>
        public static XCJGAREA GetXCJGAreaByAreaID(decimal areaID)
        {
            PLEEntities db = new PLEEntities();

            XCJGAREA area = db.XCJGAREAS
                .SingleOrDefault(t => t.AREAID == areaID);

            return area;
        }

        /// <summary>
        /// 根据登陆用户标识及所有者类型标识获取该用户所属中队下的巡查区域列表
        /// </summary>
        /// <param name="userID">登陆用户标识</param>
        /// <param name="typeID">所有者类型标识</param>
        /// <returns>该用户所属中队下的巡查区域列表</returns>
        public static IQueryable<XCJGAREA> GetPatrolAreas
            (decimal userID, decimal typeID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGAREA> areas =
                from r in db.XCJGAREAS
                from u in db.USERS
                where r.SSZDID == u.UNITID
                   && r.AREAOWNERTYPE == typeID
                   && u.USERID == userID
                select r;

            return areas;
        }

        /// <summary>
        /// 根据区域对象添加区域
        /// </summary>
        /// <param name="area">区域对象</param>
        public static void AddArea(XCJGAREA area)
        {
            PLEEntities db = new PLEEntities();

            db.XCJGAREAS.Add(area);
            db.SaveChanges();
        }

        /// <summary>
        /// 根据区域对象修改区域
        /// </summary>
        /// <param name="area">区域对象</param>
        public static void ModifyArea(XCJGAREA area)
        {
            PLEEntities db = new PLEEntities();

            XCJGAREA XCJGArea = db.XCJGAREAS
                .SingleOrDefault(t => t.AREAID == area.AREAID);

            XCJGArea.AREANAME = area.AREANAME;
            XCJGArea.AREADESCRIPTION = area.AREADESCRIPTION;
            XCJGArea.GEOMETRY = area.GEOMETRY;
            XCJGArea.XCTIME = area.XCTIME;
            XCJGArea.USERIDS = area.USERIDS;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据区域标识删除区域
        /// </summary>
        /// <param name="areaID">区域标识</param>
        public static void DeleteArea(decimal areaID)
        {
            PLEEntities db = new PLEEntities();

            XCJGAREA area = db.XCJGAREAS
                .SingleOrDefault(t => t.AREAID == areaID);
            if (area != null)
            {
                db.XCJGAREAS.Remove(area);
                db.SaveChanges();
            }

           
        }
    }
}
