using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class UserDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 用户登入验证
        /// 反馈信息为：登入成功；密码错误；用户名错误
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>反馈信息</returns>
        public string UserLogin(string account, string password)
        {
            IQueryable<UserModel> users = db.SYS_USERS
                .Where(t => t.ACCOUNT == account)
                .Select(t => new UserModel
                {
                    UserId = t.USERID,
                    Account = t.ACCOUNT,
                    Password = t.PASSWORD
                });
            if (users.Count() > 0)
            {
                UserModel user = users.Where(t => t.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return "登入成功";
                }
                else
                {
                    return "密码错误";
                }
            }
            else
            {
                return "用户名错误";
            }
        }

        /// <summary>
        /// 用户登入验证
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>反馈信息</returns>
        public UserModel UserLogin2(string account, string password)
        {
            IQueryable<SYS_ROLES> roles = from u in db.SYS_USERS
                                          from t in db.SYS_USERROLES
                                          from r in db.SYS_ROLES
                                          where u.ACCOUNT == account
                                          && t.USERID == u.USERID
                                          && r.ROLEID == t.ROLEID
                                          select r;
            string roleNames = "";
            foreach (SYS_ROLES item in roles)
            {
                roleNames += item.ROLENAME + ",";
            }
            IQueryable<UserModel> users = db.SYS_USERS
                .Where(t => t.ACCOUNT == account)
                .Select(t => new UserModel
                {
                    UserId = t.USERID,
                    Account = t.ACCOUNT,
                    Password = t.PASSWORD,
                    RoleName = roleNames
                });
            if (users.Count() > 0)
            {
                UserModel user = users.Where(t => t.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<UserModel> GetUsersByPage(string userName, decimal? unitId, decimal? skipNum, decimal? takeNum)
        {
            IQueryable<SYS_USERS> us = db.SYS_USERS.Where(t => t.STATUSID == 1 && t.SYS_UNITS.PARENTID == 17);
            if (!string.IsNullOrEmpty(userName))
                us = us.Where(t => t.USERNAME.Contains(userName));
            if (unitId != null)
                us = us.Where(t => t.UNITID == unitId);
            us = us.OrderByDescending(t => t.ISONLINE);
            if (skipNum != null && takeNum != null)
                us = us.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            List<UserModel> result = (from t in us
                                      join up in db.QWGL_USERLATESTPOSITIONS
                                      on t.USERID equals up.USERID
                                      into temp
                                      from up in temp.DefaultIfEmpty()
                                      select new UserModel
                                        {
                                            UserId = t.USERID,
                                            UserName = t.USERNAME,
                                            Unitid = t.UNITID,
                                            UnitName = t.SYS_UNITS.UNITNAME,
                                            Account = t.ACCOUNT,
                                            Password = t.PASSWORD,
                                            UserPositionid = t.USERPOSITIONID,
                                            UserPositionName = t.SYS_USERPOSITIONS.USERPOSITIONNAME,
                                            Statusid = t.STATUSID,
                                            Phone = t.PHONE,
                                            SPhone = t.SPHONE,
                                            ZFZBH = t.ZFZBH,
                                            PhoneIMEI = t.PHONEIMEI,
                                            Avatar = t.AVATAR,
                                            SEQNO = t.SEQNO,
                                            Sex = t.SEX,
                                            Birthday = t.BIRTHDAY,
                                            SLAvatar = t.SLAVATAR,
                                            SmallAvatar = t.SMALLAVATAR,
                                            PositionTime = up.POSITIONTIME,
                                            LastLoginTime = up.LASTLOGINTIME,
                                            IsOnline = t.ISONLINE,
                                            IsOnlineName = t.ISONLINE == 0 ? "离线" : "在线",
                                            IsAlarm = t.ISPOLICE,
                                            IsAlarmName = t.ISPOLICE == 0 ? "正常" : "报警",
                                            isMessage = t.ISMESSAGE,
                                            X84 = up.X84,
                                            Y84 = up.Y84,
                                            X2000 = up.X2000,
                                            Y2000 = up.Y2000
                                        }).ToList();
            return result;
        }

        /// <summary>
        ///  获取用户数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUsersCount(string userName, decimal? unitId)
        {
            IQueryable<SYS_USERS> us = db.SYS_USERS.Where(t => t.STATUSID == 1 && t.SYS_UNITS.PARENTID == 17);
            if (!string.IsNullOrEmpty(userName))
                us = us.Where(t => t.USERNAME.Contains(userName));
            if (unitId != null)
                us = us.Where(t => t.UNITID == unitId);

            return us.Count();
        }

        /// <summary>
        /// 根据用户标识获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserModel GetUserByUserId(decimal userId)
        {
            IQueryable<UserModel> result = from t in db.SYS_USERS
                                           join up in db.QWGL_USERLATESTPOSITIONS
                                           on t.USERID equals up.USERID
                                           into temp
                                           from up in temp.DefaultIfEmpty()
                                           where t.USERID == userId
                                           select new UserModel
                                           {
                                               UserId = t.USERID,
                                               UserName = t.USERNAME,
                                               Unitid = t.UNITID,
                                               UnitName = t.SYS_UNITS.UNITNAME,
                                               Account = t.ACCOUNT,
                                               Password = t.PASSWORD,
                                               UserPositionid = t.USERPOSITIONID,
                                               UserPositionName = t.SYS_USERPOSITIONS.USERPOSITIONNAME,
                                               Statusid = t.STATUSID,
                                               Phone = t.PHONE,
                                               SPhone = t.SPHONE,
                                               ZFZBH = t.ZFZBH,
                                               PhoneIMEI = t.PHONEIMEI,
                                               Avatar = t.AVATAR,
                                               SEQNO = t.SEQNO,
                                               Sex = t.SEX,
                                               Birthday = t.BIRTHDAY,
                                               SLAvatar = t.SLAVATAR,
                                               SmallAvatar = t.SMALLAVATAR,
                                               PositionTime = up.POSITIONTIME,
                                               LastLoginTime = up.LASTLOGINTIME,
                                               X84 = up.X84,
                                               Y84 = up.Y84,
                                               X2000 = up.X2000,
                                               Y2000 = up.Y2000,
                                               IsOnline = t.ISONLINE,
                                               IsOnlineName = t.ISONLINE == 0 ? "离线" : "在线",
                                               IsAlarm = t.ISPOLICE,
                                               IsAlarmName = t.ISPOLICE == 0 ? "正常" : "报警",
                                               isMessage = t.ISMESSAGE,
                                           };
            return result.SingleOrDefault();
        }

        /// <summary>
        /// 获取人员历史定位列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<UserPositionModel> GetUserPositions(decimal userId, DateTime? startTime, DateTime? endTime)
        {
            IQueryable<UserPositionModel> result = db.QWGL_USERHISTORYPOSITIONS
                .Where(t => t.USERID == userId
                    && t.POSITIONTIME >= startTime
                    && t.POSITIONTIME <= endTime)
                    .Select(t => new UserPositionModel
                    {
                        UPId = t.UPID,
                        UserId = t.USERID,
                        PositionTime = t.POSITIONTIME,
                        IMEICode = t.IMEICODE,
                        X84 = t.X84,
                        Y84 = t.Y84,
                        X2000 = t.X2000,
                        Y2000 = t.Y2000
                    });
            return result.OrderBy(t => t.PositionTime).ToList();
        }

        /// <summary>
        /// 获取在线人员数
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public int GetOnlineCount(decimal? seconds)
        {
            DateTime time = DateTime.Now.AddSeconds(-Convert.ToDouble(seconds));
            int count = (from ulps in db.QWGL_USERLATESTPOSITIONS
                         from t in db.SYS_USERS
                         where ulps.POSITIONTIME >= time
                         && t.USERID == ulps.USERID
                         && t.STATUSID == 1
                         select t).Count();
            return count;
        }

        /// <summary>
        /// 获取部门在线统计数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitOnline(decimal? parentId, decimal? seconds)
        {
            DateTime time = DateTime.Now.AddSeconds(-Convert.ToDouble(seconds));
            IQueryable<SYS_UNITS> units = db.SYS_UNITS
                .Where(t => t.STATUSID == 1
                    && t.PARENTID == parentId);

            //var stat = from t in db.SYS_USERS
            //           from ulps in db.QWGL_USERLATESTPOSITIONS
            //           from u in units
            //           where t.USERID == ulps.USERID
            //           && t.UNITID == u.UNITID
            //           && ulps.POSITIONTIME >= time
            //           && t.STATUSID == 1
            //           group u by u.UNITID
            //               into g
            //               select new
            //               {
            //                   UnitId = g.Key,
            //                   Sum = g.Count()
            //               };

            var stat = from t in db.SYS_USERS
                       from u in units
                       where t.UNITID == u.UNITID
                       && t.ISONLINE == 1
                       && t.STATUSID == 1
                       group u by u.UNITID
                           into g
                           select new
                           {
                               UnitId = g.Key,
                               Sum = g.Count()
                           };

            IQueryable<R_UserUnitModel> result = from u in units
                                                 join st in stat
                                                 on u.UNITID equals st.UnitId
                                                 into temp
                                                 from st in temp.DefaultIfEmpty()
                                                 select new R_UserUnitModel
                                                 {
                                                     UnitId = u.UNITID,
                                                     UnitName = u.UNITNAME,
                                                     Description = u.DESCRIPTION,
                                                     Abbreviation = u.ABBREVIATION,
                                                     Sum = st.Sum == null ? 0 : st.Sum
                                                 };
            return result.ToList();
        }

        /// <summary>
        /// 获取部门人员总数
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitAll(decimal? parentId)
        {
            IQueryable<SYS_UNITS> units = db.SYS_UNITS
                .Where(t => t.STATUSID == 1
                    && t.PARENTID == parentId);
            var stat = from u in units
                       from us in db.SYS_USERS
                       where us.UNITID == u.UNITID
                       && us.STATUSID == 1
                       group u by u.UNITID
                           into g
                           select new
                           {
                               UnitId = g.Key,
                               Sum = g.Count()
                           };

            IQueryable<R_UserUnitModel> result = from u in units
                                                 join st in stat
                                                 on u.UNITID equals st.UnitId
                                                 into temp
                                                 from st in temp.DefaultIfEmpty()
                                                 select new R_UserUnitModel
                                                 {
                                                     UnitId = u.UNITID,
                                                     UnitName = u.UNITNAME,
                                                     Description = u.DESCRIPTION,
                                                     Abbreviation = u.ABBREVIATION,
                                                     All = st.Sum == null ? 0 : st.Sum
                                                 };
            return result.ToList();
        }

        /// <summary>
        /// 获取今日人员在线、离线、报警分段统计
        /// </summary>
        /// <returns></returns>
        public List<R_StatModel> GetUserStatSub(decimal? parentId, DateTime? startTime, DateTime? endTime)
        {
            int allCount = db.SYS_USERS.Where(t => t.SYS_UNITS.PARENTID == parentId&&t.STATUSID==1).Count();
            IQueryable<R_StatModel> statOnline = from st in db.TJ_PERSONONLINE_TODAY
                                                 where st.STATTIME >= startTime
                                                 && st.STATTIME <= endTime
                                                 group st by st.STATTIME
                                                     into g
                                                     select new R_StatModel
                                                     {
                                                         TypeName = "今日在线",
                                                         StatTime = g.Key,
                                                         Sum = g.Sum(t => t.ONLINECOUNT)
                                                     };
            List<R_StatModel> statOffline = new List<R_StatModel>();
            foreach (R_StatModel item in statOnline)
            {
                statOffline.Add(new R_StatModel
                {
                    TypeName = "今日离线",
                    StatTime = item.StatTime,
                    Sum = allCount - item.Sum
                });
            }

            IQueryable<R_StatModel> statAlarm = from t in db.TJ_UNITPERSONPOLICE_TODAY
                                                where t.STATTIME >= startTime
                                                && t.STATTIME <= endTime
                                                group t by t.STATTIME
                                                    into g
                                                    select new R_StatModel
                                                        {
                                                            TypeName = "今日报警",
                                                            StatTime = g.Key,
                                                            Sum = g.Sum(t => t.POLICECOUNT)
                                                        };


            List<R_StatModel> result = new List<R_StatModel>();
            result.AddRange(statOnline.OrderBy(r => r.StatTime));
            result.AddRange(statOffline.OrderBy(r => r.StatTime));
            result.AddRange(statAlarm.OrderBy(r => r.StatTime));
            return result;
        }

        /// <summary>
        /// 根据用户标识获取人员报警信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="takeNum"></param>
        /// <param name="skipNum"></param>
        /// <returns></returns>
        public IQueryable<UserAlarmModel> GetAlarmsByPage(decimal? userId, decimal? skipNum, decimal? takeNum)
        {
            IQueryable<UserAlarmModel> result = from t in db.QWGL_ALARMMEMORYLOCATIONDATA
                                                where t.USERID == userId
                                                select new UserAlarmModel
                                                {
                                                    Id = t.ID,
                                                    X = t.X,
                                                    Y = t.Y,
                                                    GPSTime = t.GPSTIME,
                                                    Speed = t.SPEED,
                                                    StartTime = t.ALARMSTRATTIME,
                                                    EndTime = t.ALARMENDTIME,
                                                    TypeId = t.ALARMTYPE,
                                                    TypeName = t.ALARMTYPE == 1 ? "停留" : t.ALARMTYPE == 2 ? "越界" : "离线",
                                                    UserId = t.USERID,
                                                    State = t.STATE,
                                                    StateName = t.STATE == 0 ? "未处理" : t.STATE == 1 ? "生效" : "作废",
                                                    Content = t.CONTENT,
                                                    DealTime = t.DEALTIME,
                                                    DealUserId = t.DEALUSERID,
                                                    IsAllege = t.ISALLEGE,
                                                    AllegeReason = t.ALLEGEREASON,
                                                    AllegeTime = t.ALLEGETIME
                                                };
            result = result.OrderByDescending(t => t.GPSTime).Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            return result;
        }

        /// <summary>
        /// 获取报警数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetAlarmsCount(decimal? userId)
        {
            IQueryable<QWGL_ALARMMEMORYLOCATIONDATA> result = db.QWGL_ALARMMEMORYLOCATIONDATA
                .Where(t => t.USERID == userId);
            int count = result.Count();
            return count;
        }

        /// <summary>
        /// 获取指定时间之间的人员报警
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IQueryable<UserAlarmModel> GetAlarmsByUserId(decimal? userId, DateTime? startTime, DateTime? endTime)
        {
            IQueryable<UserAlarmModel> result = from t in db.QWGL_ALARMMEMORYLOCATIONDATA
                                                where t.USERID == userId
                                            && t.ALARMSTRATTIME <= endTime && t.ALARMENDTIME >= startTime
                                                select new UserAlarmModel
                                                {
                                                    Id = t.ID,
                                                    X = t.X,
                                                    Y = t.Y,
                                                    GPSTime = t.GPSTIME,
                                                    Speed = t.SPEED,
                                                    StartTime = t.ALARMSTRATTIME,
                                                    EndTime = t.ALARMENDTIME,
                                                    TypeId = t.ALARMTYPE,
                                                    TypeName = t.ALARMTYPE == 1 ? "停留" : t.ALARMTYPE == 2 ? "越界" : "离线",
                                                    UserId = t.USERID,
                                                    State = t.STATE,
                                                    StateName = t.STATE == 0 ? "未处理" : t.STATE == 1 ? "生效" : "作废",
                                                    Content = t.CONTENT,
                                                    DealTime = t.DEALTIME,
                                                    DealUserId = t.DEALUSERID,
                                                    IsAllege = t.ISALLEGE,
                                                    AllegeReason = t.ALLEGEREASON,
                                                    AllegeTime = t.ALLEGETIME
                                                };
            result = result.OrderByDescending(t => t.GPSTime);
            return result;
        }
    }
}
