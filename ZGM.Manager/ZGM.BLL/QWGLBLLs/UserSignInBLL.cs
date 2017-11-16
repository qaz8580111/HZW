using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.BLL.UserBLLs;
using ZGM.Common.Enums;
using ZGM.Model.PhoneModel;
using Common;

namespace ZGM.BLL.QWGLBLLs
{
    public class UserSignInBLL
    {

        /// <summary>
        /// 查询签到区域坐标集合
        /// </summary>
        /// <returns></returns>
        public static List<SignInAreaPartModel> GetAreaGeometry(decimal UserId)
        {
            Entities db = new Entities();
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToLongDateString());
            List<SignInAreaPartModel> list = (from sa in db.QWGL_SIGNINAREAS
                                              join us in db.QWGL_USERSIGNINTASKS
                                              on sa.AREAID equals us.AREAID
                                              where us.USERID == UserId && us.SIGNINDAY.Value.Year == nowdate.Year && us.SIGNINDAY.Value.Month == nowdate.Month && us.SIGNINDAY.Value.Day == nowdate.Day
                                              select new SignInAreaPartModel
                                              {
                                                  AREAID = sa.AREAID,
                                                  GEOMETRY = sa.GEOMETRY,
                                                  AREANAME = sa.AREANAME,
                                                  AREADESCRIPTION = sa.AREADESCRIPTION
                                              }).ToList();
            return list;
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

        //84转2000
        public static MapPoint Geo84ToGeo2000(string Longitude, string Latitude)
        {
            MapPoint geometry = new MapPoint();
            string geo84 = double.Parse(Longitude) + "," + double.Parse(Latitude);
            string geo2000 = MapXYConvent.WGS84ToCGCS2000(geo84);
            geometry.X = double.Parse(geo2000.Split(',')[0]);
            geometry.Y = double.Parse(geo2000.Split(',')[1]);

            return geometry;
        }

