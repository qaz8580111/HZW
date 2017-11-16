using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.Common.Enums;
using ZGM.BLL.UserBLLs;
using ZGM.Model.PhoneModel;
using ZGM.Model.CoordinationManager;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;
using ZGM.BLL.QWGLBLLs;

namespace ZGM.BLL.PJKHBLLs
{
    public class ExamineBLL
    {
        /// <summary>
        /// PC获取需要考核的队员
        /// </summary>
        /// <returns></returns>
        public static List<SYS_USERS> GetComExaminedUser(decimal? UserId)
        {
            Entities db = new Entities();
            decimal? UnitId = db.SYS_USERS.FirstOrDefault(t => t.USERID == UserId).UNITID;
            List<SYS_USERS> users = db.SYS_USERS.Where(t => t.STATUSID == (decimal)StatusEnum.Normal && t.UNITID == UnitId && t.USERID != UserId).OrderBy(t => t.SEQNO).ToList();

            return users;
        }

        /// <summary>
        /// PC获取需要考核的分队
        /// </summary>
        /// <returns></returns>
        public static List<SYS_UNITS> GetExamineUnit()
        {
            Entities db = new Entities();
            List<SYS_UNITS> units = db.SYS_UNITS.Where(t => t.STATUSID == (decimal)StatusEnum.Normal && t.PARENTID == 17 && t.UNITTYPEID == 4).OrderBy(t => t.SEQNUM).ToList();
            return units;
        }

        /// <summary>
        /// 获取需要考核的队员
        /// </summary>
        /// <returns></returns>
        public static List<UserExamineInfo> GetExaminedUser(decimal unitid,decimal userid)
        {
            Entities db = new Entities();
            List<UserExamineInfo> users = (from user in db.SYS_USERS
                                           join u in db.SYS_UNITS
                                           on user.UNITID equals u.UNITID
                                           where user.STATUSID == (decimal)StatusEnum.Normal && u.UNITID == unitid && user.USERID != userid
                                            orderby user.SEQNO
                                           select new UserExamineInfo
                                           {
                                               UserId = user.USERID,
                                               UserName = user.USERNAME
                                           }).ToList();
            return users;
        }

        /// <summary>
        ///获取条件查询的评价考核列表
        /// </summary>
        /// <returns></returns>
        public static List<VMPJKH> GetSearchExaminesList(string starttime, string endtime, string username)
        {
            Entities db = new Entities();

            IQueryable<VMPJKH> list = (from pe in db.PJKH_EXAMINES
                                       join u in db.SYS_USERS
                                       on pe.USERID equals u.USERID
                                       select new VMPJKH
                                       {
                                           EXAMINETIME = pe.EXAMINETIME,
                                           UserName = u.USERNAME,
                                           JOB = pe.JOB,
                                           SIGNIN = pe.SIGNIN,
                                           ALARM = pe.ALARM,
                                           SCORE = pe.SCORE
                                       });
            if (!string.IsNullOrEmpty(starttime))
            {
                DateTime startTime = DateTime.Parse(starttime).Date;
                list = list.Where(t => t.EXAMINETIME >= startTime);
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                DateTime endTime = DateTime.Parse(endtime).Date.AddDays(1);
                list = list.Where(t => t.EXAMINETIME < endTime);
            }
            if (!string.IsNullOrEmpty(username))
                list = list.Where(t => t.UserName.Contains(username));

            return list.ToList();
        }

