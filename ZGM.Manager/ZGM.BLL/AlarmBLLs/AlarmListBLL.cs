using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZGM.BLL;
using ZGM.Model;
using System.Text.RegularExpressions;

namespace ZGM.BLL.AlarmBLLs
{
    public class AlarmListBLL
    {
        /// <summary>
        /// 添加30分钟停留超时报警记录
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static int AddTeamMemoryLocationData(ZGM.Model.QWGL_ALARMMEMORYLOCATIONDATA va)
        {
            Entities db = new Entities();
            int al = db.QWGL_ALARMMEMORYLOCATIONDATA.Where(t => t.ALARMSTRATTIME == va.ALARMSTRATTIME && t.ALARMENDTIME == va.ALARMENDTIME && t.USERID == va.USERID && t.ALARMTYPE == va.ALARMTYPE).Count();
            if (al == 0)
            {
                va.CREATETIME = DateTime.Now;
                db.QWGL_ALARMMEMORYLOCATIONDATA.Add(va);
            }
            return db.SaveChanges();

        }

        /// <summary>
        /// 添加Catch捕捉的异常记录
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static int AddTeamAlarmErrorCatchData(ZGM.Model.QWGL_ALARMERRORCATCH va)
        {
            Entities db = new Entities();

            va.CREATETIME = DateTime.Now;
            db.QWGL_ALARMERRORCATCH.Add(va);
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据队员ID和定位时间读取巡查范围(用户ID和任务开始结束时间来确定唯一任务)
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<string> SelectInspectionScopeByUserID(int UserID, DateTime GPSTIME)
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

                //decimal areaid = (decimal)db.QWGL_USERTASKAREARS.FirstOrDefault(t => t.USERTASKID == qut).AREAID;
                //string re = db.QWGL_AREAS.FirstOrDefault(t => t.AREAID == areaid).GEOMETRY;
                //return re;
            }
            else
            {
                return null;
            }


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


                //IQueryable<QWGL_USERTASKAREARS> areaid = db.QWGL_USERTASKAREARS.Where(t => t.USERTASKID == qut);

                //IQueryable<QWGL_RESTAREARS> Rests = db.QWGL_RESTAREARS.Where(a => a.AREAID in areaid);
                //foreach (var rest in Rests)
                //{
                //    if (db.QWGL_RESTPOINTS.Where(t => t.RESTID == rest.RESTID).Count() > 0)
                //    {
                //        QWGL_RESTPOINTS item = db.QWGL_RESTPOINTS.Where(t => t.RESTID == rest.RESTID).FirstOrDefault();
                //        re.Add(item);
                //    }
                //}
            }