        /// <summary>
        /// 添加队员签到 更新签到任务关系表
        /// </summary>
        /// <returns></returns>
        public static int UpdateSignInStatu(decimal userid)
        {
            Entities db = new Entities();
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToLongDateString());
            QWGL_USERSIGNINTASKS list = db.QWGL_USERSIGNINTASKS.FirstOrDefault(t => t.USERID == userid && t.SIGNINDAY.Value.Year == nowdate.Year && t.SIGNINDAY.Value.Month == nowdate.Month && t.SIGNINDAY.Value.Day == nowdate.Day);
            if (list.SIGNINTIME == null)
            {
                list.SIGNINTIME = DateTime.Now;
                return db.SaveChanges();
            }
            else
            {
                list.SIGNOUTTIME = DateTime.Now;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 添加队员签到
        /// </summary>
        /// <returns></returns>
        public static int AddUserSignIn(QWGL_USERSIGNINS model)
        {
            Entities db = new Entities();
            db.QWGL_USERSIGNINS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据UserId获取所有签到记录
        /// </summary>
        /// <returns></returns>
        public static List<UserSignInModel> GetSignInListById(decimal UserId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserSignInModel> list = new List<UserSignInModel>();
            DateTime nowdate = DateTime.Now;

            list = (from us in db.QWGL_USERSIGNINS

                    join u in db.SYS_USERS
                    on us.USERID equals u.USERID

                    where us.USERID == UserId
                    orderby us.SIGNINTIME descending
                    select new UserSignInModel
                    {
                        SGID = us.SIGNINID,
                        USERNAME = u.USERNAME,
                        USERAVATAR = u.AVATAR,
                        SIGNINALL = us.SIGNINTIME,
                        QueryName = u.USERNAME.ToUpper()
                    }).ToList();
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            list = list.Skip(PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.SIGNINDATE = ((DateTime)item.SIGNINALL).ToLongDateString();
                item.SIGNINTIME = (item.SIGNINALL.Value.Hour.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Hour.ToString() : item.SIGNINALL.Value.Hour.ToString()) + ":" + (item.SIGNINALL.Value.Minute.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Minute.ToString() : item.SIGNINALL.Value.Minute.ToString());
                item.SIGNINWEEK = ((DateTime)item.SIGNINALL).ToString("dddd", new System.Globalization.CultureInfo("zh-CN"));
            }

            return list;
        }

        /// <summary>
        /// 部门所有用户签到记录
        /// </summary>
        /// <returns></returns>
        public static List<UserSignInModel> GetAllUserSignIn(decimal UserId, decimal UnitId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserSignInModel> list = new List<UserSignInModel>();
            DateTime nowdate = DateTime.Now;

            list = (from us in db.QWGL_USERSIGNINS

                    join u in db.SYS_USERS
                    on us.USERID equals u.USERID

                    where u.UNITID == UnitId && us.USERID != UserId
                    orderby us.SIGNINTIME descending
                    select new UserSignInModel
                    {
                        SGID = us.SIGNINID,
                        USERNAME = u.USERNAME,
                        USERAVATAR = u.AVATAR,
                        SIGNINALL = us.SIGNINTIME,
                        QueryName = u.USERNAME.ToUpper()
                    }).ToList();
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            list = list.Skip(PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.SIGNINDATE = ((DateTime)item.SIGNINALL).ToLongDateString();
                item.SIGNINTIME = (item.SIGNINALL.Value.Hour.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Hour.ToString() : item.SIGNINALL.Value.Hour.ToString()) + ":" + (item.SIGNINALL.Value.Minute.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Minute.ToString() : item.SIGNINALL.Value.Minute.ToString());
                item.SIGNINWEEK = ((DateTime)item.SIGNINALL).ToString("dddd", new System.Globalization.CultureInfo("zh-CN"));
            }

            return list;
        }

        /// <summary>
        /// 根据UnitId获取团队成员今日签到记录
        /// </summary>
        /// <returns></returns>
        public static List<UserSignInModel> GetSignInListByUnitId(decimal UnitId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserSignInModel> list = new List<UserSignInModel>();
            DateTime nowdate = DateTime.Now;

            list = (from ust in db.QWGL_USERSIGNINTASKS

                    join u in db.SYS_USERS
                    on ust.USERID equals u.USERID

                    join us in db.QWGL_USERSIGNINS
                    on new { id = u.USERID, id1 = nowdate.Year, id2 = nowdate.Month, id3 = nowdate.Day } equals new { id = (decimal)us.USERID, id1 = us.SIGNINTIME.Value.Year, id2 = us.SIGNINTIME.Value.Month, id3 = us.SIGNINTIME.Value.Day }

                    where u.UNITID == UnitId && ust.SIGNINTIME.Value.Year == nowdate.Year && ust.SIGNINTIME.Value.Month == nowdate.Month && ust.SIGNINTIME.Value.Day == nowdate.Day && ust.SIGNINTIME != null && ust.SIGNOUTTIME != null
                    orderby us.SIGNINTIME descending

                    select new UserSignInModel
                    {
                        USERID = (decimal)u.USERID,
                        SGID = us.SIGNINID,
                        USERNAME = u.USERNAME,
                        USERAVATAR = u.AVATAR,
                        SIGNINALL = us.SIGNINTIME,
                        ACSIGNINTIME = ust.SIGNINTIME,
                        PLANSIGNINTIME = ust.SIGNOUTTIME,
                        QueryName = u.USERNAME.ToUpper()
                    }).ToList();
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            list = list.Distinct(new UserSignInNoComparer()).Skip(PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.SIGNINDATE = ((DateTime)item.SIGNINALL).ToLongDateString();
                item.SIGNINTIME = (item.SIGNINALL.Value.Hour.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Hour.ToString() : item.SIGNINALL.Value.Hour.ToString()) + ":" + (item.SIGNINALL.Value.Minute.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Minute.ToString() : item.SIGNINALL.Value.Minute.ToString());
                item.SIGNINWEEK = ((DateTime)item.SIGNINALL).ToString("dddd", new System.Globalization.CultureInfo("zh-CN"));
            }

            return list;
        }

        /// <summary>
        /// 获取团队成员今日未签到记录
        /// </summary>
        /// <returns></returns>
        public static List<UserSignInModel> GetUnSignInListByUnitId(decimal UnitId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserSignInModel> list = new List<UserSignInModel>();
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToLongDateString());

            list = (from ust in db.QWGL_USERSIGNINTASKS

                    join u in db.SYS_USERS
                    on ust.USERID equals u.USERID

                    where u.UNITID == UnitId && (ust.SIGNINTIME == null || ust.SIGNOUTTIME == null) && ust.SIGNINDAY.Value.Year == nowdate.Year && ust.SIGNINDAY.Value.Month == nowdate.Month && ust.SIGNINDAY.Value.Day == nowdate.Day

                    select new UserSignInModel
                    {
                        SGID = ust.SGID,
                        USERNAME = u.USERNAME,
                        USERAVATAR = u.AVATAR,
                        SIGNINALL = ust.SIGNINDAY,
                        QueryName = u.USERNAME.ToUpper()
                    }).ToList();
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            list = list.Skip(PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.SIGNINDATE = ((DateTime)item.SIGNINALL).ToLongDateString();
                item.SIGNINTIME = (item.SIGNINALL.Value.Hour.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Hour.ToString() : item.SIGNINALL.Value.Hour.ToString()) + ":" + (item.SIGNINALL.Value.Minute.ToString().Length == 1 ? "0" + item.SIGNINALL.Value.Minute.ToString() : item.SIGNINALL.Value.Minute.ToString());
                item.SIGNINWEEK = ((DateTime)item.SIGNINALL).ToString("dddd", new System.Globalization.CultureInfo("zh-CN"));
            }

            return list;
        }

        /// <summary>
        /// 根据签到标识获取签到信息
        /// </summary>
        /// <returns></returns>
        public static UserSignInModel GetSignInInfoByUserId(decimal SGID)
        {
            Entities db = new Entities();
            List<UserSignInModel> list = new List<UserSignInModel>();
            DateTime nowdate = (DateTime)db.QWGL_USERSIGNINS.FirstOrDefault(t => t.SIGNINID == SGID).SIGNINTIME;

            list = (from us in db.QWGL_USERSIGNINS

                    join u in db.SYS_USERS
                    on us.USERID equals u.USERID

                    join ust in db.QWGL_USERSIGNINTASKS
                    on new { id = u.USERID, id1 = nowdate.Year, id2 = nowdate.Month, id3 = nowdate.Day } equals new { id = (decimal)ust.USERID, id1 = ust.SIGNINDAY.Value.Year, id2 = ust.SIGNINDAY.Value.Month, id3 = ust.SIGNINDAY.Value.Day }

                    join ut in db.SYS_UNITS
                    on u.UNITID equals ut.UNITID

                    join sa in db.QWGL_SIGNINAREAS
                    on ust.AREAID equals sa.AREAID

                    where us.SIGNINID == SGID
                    select new UserSignInModel
                    {
                        USERID = (decimal)u.USERID,
                        SGID = us.SIGNINID,
                        USERNAME = u.USERNAME,
                        UNITID = (decimal)u.UNITID,
                        UNITNAME = ut.UNITNAME,
                        USERAVATAR = u.AVATAR,
                        SIGNINALL = ust.SIGNINDAY,
                        PLANSIGNINTIME = sa.SDATE,
                        PLANSIGNOUTTIME = sa.EDATE,
                        ACSIGNINTIME = ust.SIGNINTIME,
                        ACSIGNOUTTIME = ust.SIGNOUTTIME,
                        AREANAME = sa.AREANAME
                    }).ToList();
            return list.FirstOrDefault(t => t.SGID == SGID);
        }

        /// <summary>
        /// 根据签到任务标识获取签到信息
        /// </summary>
        /// <returns></returns>
        public static UserSignInModel GetSignInInfoByTaskId(decimal SGID)
        {
            Entities db = new Entities();
            List<UserSignInModel> list = new List<UserSignInModel>();
            DateTime nowdate = DateTime.Now;

            list = (from ust in db.QWGL_USERSIGNINTASKS

                    join u in db.SYS_USERS
                    on ust.USERID equals u.USERID

                    join ut in db.SYS_UNITS
                    on u.UNITID equals ut.UNITID

                    join sa in db.QWGL_SIGNINAREAS
                    on ust.AREAID equals sa.AREAID

                    where ust.SGID == SGID && ust.SIGNINDAY.Value.Year == nowdate.Year && ust.SIGNINDAY.Value.Month == nowdate.Month && ust.SIGNINDAY.Value.Day == nowdate.Day
                    select new UserSignInModel
                    {
                        USERID = (decimal)u.USERID,
                        SGID = ust.SGID,
                        USERNAME = u.USERNAME,
                        UNITID = (decimal)u.UNITID,
                        UNITNAME = ut.UNITNAME,
                        USERAVATAR = u.AVATAR,
                        SIGNINALL = ust.SIGNINDAY,
                        PLANSIGNINTIME = sa.SDATE,
                        PLANSIGNOUTTIME = sa.EDATE,
                        ACSIGNINTIME = ust.SIGNINTIME,
                        ACSIGNOUTTIME = ust.SIGNOUTTIME,
                        AREANAME = sa.AREANAME
                    }).ToList();
            return list.FirstOrDefault(t => t.SGID == SGID);
        }

        /// <summary>
        /// 用户今日签到次数
        /// </summary>
        /// <returns></returns>
        public static int GetUserSignInCount(decimal UserId)
        {
            Entities db = new Entities();
            DateTime nowdate = DateTime.Now;
            List<QWGL_USERSIGNINS> list = db.QWGL_USERSIGNINS.Where(t => t.USERID == UserId && t.SIGNINTIME.Value.Year == nowdate.Year && t.SIGNINTIME.Value.Month == nowdate.Month && t.SIGNINTIME.Value.Day == nowdate.Day).ToList();
            return list.Count;
        }

        /// <summary>
        /// 获取一段时间内队员签到总次数
        /// </summary>
        /// <returns></returns>
        public static List<QWGL_USERSIGNINS> GetAllSignInList(decimal userid, DateTime starttime, DateTime endtime)
        {
            Entities db = new Entities();
            List<QWGL_USERSIGNINS> list = db.QWGL_USERSIGNINS.Where(t => t.USERID == userid &&
                t.SIGNINTIME >= starttime && t.SIGNINTIME <= endtime).ToList();
            return list;
        }



        ///////////////////pc
        /// <summary>
        ///获取条件查询的队员签到列表
        /// </summary>
        /// <returns></returns>
        public static List<VMUserSignIn> GetSignInSearchList(string userbh, string username, string starttime, string endtime, string status)
        {
            Entities db = new Entities();
            var list = (from us in db.QWGL_USERSIGNINS
                        from u in db.SYS_USERS
                        where us.USERID == u.USERID
                        select new VMUserSignIn
                        {
                            ZFZBH = u.ZFZBH,
                            UserName = u.USERNAME,
                            SignInDate = us.SIGNINTIME
                        }).ToList();

            if (!string.IsNullOrEmpty(userbh))
                list = list.Where(t => t.ZFZBH.Contains(userbh)).ToList();
            if (!string.IsNullOrEmpty(username))
                list = list.Where(t => t.UserName.Contains(username)).ToList();
            if (!string.IsNullOrEmpty(starttime))
                list = list.Where(t => t.SignInDate >= DateTime.Parse(starttime)).ToList();
            if (!string.IsNullOrEmpty(endtime))
                list = list.Where(t => t.SignInDate <= DateTime.Parse(endtime).AddDays(1)).ToList();

            return list;
        }

        ///////////////////pc
        /// <summary>
        ///获取条件查询的队员签到列表(周海飞)
        /// </summary>
        /// <returns></returns>
        public static List<VMUserSignIn> GetSignSearchList(string userbh, string username, string starttime, string endtime, string status)
        {
            Entities db = new Entities();
            var list = (from ust in db.QWGL_USERSIGNINTASKS
                        from usa in db.QWGL_SIGNINAREAS
                        from u in db.SYS_USERS
                        from uni in db.SYS_UNITS
                        where ust.AREAID == usa.AREAID
                        && ust.USERID == u.USERID
                        && u.UNITID == uni.UNITID
                        && usa.STATE == 1
                        && u.STATUSID == 1
                        && uni.STATUSID == 1 && uni.PARENTID == 17
                        select new VMUserSignIn
                        {
                            ZFZBH = u.ZFZBH,
                            UserName = u.USERNAME,
                            SignSTime = usa.SDATE,
                            SignETime = usa.EDATE,
                            SigninSTime = ust.SIGNINTIME,
                            SigninETime = ust.SIGNOUTTIME,
                            SignInDate = ust.SIGNINDAY
                        }).ToList();

            if (!string.IsNullOrEmpty(userbh))
                list = list.Where(t => t.ZFZBH.Contains(userbh)).ToList();
            if (!string.IsNullOrEmpty(username))
                list = list.Where(t => t.UserName.Contains(username)).ToList();
            if (!string.IsNullOrEmpty(starttime))
                list = list.Where(t => t.SignInDate.Value.Date >= DateTime.Parse(starttime).Date).ToList();
            if (!string.IsNullOrEmpty(endtime))
                list = list.Where(t => t.SignInDate.Value.Date <= DateTime.Parse(endtime).Date).ToList();

            return list;
        }

        /// <summary>
        /// 获取一段时间内队员签到总次数
        /// </summary>
        /// <returns></returns>
        public static PJKH_EXAMINES_User GetSignInCountList(decimal userid, DateTime starttime, DateTime endtime)
        {
            Entities db = new Entities();
            PJKH_EXAMINES_User countmodel = new PJKH_EXAMINES_User();
            int ResultSMinute = 0;
            int ResultEMinute = 0;
            var list = (from ust in db.QWGL_USERSIGNINTASKS
                        from usa in db.QWGL_SIGNINAREAS
                        from u in db.SYS_USERS

                        where ust.AREAID == usa.AREAID
                        && ust.USERID == u.USERID
                        && usa.STATE == 1
                        select new VMUserSignIn
                        {
                            UserId = u.USERID,
                            SignSTime = usa.SDATE,
                            SignETime = usa.EDATE,
                            SigninSTime = ust.SIGNINTIME,
                            SigninETime = ust.SIGNOUTTIME,
                            SignInDate = ust.SIGNINDAY
                        }).ToList();
            list = list.Where(t => t.UserId == userid && t.SignInDate.Value.Date >= starttime.Date && t.SignInDate.Value.Date <= endtime.Date).ToList();
            foreach (var item in list)
            {
                ResultSMinute = item.SigninSTime == null ? 1 : (item.SigninSTime.Value.Hour * 60 + item.SigninSTime.Value.Minute) - (item.SignSTime.Value.Hour * 60 + item.SignSTime.Value.Minute);
                ResultEMinute = item.SigninETime == null ? -1 : (item.SigninETime.Value.Hour * 60 + item.SigninETime.Value.Minute) - (item.SignETime.Value.Hour * 60 + item.SignETime.Value.Minute);
                if (ResultSMinute <= 0 && ResultEMinute >= 0)
                    countmodel.SIGNINCOUNT++;
                else
                    countmodel.UNSIGNINCOUNT++;
            }

            return countmodel;
        }

        /// <summary>
        /// 获取一段时间内队员签到情况
        /// </summary>
        /// <returns></returns>
        public static int GetSignInTJInfoList(decimal UserId, string STime, string ETime, decimal Type)
        {
            Entities db = new Entities();
            int count = 0;
            int uncount = 0;
            int ResultSMinute = 0;
            int ResultEMinute = 0;
            List<PJKH_EXAMINES_User> list = (from ust in db.QWGL_USERSIGNINTASKS
                                             from usa in db.QWGL_SIGNINAREAS
                                             from u in db.SYS_USERS

                                             where ust.AREAID == usa.AREAID
                                             && ust.USERID == u.USERID
                                             && usa.STATE == 1 && ust.USERID == UserId
                                             orderby ust.SIGNINDAY descending
                                             select new PJKH_EXAMINES_User
                                             {
                                                 USERNAME = u.USERNAME,
                                                 SignSTime = usa.SDATE,
                                                 SignETime = usa.EDATE,
                                                 SigninSTime = ust.SIGNINTIME,
                                                 SigninETime = ust.SIGNOUTTIME,
                                                 SignInDate = ust.SIGNINDAY
                                             }).ToList();
            if (!string.IsNullOrEmpty(STime))
                list = list.Where(t => t.SignInDate >= DateTime.Parse(STime)).ToList();
            if (!string.IsNullOrEmpty(ETime))
                list = list.Where(t => t.SignInDate <= DateTime.Parse(ETime)).ToList();
            foreach (var item in list)
            {
                ResultSMinute = item.SigninSTime == null ? 1 : (item.SigninSTime.Value.Hour * 60 + item.SigninSTime.Value.Minute) - (item.SignSTime.Value.Hour * 60 + item.SignSTime.Value.Minute);
                ResultEMinute = item.SigninETime == null ? -1 : (item.SigninETime.Value.Hour * 60 + item.SigninETime.Value.Minute) - (item.SignETime.Value.Hour * 60 + item.SignETime.Value.Minute);
                if (ResultSMinute <= 0 && ResultEMinute >= 0)
                    count++;
                else
                    uncount++;
            }
            if (Type == 1)
                return count;
            else
                return uncount;
        }

    }
}
