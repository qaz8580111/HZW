using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class AreaDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 获取全部巡查区域
        /// 区域所有者类型 1：队员 2：车辆
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IQueryable<AreaModel> GetAllAreas(decimal? typeId)
        {
            IQueryable<AreaModel> result = from t in db.QWGL_AREAS
                                           where t.AREAOWNERTYPE == typeId
                                           && t.STATE == 1
                                           select new AreaModel
                                           {
                                               AreaId = t.AREAID,
                                               AreaName = t.AREANAME,
                                               AreaDescription = t.AREADESCRIPTION,
                                               Geometry = t.GEOMETRY,
                                               Color = t.COLOR
                                           };
            return result;
        }

        /// <summary>
        /// 根据人员标识获取巡查区域
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<AreaModel> GetUserAreas(decimal? userId)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            IQueryable<AreaModel> result = from t in db.QWGL_USERTASKS
                                           from gx in db.QWGL_USERTASKAREARS
                                           from a in db.QWGL_AREAS
                                           where t.USERID == userId
                                           && t.SDATE >= today
                                           && t.EDATE < tomorrow
                                           && gx.USERTASKID == t.USERTASKID
                                           && a.AREAID == gx.AREAID
                                           && a.STATE == 1
                                           && a.AREAOWNERTYPE == 1
                                           select new AreaModel
                                           {
                                               AreaId = a.AREAID,
                                               AreaName = a.AREANAME,
                                               AreaDescription = a.AREADESCRIPTION,
                                               Geometry = a.GEOMETRY
                                           };
            return result;
        }

        /// <summary>
        /// 根据车辆标识获取巡查区域
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public IQueryable<AreaModel> GetCarAreas(decimal? carId)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            IQueryable<AreaModel> result = from t in db.QWGL_CARTASKS
                                           from gx in db.QWGL_CARTASKRAREARS
                                           from a in db.QWGL_AREAS
                                           where t.CARID == carId
                                           && t.SDATE >= today
                                           && t.EDATE < tomorrow
                                           && gx.CARTASKID == t.CARTASKID
                                           && a.AREAID == gx.AREAID
                                           && a.STATE == 1
                                           && a.AREAOWNERTYPE == 2
                                           select new AreaModel
                                           {
                                               AreaId = a.AREAID,
                                               AreaName = a.AREANAME,
                                               AreaDescription = a.AREADESCRIPTION,
                                               Geometry = a.GEOMETRY
                                           };
            return result;
        }
    }
}