        /// <summary>
        ///获取条件查询的考核详情列表
        /// </summary>
        /// <returns></returns>
        public static List<EXAMINESLIST_INFO> GetExamineList(string UnitId, string STime, string ETime, string UserName)
        {
            Entities db = new Entities();
            List<EXAMINESLIST_INFO> list = new List<EXAMINESLIST_INFO>();
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            var ulist = (from u in db.SYS_USERS
                        join un in db.SYS_UNITS
                        on u.UNITID equals un.UNITID
                        join ur in db.SYS_USERROLES
                        on u.USERID equals ur.USERID
                        where un.PARENTID == 17 && un.UNITTYPEID == 4 && u.STATUSID == 1 && ur.ROLEID == 6
                        select new
                        {
                            USERID = u.USERID,
                            UNITID = u.UNITID,
                            UNITNAME = un.UNITNAME,
                            USERNAME = u.USERNAME,
                            QueryName = u.USERNAME.ToUpper()
                        }).ToList();
            
            if (!string.IsNullOrEmpty(UnitId))
                ulist = ulist.Where(t => t.UNITID == decimal.Parse(UnitId)).ToList();
            if (!string.IsNullOrEmpty(UserName))
                ulist = ulist.Where(t => t.QueryName.Contains(UserName.ToUpper())).ToList();

            for (int i = 0; i < ulist.Count; i++)
            {
                EXAMINESLIST_INFO model = new EXAMINESLIST_INFO();
                model.UserName = ulist[i].USERNAME;
                model.UnitName = ulist[i].UNITNAME;
                model.EventReport = (int)ZFSJSOURCESBLL.GetReportEventList(ulist[i].USERID, STime, ETime).PCount;
                model.EventFinish = (int)ZFSJSOURCESBLL.GetReportEventList(ulist[i].USERID, STime, ETime).PCCount;
                model.FinishPercent = model.EventReport == 0 ? "0.0%" : ((double)model.EventFinish / (double)model.EventReport).ToString("0.0%");
                if (string.IsNullOrEmpty(STime) && !string.IsNullOrEmpty(ETime))
                    model.EventOverTime = WF.GetAllEvent(ulist[i].USERID).Where(t => t.userid == ulist[i].USERID && t.ISOVERDUE == 1 && t.status == 1 && t.createtime <= DateTime.Parse(ETime)).Count();
                if (!string.IsNullOrEmpty(STime) && string.IsNullOrEmpty(ETime))
                    model.EventOverTime = WF.GetAllEvent(ulist[i].USERID).Where(t => t.userid == ulist[i].USERID && t.ISOVERDUE == 1 && t.status == 1 && t.createtime >= DateTime.Parse(STime)).Count();
                if (string.IsNullOrEmpty(STime) && string.IsNullOrEmpty(ETime))
                    model.EventOverTime = WF.GetAllEvent(ulist[i].USERID).Where(t => t.userid == ulist[i].USERID && t.ISOVERDUE == 1 && t.status == 1).Count();
                else
                    model.EventOverTime = WF.GetAllEvent(ulist[i].USERID).Where(t => t.userid == ulist[i].USERID && t.ISOVERDUE == 1 && t.status == 1 && t.createtime >= DateTime.Parse(STime) && t.createtime <= DateTime.Parse(ETime)).Count();
                model.Distance = ZFSJSOURCESBLL.GetPersonWalk(ulist[i].USERID, STime, ETime);
                model.SignIn = UserSignInBLL.GetSignInTJInfoList(ulist[i].USERID, STime, ETime, 1);
                model.UnSignIn = UserSignInBLL.GetSignInTJInfoList(ulist[i].USERID, STime, ETime, 2);
                model.SignPercent = (model.SignIn + model.UnSignIn) == 0 ? "0.0%" : ((double)model.SignIn / (double)(model.SignIn + model.UnSignIn)).ToString("0.0%");
                model.OverTime = AlarmBLL.GetAlarmInTimeList(ulist[i].USERID, STime, ETime, 1);
                model.OverBorder = AlarmBLL.GetAlarmInTimeList(ulist[i].USERID, STime, ETime, 2);
                model.OverLine = AlarmBLL.GetAlarmInTimeList(ulist[i].USERID, STime, ETime, 3);
                list.Add(model);
            }

            return list;
        }
        public static List<EXAMINESLIST_INFO> GetExamineListBySQL(string UnitId, string STime, string ETime, string UserName)
        {
            Entities db = new Entities();
            #region sql语句
            string sql = string.Format(@"select user_1.userid,user_1.username,user_1.unitname,user_1.unitid,pe_1.EventReport,pe_1.EventFinish,
            (case when pe_1.EventReport=0 then '0%' else (round(pe_1.EventFinish/pe_1.EventReport*100,1))||'%' end) as FinishPercent
            ,wfsource_1.EventOverTime,pw_1.Distance,qwgl_1.signintotal,qwgl_1.SignIn,qwgl_1.UnSignIn,
            (case when qwgl_1.signintotal=0 then '0%' else (round(qwgl_1.SignIn/qwgl_1.signintotal*100,1))||'%' end) as SignPercent,
            qwbj_1.OverTime,qwbj_1.OverBorder,qwbj_1.OverLine from 

            (select users.userid,users.username,users.unitid,unit.unitname from sys_users users
            left join sys_units unit on users.unitid = unit.unitid
            left join sys_userroles ur on ur.userid = users.userid
            where unit.parentid = 17 and ur.roleid = 6) user_1

            left join 
                (select user_1.userid as peuserid,sum(tjph.PATROLRCOUNT) as EventReport,sum(tjph.PATROLCLOSEDCOUNT) as EventFinish from
                (select users.userid,users.username from sys_users users
                left join sys_units unit on users.unitid = unit.unitid
                left join sys_userroles ur on ur.userid = users.userid
                where unit.parentid = 17 and ur.roleid = 6) user_1
                left join tj_personevent_history tjph on user_1.userid = tjph.personid
                where to_char(tjph.STATTIME,'yyyy-mm-dd')>='{0}' and to_char(tjph.STATTIME,'yyyy-mm-dd')<='{1}'
                group by user_1.userid) pe_1 on pe_1.peuserid = user_1.userid

            left join 
                (select user_1.userid as wfuserid,count(wfsource.userid) as EventOverTime from 
                (select users.userid,users.username from sys_users users
                left join sys_units unit on users.unitid = unit.unitid
                left join sys_userroles ur on ur.userid = users.userid
                where unit.parentid = 17 and ur.roleid = 6) user_1
                left join (select wfs.wfsid,wfs.wfsname,zfsjs.CREATETTIME as createtime,wfs.status,wf.wfid,wf.wfname,u.userid,u.username,wfsa.wfdid,wfd.wfdname,
                wfsa.wfsaid,zfsjs.EVENTTITLE,zfsjs.SOURCEID,zfsjs.ISOVERDUE,xz.SOURCENAME,zfsjs.LEVELNUM,zfsjs.ZFSJID,zfsjs.EVENTCODE
                 from WF_WORKFLOWSPECIFICS wfs
                 inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid and         
                 wfs.CURRENTWFSAID=wfsa.wfsaid
                 inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid
                 inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid
                 inner join XTGL_ZFSJS zfsjs on wfs.TABLENAMEID=zfsjs.zfsjid
                 inner join sys_users u on u.userid=wfs.createuserid
                 inner join XTGL_ZFSJSOURCES xz on zfsjs.SOURCEID=xz.SOURCEID
                 where wfs.wfsid in (
                   select wfsid from WF_WORKFLOWSPECIFICACTIVITYS where wfsaid in (
                     select wfsaid from WF_WORKFLOWSPECIFICUSERS where  status<>3
                   )
                 ) and zfsjs.ISOVERDUE = 1 and wfs.status = 1 and to_char(createtime,'yyyy-mm-dd')>='{0}' and to_char(createtime,'yyyy-mm-dd')<='{1}') wfsource on wfsource.userid = user_1.userid
                 group by user_1.userid) wfsource_1 on user_1.userid = wfsource_1.wfuserid
   
            left join 
                (select user_1.userid as pwuserid,sum(pwh.walksum) as Distance from
                (select users.userid,users.username from sys_users users
                left join sys_units unit on users.unitid = unit.unitid
                left join sys_userroles ur on ur.userid = users.userid
                where unit.parentid = 17 and ur.roleid = 6) user_1
                left join tj_personwalk_history pwh on pwh.personid = user_1.userid
                where to_char(pwh.STATTIME,'yyyy-mm-dd')>='{0}' and to_char(pwh.STATTIME,'yyyy-mm-dd')<='{1}'
                group by user_1.userid) pw_1 on pw_1.pwuserid = user_1.userid
 
            left join
                (select user_1.userid,nvl(signtotal,0) signintotal,nvl(signcount,0) SignIn,(nvl(signtotal,0) - nvl(signcount,0)) as UnSignIn
                from (select users.userid,users.username from sys_users users
                left join sys_units unit on users.unitid = unit.unitid
                left join sys_userroles ur on ur.userid = users.userid
                where unit.parentid = 17 and ur.roleid = 6) user_1
                left join 
                (select qwst.USERID,count(qwst.USERID) as signtotal from qwgl_usersignintasks qwst
                  left join qwgl_signinareas qwsa on qwsa.areaid = qwst.areaid
                  where to_char(qwst.SIGNINDAY,'yyyy-mm-dd')>='{0}' and to_char(qwst.SIGNINDAY,'yyyy-mm-dd')<='{1}'
                  group by qwst.USERID) qws1 on qws1.USERID = user_1.userid
                left join 
                  (select qwst.USERID,count(qwst.USERID) as signcount from qwgl_usersignintasks qwst
                  left join qwgl_signinareas qwsa on qwsa.areaid = qwst.areaid
                  where to_char(qwst.SIGNINTIME,'hh24:mi:ss') <= to_char(qwsa.SDATE,'hh24:mi:ss') and
                  to_char(qwst.SIGNOUTTIME,'hh24:mi:ss') >= to_char(qwsa.EDATE,'hh24:mi:ss') and to_char(qwst.SIGNINDAY,'yyyy-mm-dd')>='{0}' and to_char(qwst.SIGNINDAY,'yyyy-mm-dd')<='{1}'
                  group by qwst.USERID) qws on qws.USERID = user_1.userid) qwgl_1 on qwgl_1.userid = user_1.userid
  
            left join 
                  (select user_1.userid,nvl(overtime_count,0) as OverTime,nvl(overboard_count,0) as OverBorder,nvl(overline_count,0) as OverLine from
                  (select users.userid,users.username from sys_users users
                  left join sys_units unit on users.unitid = unit.unitid
                  left join sys_userroles ur on ur.userid = users.userid
                  where unit.parentid = 17 and ur.roleid = 6) user_1
  
                  left join (select user_1.userid,count(qwad.userid) as overtime_count from
                  (select users.userid,users.username from sys_users users
                  left join sys_units unit on users.unitid = unit.unitid
                  left join sys_userroles ur on ur.userid = users.userid
                  where unit.parentid = 17 and ur.roleid = 6) user_1
                  left join QWGL_ALARMMEMORYLOCATIONDATA qwad on qwad.userid = user_1.userid
                  where qwad.ALARMTYPE=1 and qwad.STATE=1 and to_char(qwad.CREATETIME,'yyyy-mm-dd')>='{0}' and to_char(qwad.CREATETIME,'yyyy-mm-dd')<='{1}'
                  group by user_1.userid) qwbj_2 on qwbj_2.userid = user_1.userid
  
                  left join(select user_1.userid,count(qwad.userid) as overboard_count from
                  (select users.userid,users.username from sys_users users
                  left join sys_units unit on users.unitid = unit.unitid
                  left join sys_userroles ur on ur.userid = users.userid
                  where unit.parentid = 17 and ur.roleid = 6) user_1
                  left join QWGL_ALARMMEMORYLOCATIONDATA qwad on qwad.userid = user_1.userid
                  where qwad.ALARMTYPE=2 and qwad.STATE=1 and to_char(qwad.CREATETIME,'yyyy-mm-dd')>='{0}' and to_char(qwad.CREATETIME,'yyyy-mm-dd')<='{1}'
                  group by user_1.userid) qwbj_3 on qwbj_3.userid = user_1.userid
  
                  left join(select user_1.userid,count(qwad.userid) as overline_count from
                  (select users.userid,users.username from sys_users users
                  left join sys_units unit on users.unitid = unit.unitid
                  left join sys_userroles ur on ur.userid = users.userid
                  where unit.parentid = 17 and ur.roleid = 6) user_1
                  left join QWGL_ALARMMEMORYLOCATIONDATA qwad on qwad.userid = user_1.userid
                  where qwad.ALARMTYPE=3 and qwad.STATE=1 and to_char(qwad.CREATETIME,'yyyy-mm-dd')>='{0}' and to_char(qwad.CREATETIME,'yyyy-mm-dd')<='{1}'
                  group by user_1.userid) qwbj_4 on qwbj_4.userid = user_1.userid) qwbj_1 on qwbj_1.userid = user_1.userid ", STime, ETime);
            #endregion
            IEnumerable<EXAMINESLIST_INFO> list = db.Database.SqlQuery<EXAMINESLIST_INFO>(sql);
            if (!string.IsNullOrEmpty(UnitId))
                list = list.Where(t => t.UnitId == decimal.Parse(UnitId));
            if (!string.IsNullOrEmpty(UserName))
                list = list.Where(t => t.UserName.Contains(UserName.ToUpper()));
            return list.ToList();
        }

        /// <summary>
        /// 添加评价
        /// </summary>
        /// <returns></returns>
        public static int AddExamine(PJKH_EXAMINES model)
        {
            Entities db = new Entities();
            db.PJKH_EXAMINES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 查看用户的考核
        /// </summary>
        /// <returns></returns>
        public static List<UserExamineInfo> GetUserExamineList(decimal userid, decimal roleid, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserExamineInfo> list = new List<UserExamineInfo>();
            if (roleid == 6)
            {
                list = (from ue in db.PJKH_EXAMINES
                        join user in db.SYS_USERS
                        on ue.USERID equals user.USERID
                        where ue.USERID == userid
                        orderby ue.CREATETIME descending
                        select new UserExamineInfo
                        {
                            ExamineId = ue.EXAMINEID,
                            UserName = user.USERNAME,
                            UserAvatar = user.AVATAR,
                            AllDate = ue.EXAMINETIME,
                            QueryName = user.USERNAME.ToUpper()
                        }).ToList();
            }
            else
            {
                list = (from ue in db.PJKH_EXAMINES
                        join user in db.SYS_USERS
                        on ue.USERID equals user.USERID
                        where ue.CREATEUSER == userid
                        orderby ue.CREATETIME descending
                        select new UserExamineInfo
                        {
                            ExamineId = ue.EXAMINEID,
                            UserName = user.USERNAME,
                            UserAvatar = user.AVATAR,
                            AllDate = ue.EXAMINETIME,
                            QueryName = user.USERNAME.ToUpper()
                        }).ToList();
            }
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            list = list.Skip(PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.ExamineDate = ((DateTime)item.AllDate).ToLongDateString();
            }

            return list;
        }

        /// <summary>
        /// 查看部门的考核
        /// </summary>
        /// <returns></returns>
        public static List<UserExamineInfo> GetTeamExamineList(decimal UserId,decimal unitid, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserExamineInfo> list = (from ue in db.PJKH_EXAMINES
                                          join user in db.SYS_USERS
                                          on ue.USERID equals user.USERID
                                        where user.UNITID == unitid && ue.CREATEUSER != UserId
                                        orderby ue.CREATETIME descending
                                        select new UserExamineInfo
                                        {
                                            ExamineId = ue.EXAMINEID,
                                            UserName = user.USERNAME,
                                            UserAvatar = user.AVATAR,
                                            AllDate = ue.EXAMINETIME,
                                            JobScore = ue.JOB,
                                            SignInScore = ue.SIGNIN,
                                            AlarmScore = ue.ALARM,
                                            Score = ue.SCORE,
                                            QueryName = user.USERNAME.ToUpper()
                                        }).ToList();
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            list = list.Skip(PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.ExamineDate = ((DateTime)item.AllDate).ToLongDateString();
            }

            return list;
        }
        
        /// <summary>
        /// 根据考核标识获取考核信息
        /// </summary>
        /// <returns></returns>
        public static UserExamineInfo GetExamineInfoByExamineId(decimal ExamineDataId)
        {
            Entities db = new Entities();
            List<UserExamineInfo> list = (from e in db.PJKH_EXAMINES
                                     join us in db.SYS_USERS
                                    on e.USERID equals us.USERID
                                     join un in db.SYS_UNITS
                                    on us.UNITID equals un.UNITID
                                    where e.EXAMINEID == ExamineDataId
                                    select new UserExamineInfo
                                    {
                                        ExamineId = e.EXAMINEID,
                                        UserName = us.USERNAME,
                                        UnitName = un.UNITNAME,
                                        UserAvatar = us.AVATAR,
                                        AllDate = e.EXAMINETIME,
                                        JobScore = e.JOB,
                                        SignInScore = e.SIGNIN,
                                        AlarmScore = e.ALARM,
                                        Score = e.SCORE,
                                        CreateUser = (decimal)e.CREATEUSER
                                    }).ToList();
            return list.FirstOrDefault(t => t.ExamineId == ExamineDataId);
        }
    }
}
