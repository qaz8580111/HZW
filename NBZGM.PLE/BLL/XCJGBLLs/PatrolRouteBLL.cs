using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums.XCJGEnums;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    /// <summary>
    /// 巡查监管巡查路线逻辑层
    /// </summary>
    public class PatrolRouteBLL
    {
        /// <summary>
        /// 根据路线标识获取路线对象
        /// </summary>
        /// <param name="routeID">路线标识</param>
        /// <returns>路线对象</returns>
        public static XCJGROUTE GetXCJGRouteByRouteID(decimal routeID)
        {
            PLEEntities db = new PLEEntities();

            XCJGROUTE route = db.XCJGROUTES
                .SingleOrDefault(t => t.ROUTEID == routeID);

            return route;
        }

        /// <summary>
        /// 根据登陆用户标识及所有者类型标识获取该用户所属中队下的巡查路线列表
        /// </summary>
        /// <param name="userID">登陆用户标识</param>
        /// <param name="typeID">所有者类型标识</param>
        /// <returns>该用户所属中队下的巡查路线列表</returns>
        public static IQueryable<XCJGROUTE> GetPatrolRoutes
            (decimal userID, decimal typeID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGROUTE> routes =
                from r in db.XCJGROUTES
                from u in db.USERS
                where r.SSZDID == u.UNITID
                   && r.ROUTEOWNERTYPE == typeID
                   && u.USERID == userID
                select r;

            return routes;
        }

        /// <summary>
        /// 根据路线对象添加路线
        /// </summary>
        /// <param name="route">路线对象</param>
        public static void AddRoute(XCJGROUTE route)
        {
            PLEEntities db = new PLEEntities();
            db.XCJGROUTES.Add(route);
            db.SaveChanges();
        }

        /// <summary>
        /// 根据路线对象修改路线
        /// </summary>
        /// <param name="route">路线对象</param>
        public static void ModifyRoute(XCJGROUTE route)
        {
            PLEEntities db = new PLEEntities();

            XCJGROUTE XCJGRoute = db.XCJGROUTES
                .SingleOrDefault(t => t.ROUTEID == route.ROUTEID);

            XCJGRoute.ROUTENAME = route.ROUTENAME;
            XCJGRoute.ROUTEDESCRIPTION = route.ROUTEDESCRIPTION;
            XCJGRoute.GEOMETRY = route.GEOMETRY;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据路线标识删除路线
        /// </summary>
        /// <param name="routeID">路线标识</param>
        public static void DeleteRoute(decimal routeID)
        {
            PLEEntities db = new PLEEntities();

            XCJGROUTE route = db.XCJGROUTES
                .SingleOrDefault(t => t.ROUTEID == routeID);
            db.XCJGROUTES.Remove(route);
            db.SaveChanges();
        }
    }
}