            return re;
        }

        /// <summary>
        /// 获取队员任务列表
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static IQueryable<QWGL_USERTASKS> GetAllUserTaskAreas()
        {
            Entities db = new Entities();
            IQueryable<QWGL_USERTASKS> us = db.QWGL_USERTASKS;
            return us;
        }

       /// <summary>
       /// 获取最后一次定位信息
       /// </summary>
       /// <param name="userid"></param>
       /// <returns></returns>
        public static IQueryable<QWGL_USERLATESTPOSITIONS> GetAllUserLatestPositions(decimal userid)
        {
            Entities db = new Entities();
            IQueryable<QWGL_USERLATESTPOSITIONS> us = db.QWGL_USERLATESTPOSITIONS.Where(t => t.USERID == userid);
            return us;
        }

        /// <summary>
        /// 获取全部报警信息列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<QWGL_ALARMMEMORYLOCATIONDATA> GetAllAlarmList()
        {
            Entities db = new Entities();
            return db.QWGL_ALARMMEMORYLOCATIONDATA;
        }

        /// <summary>
        /// 获取请假表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<QWGL_LEAVES> GetAllLeaves()
        {
            Entities db = new Entities();
            return db.QWGL_LEAVES;
        }



        /// <summary>
        /// 获取1000条巡查数据存入临时表
        /// </summary>
        /// <returns></returns>
        public static int Get1000PatrolDataToTemporary(decimal? userid)
        {
            Entities db = new Entities();
            List<QWGL_USERHISTORYPOSITIONS> pss = db.QWGL_USERHISTORYPOSITIONS.Where(a => a.ISANALYZE != 1 && a.USERID == userid)
                                                 .OrderBy(a => a.POSITIONTIME).Take(1000).ToList();
            int i = 0;
            //取出1000条数据存入本地数据库临时表
            foreach (var ps in pss)
            {
                QWGL_ALARMDETAILS list = new QWGL_ALARMDETAILS();
                list.MEMBERID = ps.USERID;
                list.GPSTIME = ps.POSITIONTIME;
                list.SPEED = ps.SPEED;
                list.LON = ps.X2000;
                list.LAT = ps.Y2000;
                list.ID = i + 1;
                db.QWGL_ALARMDETAILS.Add(list);
                Console.WriteLine("插入临时表QWGL_USERHISTORYPOSITIONS,第(" + i + ")条数据!");
                i++;
            }
            Console.WriteLine("读取1000条数据");
            int j = 0;
            //修改状态为已分析
            foreach (var ps in pss)
            {
                QWGL_USERHISTORYPOSITIONS _model = db.QWGL_USERHISTORYPOSITIONS.FirstOrDefault(t => t.UPID == ps.UPID);
                if (_model != null)
                {
                    _model.ISANALYZE = 1;
                    Console.WriteLine("QWGL_USERHISTORYPOSITIONS,第(" + j + ")条数据已分析!");
                }

                j++;
            }
            Console.WriteLine("分析1000条数据");
            return db.SaveChanges();
        }


        /// <summary>
        /// 从临时数据表中读取巡查数据
        /// </summary>
        /// <returns></returns >
        public static IQueryable<QWGL_ALARMDETAILS> Get1000PatrolData()
        {
            Entities db = new Entities();
            return db.QWGL_ALARMDETAILS;
        }


        /// <summary>
        /// 清空临时表
        /// </summary>
        /// <returns></returns>
        public static decimal ClearALARMDETAILS()
        {
            Entities db = new Entities();
            string sql = "delete from QWGL_ALARMDETAILS";
            decimal re = db.Database.ExecuteSqlCommand(sql);
            return re;
        }


        /// <summary>
        /// 获取一个停留超时报警记录序列
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewAlarmListID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_ALARMMEMORYLOCATIONDATAID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个错误捕捉信息序列
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewErrorCatchID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_ALARMERRORCATCHID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 根据队员ID和定位时间读取休息区(用户ID和任务开始结束时间来确定唯一任务)（车辆报警）
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<QWGL_RESTPOINTS> SelectRestAreaByCarID(int CarID, DateTime GPSTIME)
        {
            Entities db = new Entities();
            List<QWGL_RESTPOINTS> re = new List<QWGL_RESTPOINTS>();

            QWGL_CARTASKS model = db.QWGL_CARTASKS.Where(a => a.CARID == CarID && a.SDATE.Value.Year == GPSTIME.Year && a.SDATE.Value.Month == GPSTIME.Month && a.SDATE.Value.Day == GPSTIME.Day).FirstOrDefault();
            if (model != null)
            {
                decimal qut = model.CARTASKID;

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

        /// <summary>
        /// 获取车辆任务列表（车辆报警）
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
        /// 清空临时表(车辆报警)
        /// </summary>
        /// <returns></returns>
        public static decimal ClearCarALARMDETAILS()
        {
            Entities db = new Entities();
            string sql = "delete from QWGL_CARALARMDETAILS";
            decimal re = db.Database.ExecuteSqlCommand(sql);
            return re;
        }

        /// <summary>
        /// 获取1000条巡查数据存入临时表(车辆报警)
        /// </summary>
        /// <returns></returns>
        public static int GetCar1000PatrolDataToTemporary()
        {
            Entities db = new Entities();
            List<QWGL_CARHISTORYPOSITIONS> pss = db.QWGL_CARHISTORYPOSITIONS.Where(a => a.ISANALYZE != 1)
                                                 .OrderBy(a => a.LOCATETIME).Take(1000).ToList();
            int i = 0;
            //取出1000条数据存入本地数据库临时表
            foreach (var ps in pss)
            {
                QWGL_CARALARMDETAILS list = new QWGL_CARALARMDETAILS();
                list.CARID = ps.CARID;
                list.GPSTIME = ps.LOCATETIME;
                list.SPEED = ps.SPEED;
                list.LON = ps.X2000;
                list.LAT = ps.Y2000;
                list.ID = i + 1;
                db.QWGL_CARALARMDETAILS.Add(list);
                // Console.WriteLine("插入临时表QWGL_USERHISTORYPOSITIONS,第(" + i + ")条数据!");
                i++;
            }
            Console.WriteLine("读取1000条数据");
            int j = 0;
            //修改状态为已分析
            foreach (var ps in pss)
            {
                QWGL_CARHISTORYPOSITIONS _model = db.QWGL_CARHISTORYPOSITIONS.FirstOrDefault(t => t.CLHID == ps.CLHID);
                if (_model != null)
                {
                    _model.ISANALYZE = 1;
                    // Console.WriteLine("QWGL_USERHISTORYPOSITIONS,第(" + j + ")条数据已分析!");
                }

                j++;
            }
            Console.WriteLine("分析1000条数据");
            return db.SaveChanges();
        }
        /// <summary>
        /// 从临时数据表中读取巡查数据
        /// </summary>
        /// <returns></returns >
        public static IQueryable<QWGL_CARALARMDETAILS> GetCar1000PatrolData()
        {
            Entities db = new Entities();
            return db.QWGL_CARALARMDETAILS;
        }

        /// <summary>
        /// 获取一个错误捕捉信息序列(车辆报警)
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewCarErrorCatchID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_CARALARMERRORCATCHID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }
        /// <summary>
        /// 获取一个停留超时报警记录序列(车辆报警)
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewCarAlarmListID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_CARALARMMEMORYDATAID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }
        /// <summary>
        /// 添加Catch捕捉的异常记录(车辆报警)
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static int AddCarTeamAlarmErrorCatchData(ZGM.Model.QWGL_CARALARMERRORCATCH va)
        {
            Entities db = new Entities();

            va.CREATETIME = DateTime.Now;
            db.QWGL_CARALARMERRORCATCH.Add(va);
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据队员ID和定位时间读取巡查范围(用户ID和任务开始结束时间来确定唯一任务)(车辆报警)
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<string> SelectInspectionScopeByCarID(int CarID, DateTime GPSTIME)
        {
            Entities db = new Entities();
            QWGL_CARTASKS model = db.QWGL_CARTASKS.Where(a => a.CARID == CarID && a.SDATE.Value.Year == GPSTIME.Year && a.SDATE.Value.Month == GPSTIME.Month && a.SDATE.Value.Day == GPSTIME.Day).FirstOrDefault();
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
            }
            else
            {
                return null;
            }


        }
        /// <summary>
        /// 添加30分钟停留超时报警记录(车辆报警)
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static int AddCarTeamMemoryData(ZGM.Model.QWGL_CARALARMMEMORYDATA va)
        {
            Entities db = new Entities();
            int al = db.QWGL_CARALARMMEMORYDATA.Where(t => t.ALARMSTRATTIME == va.ALARMSTRATTIME && t.ALARMENDTIME == va.ALARMENDTIME && t.CARID == va.CARID && t.ALARMTYPE == va.ALARMTYPE).Count();
            if (al == 0)
            {
                va.CREATETIME = DateTime.Now;
                db.QWGL_CARALARMMEMORYDATA.Add(va);
            }
            return db.SaveChanges();

        }
        /// <summary>
        ///   修改离线报警
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        public static int EditTeamMemoryLocationData(ZGM.Model.QWGL_ALARMMEMORYLOCATIONDATA va)
        {
            Entities db = new Entities();
            ZGM.Model.QWGL_ALARMMEMORYLOCATIONDATA al = db.QWGL_ALARMMEMORYLOCATIONDATA.Where(t => t.ID == va.ID).FirstOrDefault();
            if (al != null)
            {
                al.ALARMENDTIME = va.ALARMENDTIME;
                al.CREATETIME = va.CREATETIME;
            }
            return db.SaveChanges();

        }
    }
}
