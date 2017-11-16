using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.AlarmBLLs
{
   public class CarAlarmListBLL
    {

        /// <summary>
        /// 获取车辆任务列表
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static IQueryable<QWGL_CARTASKS> GetAllCarTaskAreas()
        {
            Entities db = new Entities();
            IQueryable<QWGL_CARTASKS> us = db.QWGL_CARTASKS;
            return us;
        }

        /// <summary>
        /// 获取最后一次定位信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IQueryable<QWGL_CARLATESTPOSITIONS> GetAllUserLatestPositions(decimal CARID)
        {
            Entities db = new Entities();
            IQueryable<QWGL_CARLATESTPOSITIONS> us = db.QWGL_CARLATESTPOSITIONS.Where(t => t.CARID == CARID);
            return us;
        }


        /// <summary>
        /// 根据车辆ID和定位时间读取巡查范围(车辆ID和任务开始结束时间来确定唯一任务)
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<string> SelectInspectionScopeByUserID(int CARID, DateTime GPSTIME)
        {
            Entities db = new Entities();
            QWGL_CARTASKS model = db.QWGL_CARTASKS.Where(a => a.CARID == CARID && a.SDATE.Value.Year == GPSTIME.Year && a.SDATE.Value.Month == GPSTIME.Month && a.SDATE.Value.Day == GPSTIME.Day).FirstOrDefault();
            if (model != null)
            {
                List<string> list = new List<string>();

                decimal qut = model.CARTASKID;

                IQueryable<QWGL_AREAS> result = from uts in db.QWGL_CARTASKRAREARS
                                                from area in db.QWGL_AREAS
                                                where uts.AREAID == area.AREAID
                                                && uts.CARTASKID == qut
                                                select area;

                foreach (var item in result)
                {
                    list.Add(item.GEOMETRY);
                }

                return list;

                //decimal areaid = (decimal)db.QWGL_USERTASKAREARS.FirstOrDefault(t => t.USERTASKID == qut).AREAID;
                //string re = db.QWGL_AREAS.FirstOrDefault(t => t.AREAID == areaid).GEOMETRY;
                //return re;
            }
            else
            {
                return null;
            }


        }
    }
}
