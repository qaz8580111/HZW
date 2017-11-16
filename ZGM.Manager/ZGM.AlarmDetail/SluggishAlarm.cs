using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.AlarmDetail;
using ZGM.BLL;
using ZGM.BLL.AlarmBLLs;
using ZGM.Model;

namespace ZGM.AlarmDetail
{
    public class SluggishAlarm
    {
        /// <summary>
        /// 获取队员任务列表
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<QWGL_USERTASKS> GetAllUserTaskAreas()
        {
            Entities db = new Entities();
            List<QWGL_USERTASKS> us = db.QWGL_USERTASKS.ToList();
            return us;
        }


        /// <summary>
        /// 获取1000条巡查数据存入临时表
        /// </summary>
        /// <returns></returns>
        public static List<QWGL_USERHISTORYPOSITIONS> Get1000PatrolDataToTemporary(int userid)
        {
            Entities db = new Entities();
            var query = db.QWGL_USERHISTORYPOSITIONS.Where(a => a.ISANALYZE == 0 && a.USERID == userid).OrderBy(t => t.POSITIONTIME);
            List<QWGL_USERHISTORYPOSITIONS> List = query.Take(1000).ToList();
            // List = List.Where(b => b.USERID == userid).ToList();
            Console.WriteLine("读取1000条数据");

            return List;
        }

        //判断是否请假
        public static int IsLeave(decimal? USERID, DateTime GPSTIME)
        {
            Entities db = new Entities();
            var query = db.QWGL_LEAVES.Where(t => t.USERID == USERID && t.SDATE <= GPSTIME && t.EDATE >= GPSTIME);
            //读取请假表
            List<QWGL_LEAVES> LEAVEs = query.ToList();
            if (LEAVEs.Count > 0)
            {
                return 1;

            }
            return 0;
        }

        //射线法判断点是否在多边形区域内
        public static bool PointInFences(MapPoint pnt1, IList<MapPoint> fencePnts)
        {
            int j = 0, cnt = 0;
            for (int i = 0; i < fencePnts.Count; i++)
            {
                j = (i == fencePnts.Count - 1) ? 0 : j + 1;
                if ((fencePnts[i].Y != fencePnts[j].Y) && (((pnt1.Y >= fencePnts[i].Y) && (pnt1.Y < fencePnts[j].Y))
                    || ((pnt1.Y >= fencePnts[j].Y) && (pnt1.Y < fencePnts[i].Y)))
                    && (pnt1.X < (fencePnts[j].X - fencePnts[i].X) * (pnt1.Y - fencePnts[i].Y) / (fencePnts[j].Y - fencePnts[i].Y) + fencePnts[i].X))
                {
                    cnt++;
                }
            }
            return (cnt % 2 > 0) ? true : false;
        }

        /// <summary>
        /// 根据队员ID和定位时间读取巡查范围(用户ID和任务开始结束时间来确定唯一任务)
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<string> SelectInspectionScopeByUserID(decimal? UserID, DateTime GPSTIME)
        {
            Entities db = new Entities();
            QWGL_USERTASKS model = db.QWGL_USERTASKS.Where(a => a.USERID == UserID && a.SDATE.Value.Year == GPSTIME.Year && a.SDATE.Value.Month == GPSTIME.Month && a.SDATE.Value.Day == GPSTIME.Day).FirstOrDefault();
            if (model != null)
            {
                List<string> list = new List<string>();

                decimal qut = model.USERTASKID;

                IQueryable<QWGL_AREAS> result = from uts in db.QWGL_USERTASKAREARS
                                                from area in db.QWGL_AREAS
                                                where uts.AREAID == area.AREAID
                                                && uts.USERTASKID == qut
                                                select area;

                foreach (var item in result)
                {
                    list.Add(item.GEOMETRY);
                }

                return list;


            }
            else
            {
                return null;
            }


        }
        /// <summary>
        /// 修改分析后的字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateISANALYZE(string id)
        {
            Entities db = new Entities();
            QWGL_USERHISTORYPOSITIONS _model = db.QWGL_USERHISTORYPOSITIONS.FirstOrDefault(t => t.UPID == id);
            if (_model != null)
            {
                _model.ISANALYZE = 1;
            }
            return db.SaveChanges();
        }
        //计算时间差,返回折合成分钟
        public static decimal DateDiff(DateTime DateTimeNew, DateTime DateTimeOld)
        {
            TimeSpan ts = DateTimeNew - DateTimeOld;
            //时间差转成只用分钟显示
            decimal dateDiff = (decimal)ts.TotalMinutes;
            return dateDiff;
        }

