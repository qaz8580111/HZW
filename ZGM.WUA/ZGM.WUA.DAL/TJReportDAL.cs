using System;
using System.Collections.Generic;
using System.Linq;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class TJReportDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 获取部门事件上报列表
        /// </summary>
        /// <returns></returns>
        public List<UnitReportEventModel> GetUnitReportEventsList(decimal? DateType)
        {
            DateTime dt = DateTime.Now;
            List<UnitReportEventModel> list = new List<UnitReportEventModel>();
            List<SYS_UNITS> unlist = db.SYS_UNITS.Where(t => t.PARENTID == 17 && t.STATUSID == 1).OrderBy(t => t.UNITID).ToList();
            foreach (var item in unlist)
            {
                UnitReportEventModel model = GetUnitReportEventsCount(item.UNITID, DateType, dt.Year, dt.Month, dt.Day);
                model.UnitId = item.UNITID;
                model.UnitName = item.UNITNAME;
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 获取部门事件上报数量
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public UnitReportEventModel GetUnitReportEventsCount(decimal? UnitId, decimal? DateType, int YearValue = 0, int MonthValue = 0, int DayValue = 0)
        {
            UnitReportEventModel model = new UnitReportEventModel();
            if (DateType == 0)
            {
                IQueryable<TJ_PERSONEVENT_TODAY> tlist = db.TJ_PERSONEVENT_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.UNITID == UnitId);
                model.ReportEventCount = tlist.Max(t => t.PATROLRCOUNT);
                model.FinishEventCount = tlist.Max(t => t.PATROLCLOSEDCOUNT);
                model.ReportEventCount = model.ReportEventCount == null ? 0 : model.ReportEventCount;
                model.FinishEventCount = model.FinishEventCount == null ? 0 : model.FinishEventCount;
                return model;
            }
            else if (DateType == 1)
            {
                IQueryable<TJ_PERSONEVENT_HISTORY> hlist = db.TJ_PERSONEVENT_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.UNITID == UnitId);
                IQueryable<TJ_PERSONEVENT_TODAY> tlist = db.TJ_PERSONEVENT_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.UNITID == UnitId);
                model.ReportEventCount = hlist.Sum(t => t.PATROLRCOUNT) + tlist.Max(t => t.PATROLRCOUNT);
                model.FinishEventCount = hlist.Sum(t => t.PATROLCLOSEDCOUNT) + tlist.Max(t => t.PATROLCLOSEDCOUNT);
                model.ReportEventCount = model.ReportEventCount == null ? 0 : model.ReportEventCount;
                model.FinishEventCount = model.FinishEventCount == null ? 0 : model.FinishEventCount;
                return model;
            }
            else
            {
                IQueryable<TJ_PERSONEVENT_HISTORY> hlist = db.TJ_PERSONEVENT_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.UNITID == UnitId);
                IQueryable<TJ_PERSONEVENT_TODAY> tlist = db.TJ_PERSONEVENT_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.UNITID == UnitId);
                model.ReportEventCount = hlist.Sum(t => t.PATROLRCOUNT) + tlist.Max(t => t.PATROLRCOUNT);
                model.FinishEventCount = hlist.Sum(t => t.PATROLCLOSEDCOUNT) + tlist.Max(t => t.PATROLCLOSEDCOUNT);
                model.ReportEventCount = model.ReportEventCount == null ? 0 : model.ReportEventCount;
                model.FinishEventCount = model.FinishEventCount == null ? 0 : model.FinishEventCount;
                return model;
            }
        }

        /// <summary>
        /// 获取片区事件上报列表
        /// </summary>
        /// <returns></returns>
        public List<ZoneReportEventModel> GetZoneReportEventsList(decimal? DateType)
        {
            DateTime dt = DateTime.Now;
            List<ZoneReportEventModel> list = new List<ZoneReportEventModel>();
            List<SYS_ZONES> zlist = db.SYS_ZONES.OrderBy(t => t.ZONEID).ToList();
            foreach (var item in zlist)
            {
                ZoneReportEventModel model = new ZoneReportEventModel();
                model.Count = GetZoneReportEventsCount(item.ZONEID, DateType, dt.Year, dt.Month, dt.Day);
                model.ZoneId = item.ZONEID;
                model.ZoneName = item.ZONENAME;
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 获取片区事件上报数量
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public decimal? GetZoneReportEventsCount(decimal? ZoneId, decimal? DateType, int YearValue = 0, int MonthValue = 0, int DayValue = 0)
        {
            if (DateType == 0)
            {
                decimal? result = db.TJ_ZONEENENT_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.ZONEID == ZoneId).Sum(t => t.PATROLRCOUNT);
                return result == null ? 0 : result;
            }
            else if (DateType == 1)
            {
                decimal? result1 = db.TJ_ZONEENENT_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.ZONEID == ZoneId).Sum(t => t.PATROLRCOUNT);
                decimal? result2 = db.TJ_ZONEENENT_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.ZONEID == ZoneId).Sum(t => t.PATROLRCOUNT);
                result1 = result1 == null ? 0 : result1;
                result2 = result2 == null ? 0 : result2;
                return result1 + result2;
            }
            else
            {
                decimal? result1 = db.TJ_ZONEENENT_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.ZONEID == ZoneId).Sum(t => t.PATROLRCOUNT);
                decimal? result2 = db.TJ_ZONEENENT_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.ZONEID == ZoneId).Sum(t => t.PATROLRCOUNT);
                result1 = result1 == null ? 0 : result1;
                result2 = result2 == null ? 0 : result2;
                return result1 + result2;
            }
        }

        /// <summary>
        /// 获取部门队员签到数量
        /// </summary>
        /// <returns></returns>
        public List<UnitReportSignModel> GetUnitSignInList(decimal? DateType)
        {
            DateTime dt = DateTime.Now;
            List<UnitReportSignModel> list = new List<UnitReportSignModel>();
            List<SYS_UNITS> unlist = db.SYS_UNITS.Where(t => t.PARENTID == 17 && t.STATUSID == 1).OrderBy(t => t.UNITID).ToList();
            foreach (var item in unlist)
            {
                UnitReportSignModel model = GetUnitSignInInfo(item.UNITID, DateType, dt.Year, dt.Month, dt.Day);
                model.UnitId = item.UNITID;
                model.UnitName = item.UNITNAME;
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 获取部门队员签到数量
        /// </summary>
        /// <returns></returns>
        public UnitReportSignModel GetUnitSignInInfo(decimal? UnitId, decimal? DateType, int YearValue = 0, int MonthValue = 0, int DayValue = 0)
        {
            UnitReportSignModel model = new UnitReportSignModel();
            model.SignCount = 0;
            model.UnSignCount = 0;
            int ResultSMinute = 0;
            int ResultEMinute = 0;
            DateTime NowYMD = DateTime.Now.Date;
            var list = (from ust in db.QWGL_USERSIGNINTASKS
                        from usa in db.QWGL_SIGNINAREAS
                        from u in db.SYS_USERS
                        where ust.AREAID == usa.AREAID && ust.USERID == u.USERID
                        && usa.STATE == 1 && u.UNITID == UnitId
                        select new
                        {
                            SignSTime = usa.SDATE,
                            SignETime = usa.EDATE,
                            SigninSTime = ust.SIGNINTIME,
                            SigninETime = ust.SIGNOUTTIME,
                            SigninDay = ust.SIGNINDAY
                        }).ToList();
            if (DateType == 0)
                list = list.Where(t => t.SigninDay.Value.Year == YearValue && t.SigninDay.Value.Month == MonthValue && t.SigninDay.Value.Day == DayValue).ToList();
            if (DateType == 1)
                list = list.Where(t => t.SigninDay.Value.Year == YearValue && t.SigninDay.Value.Month == MonthValue).ToList();
            if (DateType == 2)
                list = list.Where(t => t.SigninDay.Value.Year == YearValue).ToList();
            foreach (var item in list)
            {
                ResultSMinute = item.SigninSTime == null ? 1 : (item.SigninSTime.Value.Hour * 60 + item.SigninSTime.Value.Minute) - (item.SignSTime.Value.Hour * 60 + item.SignSTime.Value.Minute) > 0 ? 1 : 0;
                ResultEMinute = item.SigninETime == null ? -1 : (item.SigninETime.Value.Hour * 60 + item.SigninETime.Value.Minute) - (item.SignETime.Value.Hour * 60 + item.SignETime.Value.Minute) < 0 ? -1 : 1;
                if (ResultSMinute <= 0 && ResultEMinute >= 0)
                    model.SignCount++;
                else
                    model.UnSignCount++;
            }
            return model;
        }

        public UnitReportAlarmModel GetPieReportAlarm(decimal? DateType)
        {
            ZGMEntities db = new ZGMEntities();
            DateTime starttime = Convert.ToDateTime("0001-01-01");
            DateTime endtime = Convert.ToDateTime("0001-01-01");
            if (DateType == 0)
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                endtime = starttime.AddDays(1);
                //starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddMonths(-2);
                //endtime = starttime.AddMonths(2);
            }
            else if (DateType == 1)
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                endtime = starttime.AddMonths(1);
            }
            else
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
                endtime = starttime.AddYears(1);
            }
            string sql = string.Format(@"
select count(decode(t.ALARMTYPE,'1',1)) as OverTimeCount,
count(decode(t.ALARMTYPE,'2',1)) as OverBorderCount,
count(decode(t.ALARMTYPE,'3',1)) as OverLineCount
from QWGL_ALARMMEMORYLOCATIONDATA t
where  t.ALARMSTRATTIME>=to_date('{0}','yyyy-mm-dd hh24:mi:ss')
and t.ALARMSTRATTIME<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')", starttime, endtime);
            UnitReportAlarmModel result = new UnitReportAlarmModel();
            result = db.Database.SqlQuery<UnitReportAlarmModel>(sql).FirstOrDefault();
            return result;
        }


        /// <summary>
        /// 获取人员报警情况
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public decimal? GetUnitAlarmCount(decimal? AlarmType, decimal? DateType, int YearValue = 0, int MonthValue = 0, int DayValue = 0)
        {
            if (DateType == 0)
            {
                decimal? result = db.TJ_UNITPERSONPOLICE_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                return result == null ? 0 : result;
            }
            else if (DateType == 1)
            {
                decimal? result1 = db.TJ_UNITPERSONPOLICE_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                decimal? result2 = db.TJ_UNITPERSONPOLICE_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                result1 = result1 == null ? 0 : result1;
                result2 = result2 == null ? 0 : result2;
                return result1 + result2;
            }
            else
            {
                decimal? result1 = db.TJ_UNITPERSONPOLICE_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                decimal? result2 = db.TJ_UNITPERSONPOLICE_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                result1 = result1 == null ? 0 : result1;
                result2 = result2 == null ? 0 : result2;
                return result1 + result2;
            }
        }

        /// <summary>
        /// 获取报警类型统计
        /// </summary>
        /// <returns></returns>
        public List<UnitReportProwledModel> GetUnitProwledList(decimal? DateType)
        {
            //DateTime dt = DateTime.Now;
            //List<UnitReportProwledModel> list = new List<UnitReportProwledModel>();
            //List<SYS_UNITS> unlist = db.SYS_UNITS.Where(t => t.PARENTID == 17 && t.STATUSID == 1).OrderBy(t=>t.UNITID).ToList();
            //foreach (var item in unlist)
            //{
            //    UnitReportProwledModel model = new UnitReportProwledModel();
            //    model.OverBorderCount = GetUnitProwledCount(item.UNITID, 1, DateType, dt.Year, dt.Month, dt.Day);
            //    model.OverTimeCount = GetUnitProwledCount(item.UNITID, 2, DateType, dt.Year, dt.Month, dt.Day);
            //    model.OverLineCount = GetUnitProwledCount(item.UNITID, 3, DateType, dt.Year, dt.Month, dt.Day);
            //    model.UnitId = item.UNITID;
            //    model.UnitName = item.UNITNAME;
            //    list.Add(model);
            //}
            //return list;
            ZGMEntities db = new ZGMEntities();
            DateTime starttime = Convert.ToDateTime("0001-01-01");
            DateTime endtime = Convert.ToDateTime("0001-01-01");
            if (DateType == 0)
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                endtime = starttime.AddDays(1);
                //starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddMonths(-2);
                //endtime = starttime.AddMonths(2);
            }
            else if (DateType == 1)
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                endtime = starttime.AddMonths(1);
            }
            else
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
                endtime = starttime.AddYears(1);
            }

            string sql = string.Format(@"with tab1
as
(
select sut.UNITID,alarm.ALARMTYPE,count(1) bjcount from QWGL_ALARMMEMORYLOCATIONDATA alarm
left join SYS_USERS su on su.USERID=alarm.userid
left join SYS_UNITS sut on sut.UNITID=su.UNITID
where  sut.STATUSID=1 and sut.PARENTID=17  and alarm.ALARMSTRATTIME>=to_date('{0}','yyyy-mm-dd hh24:mi:ss')
and alarm.ALARMSTRATTIME<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
group by sut.UNITID,alarm.ALARMTYPE)
,
tab2
as
(
select tab1.bjcount,tab1.UNITID from tab1 where tab1.ALARMTYPE=1
),
tab3
as (
select tab1.bjcount,tab1.UNITID from tab1 where tab1.ALARMTYPE=2
),
tab4
as
(
select tab1.bjcount,tab1.UNITID from tab1 where tab1.ALARMTYPE=3
)
select NVL(tab2.bjcount,0) as OverTimeCount ,NVL(tab3.bjcount,0) as OverBorderCount ,NVL(tab4.bjcount,0) as OverLineCount ,sut.UNITID,sut.unitname from SYS_UNITS sut
left join tab2 on sut.unitid=tab2.UNITID
left join tab3 on sut.unitid=tab3.UNITID
left join tab4 on sut.unitid=tab4.UNITID
where  sut.STATUSID=1 and sut.PARENTID=17 
ORDER BY unitid", starttime, endtime);
            List<UnitReportProwledModel> list = new List<UnitReportProwledModel>();
            list = db.Database.SqlQuery<UnitReportProwledModel>(sql).ToList();
            return list;
        }

        /// <summary>
        /// 获取报警类型数量
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public decimal? GetUnitProwledCount(decimal? UnitId, decimal? AlarmType, decimal? DateType, int YearValue = 0, int MonthValue = 0, int DayValue = 0)
        {
            if (DateType == 0)
            {
                decimal? result = db.TJ_UNITPERSONPOLICE_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.UNITID == UnitId && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                return result == null ? 0 : result;
            }
            else if (DateType == 1)
            {
                decimal? result1 = db.TJ_UNITPERSONPOLICE_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.UNITID == UnitId && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                decimal? result2 = db.TJ_UNITPERSONPOLICE_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.UNITID == UnitId && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                result1 = result1 == null ? 0 : result1;
                result2 = result2 == null ? 0 : result2;
                return result1 + result2;
            }
            else
            {
                decimal? result1 = db.TJ_UNITPERSONPOLICE_HISTORY.Where(t => t.STATTIME.Year == YearValue && t.UNITID == UnitId && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                decimal? result2 = db.TJ_UNITPERSONPOLICE_TODAY.Where(t => t.STATTIME.Year == YearValue && t.STATTIME.Month == MonthValue && t.STATTIME.Day == DayValue && t.UNITID == UnitId && t.POLICETYPE == AlarmType).Sum(t => t.POLICECOUNT);
                result1 = result1 == null ? 0 : result1;
                result2 = result2 == null ? 0 : result2;
                return result1 + result2;
            }
        }

        /// <summary>
        /// 获取队员路程列表
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public List<ReportWalkModel> GetPersonWalkList(decimal? DateType)
        {
            List<ReportWalkModel> list = new List<ReportWalkModel>();
            DateTime dt = DateTime.Now;
            if (DateType == 0)
            {
                list = (from pt in db.TJ_PERSONWALK_TODAY
                        where pt.STATTIME.Year == dt.Year && pt.STATTIME.Month == dt.Month &&
                       pt.STATTIME.Day == dt.Day
                        orderby pt.WALKSUM descending
                        select new ReportWalkModel
                        {
                            UserId = pt.PERSONID,
                            UserName = pt.PERSONNAME,
                            WalkCount = pt.WALKSUM
                        }).Take(10).ToList();
            }

            if (DateType == 1)
            {
                List<ReportWalkModel> tlist = (from pt in db.TJ_PERSONWALK_TODAY
                                               where pt.STATTIME.Year == dt.Year && pt.STATTIME.Month == dt.Month &&
                                                pt.STATTIME.Day == dt.Day
                                               orderby pt.WALKSUM descending
                                               select new ReportWalkModel
                                               {
                                                   UserId = pt.PERSONID,
                                                   UserName = pt.PERSONNAME,
                                                   WalkCount = pt.WALKSUM
                                               }).ToList();
                List<ReportWalkModel> hlist = (from ph in db.TJ_PERSONWALK_HISTORY
                                               where ph.STATTIME.Year == dt.Year && ph.STATTIME.Month == dt.Month
                                               group ph by new { ph.PERSONID, ph.PERSONNAME } into g
                                               orderby g.Sum(t => t.WALKSUM) descending
                                               select new ReportWalkModel
                                               {
                                                   UserId = g.Key.PERSONID,
                                                   UserName = g.Key.PERSONNAME,
                                                   WalkCount = g.Sum(t => t.WALKSUM)
                                               }).ToList();

                list = hlist.Union(tlist).ToList();
                list = (from l in list
                        group l by new { l.UserId, l.UserName } into g
                        orderby g.Sum(t => t.WalkCount) descending
                        select new ReportWalkModel
                         {
                             UserId = g.Key.UserId,
                             UserName = g.Key.UserName,
                             WalkCount = g.Sum(t => t.WalkCount)
                         }).Take(10).ToList();
            }

            if (DateType == 2)
            {
                List<ReportWalkModel> tlist = (from pt in db.TJ_PERSONWALK_TODAY
                                               where pt.STATTIME.Year == dt.Year && pt.STATTIME.Month == dt.Month &&
                                               pt.STATTIME.Day == dt.Day
                                               orderby pt.WALKSUM descending
                                               select new ReportWalkModel
                                               {
                                                   UserId = pt.PERSONID,
                                                   UserName = pt.PERSONNAME,
                                                   WalkCount = pt.WALKSUM
                                               }).ToList();
                List<ReportWalkModel> hlist = (from ph in db.TJ_PERSONWALK_HISTORY
                                               where ph.STATTIME.Year == dt.Year
                                               group ph by new { ph.PERSONID, ph.PERSONNAME } into g
                                               orderby g.Sum(t => t.WALKSUM) descending
                                               select new ReportWalkModel
                                               {
                                                   UserId = g.Key.PERSONID,
                                                   UserName = g.Key.PERSONNAME,
                                                   WalkCount = g.Sum(t => t.WALKSUM)
                                               }).ToList();
                list = hlist.Union(tlist).ToList();
                list = (from l in list
                        group l by new { l.UserId, l.UserName } into g
                        orderby g.Sum(t => t.WalkCount) descending
                        select new ReportWalkModel
                        {
                            UserId = g.Key.UserId,
                            UserName = g.Key.UserName,
                            WalkCount = g.Sum(t => t.WalkCount)
                        }).Take(10).ToList();
            }

            return list;
        }

    }
}