        /// <summary>
        /// 根据队员ID和定位时间读取休息区(用户ID和任务开始结束时间来确定唯一任务)
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<QWGL_RESTPOINTS> SelectRestAreaByAreaID(int UserID, DateTime GPSTIME)
        {
            Entities db = new Entities();
            List<QWGL_RESTPOINTS> re = new List<QWGL_RESTPOINTS>();
            QWGL_USERTASKS model = db.QWGL_USERTASKS.Where(a => a.USERID == UserID && a.SDATE.Value.Year == GPSTIME.Year && a.SDATE.Value.Month == GPSTIME.Month && a.SDATE.Value.Day == GPSTIME.Day).FirstOrDefault();
            if (model != null)
            {
                decimal qut = model.USERTASKID;

                IQueryable<QWGL_RESTPOINTS> result = from uts in db.QWGL_USERTASKAREARS
                                                     from resa in db.QWGL_RESTAREARS
                                                     from rest in db.QWGL_RESTPOINTS
                                                     where uts.AREAID == resa.AREAID
                                                     && resa.RESTID == rest.RESTID
                                                     && uts.USERTASKID == qut
                                                     select rest;
                if (result.Count() > 0)
                {
                    re.AddRange(result);
                }
            }

            return re;
        }

        //将报警信息存入数据库
        public static decimal SaveAlarmList(QWGL_ALARMMEMORYLOCATIONDATA alarmlist, string OutMapID)
        {
            Entities db = new Entities();

            QWGL_ALARMMEMORYLOCATIONDATA ta = new QWGL_ALARMMEMORYLOCATIONDATA();
            decimal outmapid = 0;
            decimal.TryParse(OutMapID, out outmapid);
            ta = db.QWGL_ALARMMEMORYLOCATIONDATA.Where(a => a.ID == outmapid).FirstOrDefault();

            decimal NEWID = 0;

            if (ta == null)
            {
                ta = new QWGL_ALARMMEMORYLOCATIONDATA();
                string sql = "SELECT SEQ_ALARMMEMORYLOCATIONDATAID.NEXTVAL FROM DUAL";
                NEWID = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
                ta.ID = NEWID;
            }

            ta.LONGITUDE = alarmlist.LONGITUDE == null ? 0 : (decimal)alarmlist.LONGITUDE;
            ta.LATITUDE = alarmlist.LATITUDE == null ? 0 : (decimal)alarmlist.LATITUDE;
            ta.X = alarmlist.X == null ? 0 : (decimal)alarmlist.X;
            ta.Y = alarmlist.Y == null ? 0 : (decimal)alarmlist.Y;
            ta.SPEED = alarmlist.SPEED == null ? 0 : (decimal)alarmlist.SPEED;
            ta.GPSTIME = alarmlist.GPSTIME;
            ta.CREATETIME = DateTime.Now;
            ta.ALARMENDTIME = alarmlist.ALARMENDTIME;
            ta.ALARMSTRATTIME = alarmlist.ALARMSTRATTIME;
            ta.ALARMTYPE = alarmlist.ALARMTYPE;
            ta.USERID = alarmlist.USERID;
            ta.STATE = 0;
            if (NEWID != 0)
            {
                db.QWGL_ALARMMEMORYLOCATIONDATA.Add(ta);
            }
            db.SaveChanges();
            return ta.ID;
        }

        /// <summary>
        /// 判断停留时间之内有没有上报事件
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static bool GetEventByTimeUserAlarm(DateTime StartTime, DateTime EndTime, decimal UserID)
        {
            Entities db = new Entities();
            XTGL_ZFSJS mode = db.XTGL_ZFSJS.Where(a => a.CREATEUSERID == UserID && a.CREATETTIME >= StartTime && a.CREATETTIME <= EndTime).FirstOrDefault();
            if (mode == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
