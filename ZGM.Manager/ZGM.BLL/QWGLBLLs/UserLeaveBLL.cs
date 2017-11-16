using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model.PhoneModel;
using ZGM.Model;
using ZGM.BLL.UserBLLs;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.QWGLBLLs
{
    public class UserLeaveBLL
    {
        /// <summary>
        /// 加载用户请假类型
        /// </summary>
        /// <returns></returns>
        public static List<UserLeaveModel> GetUserLeaveType()
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = (from lt in db.QWGL_LEAVETYPES
                                         orderby lt.LTYPEID
                                         select new UserLeaveModel
                                         {
                                             LeaveType = lt.LTYPEID,
                                             LeaveTypeName = lt.LNAME
                                         }).ToList();
            return list;
        }

        /// <summary>
        /// 获取一个新的请假标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewLEAVEID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_LEAVEID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取该部门的审批人
        /// </summary>
        /// <returns></returns>
        public static List<UserLoginModel> GetUnitExaminer(decimal UnitId)
        {
            Entities db = new Entities();
            List<UserLoginModel> list = (from u in db.SYS_USERS
                                         join ur in db.SYS_USERROLES
                                         on u.USERID equals ur.USERID
                                         where (u.UNITID == UnitId && ur.ROLEID==26) || ur.ROLEID == 8
                                         orderby u.USERID
                                         select new UserLoginModel
                                         {
                                             USERID = u.USERID,
                                             USERNAME = u.USERNAME,
                                             AVATAR = u.AVATAR
                                         }).ToList();
            return list;
        }

        /// <summary>
        /// 新增用户请假
        /// </summary>
        /// <returns></returns>
        public static int AddUserLeave(QWGL_LEAVES model)
        {
            Entities db = new Entities();
            db.QWGL_LEAVES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 新增用户请假审核
        /// </summary>
        /// <returns></returns>
        public static int AddUserLeaveExaminer(QWGL_LEAVEEXAMINES model)
        {
            Entities db = new Entities();
            db.QWGL_LEAVEEXAMINES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 团队今日请假情况
        /// </summary>
        /// <returns></returns>
        public static List<UserLeaveModel> GetTeamLeaveList(decimal UnitId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = new List<UserLeaveModel>();
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToLongDateString());

            list = (from l in db.QWGL_LEAVES

                     join u in db.SYS_USERS
                     on l.USERID equals u.USERID

                     join ut in db.SYS_UNITS
                     on u.UNITID equals ut.UNITID

                     join lt in db.QWGL_LEAVETYPES
                     on l.LTYPEID equals lt.LTYPEID

                     where u.UNITID == UnitId && l.SDATE.Value.Year == nowdate.Year && l.SDATE.Value.Month == nowdate.Month && l.SDATE.Value.Day == nowdate.Day
                     orderby l.CREATETIME descending

                     select new UserLeaveModel
                     {
                         UserId = (decimal)l.USERID,
                         UserName = u.USERNAME,
                         UserAvatar = u.AVATAR,
                         LeaveId = l.LEAVEID,
                         LeaveTypeName = lt.LNAME,
                         SDate =l.SDATE,
                         EDate = l.EDATE,
                         QueryName = u.USERNAME.ToUpper(),
                     }).ToList();

            foreach (var item in list)
            {
                //是否审批通过
                List<QWGL_LEAVEEXAMINES> llist = db.QWGL_LEAVEEXAMINES.Where(t => t.LEAVEID == item.LeaveId).ToList();
                int i = 1;
                foreach (var litem in llist)
                {
                    //未审批
                    if (litem.EXRESULT == 0)
                    {
                        item.IsExamine = 0;
                        item.IsExamineWord = "未审核";
                        item.Examiner = litem.EXUSERID;
                        item.LEId = litem.LEID;
                        break;
                    }
                    //已批准
                    if (litem.EXRESULT == 1 && i == llist.Count)
                    {
                        item.IsExamine = 1;
                        item.IsExamineWord = "已审批";
                        item.LEId = litem.LEID;
                    }
                    //不批准
                    if (litem.EXRESULT == 2)
                    {
                        item.IsExamine = 2;
                        item.IsExamineWord = "未批准";
                        item.Examiner = litem.EXUSERID;
                        item.LEId = litem.LEID;
                        break;
                    }
                    i++;
                }
                                
                item.SDateStr = ((DateTime)item.SDate).ToLongDateString();
                item.EDateStr = ((DateTime)item.EDate).ToLongDateString();
            }

            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            list = list.Skip(PageIndex * 10).Take(10).ToList();

            return list;
        }

        /// <summary>
        /// 他人未审核请假情况
        /// </summary>
        /// <returns></returns>
        public static List<UserLeaveModel> GetOtherUnLeaveList(decimal UserId, decimal UnitId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = new List<UserLeaveModel>();
            decimal? leaveid = 0;
            QWGL_LEAVEEXAMINES model = new QWGL_LEAVEEXAMINES();
            list = (from l in db.QWGL_LEAVES

                     join u in db.SYS_USERS
                     on l.USERID equals u.USERID

                     join lt in db.QWGL_LEAVETYPES
                     on l.LTYPEID equals lt.LTYPEID

                     where u.UNITID == UnitId
                     orderby l.CREATETIME descending

                     select new UserLeaveModel
                     {
                         UserId = (decimal)l.USERID,
                         UserName = u.USERNAME,
                         UserAvatar = u.AVATAR,
                         LeaveId = l.LEAVEID,
                         LeaveTypeName = lt.LNAME,
                         SDate = l.SDATE,
                         EDate = l.EDATE,
                         CreateTime = l.CREATETIME,
                         QueryName = u.USERNAME.ToUpper(),
                     }).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                leaveid = list[i].LeaveId;
                model = db.QWGL_LEAVEEXAMINES.FirstOrDefault(t => t.LEAVEID == leaveid && t.EXRESULT == 0 && t.EXUSERID != UserId);
                if (model == null)
                    list.Remove(list[i]);
                else
                {
                    list[i].LEId = model.LEID;
                    list[i].SDateStr = ((DateTime)list[i].SDate).ToLongDateString();
                    list[i].EDateStr = ((DateTime)list[i].EDate).ToLongDateString();
                    list[i].DateStr = ((DateTime)list[i].CreateTime).ToString("yyyy-MM-dd");
                }
            }
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            if (list.Count != 0)
                list[0].AllCount = list.Count;
            list = list.Skip(PageIndex * 10).Take(10).ToList();

            return list;
        }

        /// <summary>
        /// 他人已审核请假情况
        /// </summary>
        /// <returns></returns>
        public static List<UserLeaveModel> GetOtherAlLeaveList(decimal UserId, decimal UnitId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = new List<UserLeaveModel>();
            decimal? leaveid = 0;
            QWGL_LEAVEEXAMINES model = new QWGL_LEAVEEXAMINES();
            list = (from l in db.QWGL_LEAVES

                    join u in db.SYS_USERS
                    on l.USERID equals u.USERID

                    join lt in db.QWGL_LEAVETYPES
                    on l.LTYPEID equals lt.LTYPEID

                    where u.UNITID == UnitId
                    orderby l.CREATETIME descending

                    select new UserLeaveModel
                    {
                        UserId = (decimal)l.USERID,
                        UserName = u.USERNAME,
                        UserAvatar = u.AVATAR,
                        LeaveId = l.LEAVEID,
                        LeaveTypeName = lt.LNAME,
                        SDate = l.SDATE,
                        EDate = l.EDATE,
                        CreateTime = l.CREATETIME,
                        QueryName = u.USERNAME.ToUpper(),
                    }).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                leaveid = list[i].LeaveId;
                model = db.QWGL_LEAVEEXAMINES.FirstOrDefault(t => t.LEAVEID == leaveid && t.EXRESULT > 0 && t.EXUSERID != UserId);
                if (model == null)
                    list.Remove(list[i]);
                else
                {
                    list[i].LEId = model.LEID;
                    list[i].SDateStr = ((DateTime)list[i].SDate).ToLongDateString();
                    list[i].EDateStr = ((DateTime)list[i].EDate).ToLongDateString();
                    list[i].DateStr = ((DateTime)list[i].CreateTime).ToString("yyyy-MM-dd");
                }
            }
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            if (list.Count != 0)
                list[0].AllCount = list.Count;
            list = list.Skip(PageIndex * 10).Take(10).ToList();

            return list;
        }

        /// <summary>
        /// 我的请假情况
        /// </summary>
        /// <returns></returns>
        public static List<UserLeaveModel> GetUserLeaveList(decimal UserId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = new List<UserLeaveModel>();

            list = (from l in db.QWGL_LEAVES

                     join u in db.SYS_USERS
                     on l.USERID equals u.USERID

                     join lt in db.QWGL_LEAVETYPES
                     on l.LTYPEID equals lt.LTYPEID

                     where l.USERID == UserId
                     orderby l.CREATETIME descending

                     select new UserLeaveModel
                     {
                         UserId = (decimal)l.USERID,
                         UserName = u.USERNAME,
                         UserAvatar = u.AVATAR,
                         LeaveId = l.LEAVEID,
                         LeaveTypeName = lt.LNAME,
                         SDate = l.SDATE,
                         EDate = l.EDATE,
                         CreateTime = l.CREATETIME,
                         QueryName = u.USERNAME.ToUpper(),
                     }).ToList();

            foreach (var item in list)
            {
                //是否审批通过
                List<QWGL_LEAVEEXAMINES> llist = db.QWGL_LEAVEEXAMINES.Where(t => t.LEAVEID == item.LeaveId).ToList();
                int i = 1;
                foreach (var litem in llist)
                {
                    //未审批
                    if (litem.EXRESULT == 0)
                    {
                        item.IsExamine = 0;
                        item.IsExamineWord = "未审核";
                        item.Examiner = litem.EXUSERID;
                        item.LEId = litem.LEID;
                        break;
                    }
                    //已批准
                    if (litem.EXRESULT == 1 && i == llist.Count)
                    {
                        item.IsExamine = 1;
                        item.IsExamineWord = "已审批";
                        item.Examiner = litem.EXUSERID;
                        item.LEId = litem.LEID;
                    }
                    //不批准
                    if (litem.EXRESULT == 2)
                    {
                        item.IsExamine = 2;
                        item.IsExamineWord = "未批准";
                        item.Examiner = litem.EXUSERID;
                        item.LEId = litem.LEID;
                        break;
                    }
                    i++;
                }

                item.SDateStr = ((DateTime)item.SDate).ToLongDateString();
                item.EDateStr = ((DateTime)item.EDate).ToLongDateString();
                item.DateStr = ((DateTime)item.CreateTime).ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName)).ToList();
            list = list.Skip(PageIndex * 10).Take(10).ToList();

            return list;
        }

        /// <summary>
        /// 待我审批的请假情况
        /// </summary>
        /// <returns></returns>
        public static List<UserLeaveModel> GetDelayExamineLeaveList(decimal UnitId, decimal? UserId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = (from l in db.QWGL_LEAVES

                                         join u in db.SYS_USERS
                                         on l.USERID equals u.USERID

                                         join lt in db.QWGL_LEAVETYPES
                                         on l.LTYPEID equals lt.LTYPEID

                                         join le in db.QWGL_LEAVEEXAMINES
                                         on new { id1 = l.LEAVEID, id2 = UserId } equals new { id1 = (decimal)le.LEAVEID, id2 = le.EXUSERID }

                                         where u.UNITID == UnitId && le.EXRESULT == 0
                                         orderby l.CREATETIME

                                         select new UserLeaveModel
                                         {
                                             UserId = (decimal)l.USERID,
                                             LEId = le.LEID,
                                             UserName = u.USERNAME,
                                             UserAvatar = u.AVATAR,
                                             LeaveId = l.LEAVEID,
                                             LeaveTypeName = lt.LNAME,
                                             SDate = l.SDATE,
                                             EDate = l.EDATE,
                                             IsExamine = (decimal)le.EXRESULT,
                                             CreateTime = l.CREATETIME,
                                             QueryName = u.USERNAME.ToUpper(),
                                         }).ToList();
            foreach (var item in list)
            {
                item.SDateStr = ((DateTime)item.SDate).ToLongDateString();
                item.EDateStr = ((DateTime)item.EDate).ToLongDateString();
                item.DateStr = ((DateTime)item.CreateTime).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            if(list.Count != 0)
                list[0].AllCount = list.Count;
            list = list.Skip(PageIndex * 10).Take(10).ToList();

            return list;
        }

        /// <summary>
        /// 我已审批的请假情况
        /// </summary>
        /// <returns></returns>
        public static List<UserLeaveModel> GetReadyExamineLeaveList(decimal UnitId, decimal? UserId, int PageIndex, string QueryUserName)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = (from l in db.QWGL_LEAVES

                     join u in db.SYS_USERS
                     on l.USERID equals u.USERID

                     join lt in db.QWGL_LEAVETYPES
                     on l.LTYPEID equals lt.LTYPEID

                     join le in db.QWGL_LEAVEEXAMINES
                     on new { id1 = l.LEAVEID, id2 = UserId } equals new { id1 = (decimal)le.LEAVEID, id2 = le.EXUSERID }

                     where u.UNITID == UnitId && le.EXRESULT > 0
                     orderby le.EXTIME descending

                     select new UserLeaveModel
                     {
                         UserId = (decimal)l.USERID,
                         LEId = le.LEID,
                         UserName = u.USERNAME,
                         UserAvatar = u.AVATAR,
                         LeaveId = l.LEAVEID,
                         LeaveTypeName = lt.LNAME,
                         SDate = l.SDATE,
                         EDate = l.EDATE,
                         IsExamine = (decimal)le.EXRESULT,
                         CreateTime = le.EXTIME,
                         QueryName = u.USERNAME.ToUpper(),
                     }).ToList();
            foreach (var item in list)
            {
                item.SDateStr = ((DateTime)item.SDate).ToLongDateString();
                item.EDateStr = ((DateTime)item.EDate).ToLongDateString();
                item.DateStr = ((DateTime)item.CreateTime).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(QueryUserName))
                list = list.Where(t => t.QueryName.Contains(QueryUserName.ToUpper())).ToList();
            if (list.Count != 0)
                list[0].AllCount = list.Count;
            list = list.Skip(PageIndex * 10).Take(10).ToList();
            return list;
        }

        /// <summary>
        /// 根据审批标识获取审核信息
        /// </summary>
        /// <returns></returns>
        public static UserLeaveModel GetExamineLeaveList(decimal LEId)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = new List<UserLeaveModel>();
            UserLeaveModel model = new UserLeaveModel();

            list = (from le in db.QWGL_LEAVEEXAMINES

                     join l in db.QWGL_LEAVES
                     on le.LEAVEID equals l.LEAVEID

                     join u in db.SYS_USERS
                     on l.USERID equals u.USERID

                     join lt in db.QWGL_LEAVETYPES
                     on l.LTYPEID equals lt.LTYPEID

                     where le.LEID == LEId

                     select new UserLeaveModel
                     {
                         UserName = u.USERNAME,
                         UserAvatar = u.AVATAR,
                         LeaveId = l.LEAVEID,
                         LeaveTypeName = lt.LNAME,
                         SDate = l.SDATE,
                         EDate = l.EDATE,
                         LeaveDay = l.LEAVEDAY,
                         LeaveReason = l.LEAVEREASON,
                         CreateTime = l.CREATETIME,
                         IsExamine = (decimal)le.EXRESULT,
                         ExamineReason = le.EXCONTENT
                     }).ToList();

            foreach (var item in list)
            {
                item.SDateStr = ((DateTime)item.SDate).ToString("yyyy-MM-dd HH:mm");
                item.EDateStr = ((DateTime)item.EDate).ToString("yyyy-MM-dd HH:mm");
                item.DateStr = item.CreateTime.ToString();

                model.UserName = item.UserName;
                model.UserAvatar = item.UserAvatar;
                model.LeaveTypeName = item.LeaveTypeName;
                model.SDateStr = item.SDateStr;
                model.EDateStr = item.EDateStr;
                model.LeaveDay = item.LeaveDay;
                model.LeaveReason = item.LeaveReason;
                model.DateStr = item.CreateTime.ToString();
                model.IsExamine = item.IsExamine;
                model.ExamineReason = item.ExamineReason;
                if (model.IsExamine == 0)
                    model.IsExamineWord = "未审核";
                if (model.IsExamine == 1)
                    model.IsExamineWord = "已审批";
                if (model.IsExamine == 2)
                    model.IsExamineWord = "未批准";
            }

            return model;
        }

        /// <summary>
        /// 新增请假审批
        /// </summary>
        /// <returns></returns>
        public static int UpdateLeaveExamine(decimal LEId, decimal ExamineStatus, string ExamineContent)
        {
            Entities db = new Entities();
            QWGL_LEAVEEXAMINES model = db.QWGL_LEAVEEXAMINES.FirstOrDefault(t => t.LEID == LEId);
            model.EXRESULT = ExamineStatus;
            model.EXCONTENT = ExamineContent;
            model.EXTIME = DateTime.Now;
            return db.SaveChanges();
        }
        
        /// <summary>
        /// 根据用户标识获取信息
        /// </summary>
        /// <returns></returns>
        public static UserLeaveModel GetLeaveExamineByUserId(decimal UserId)
        {
            Entities db = new Entities();
            UserLeaveModel model = new UserLeaveModel();
            model.UserAvatar = db.SYS_USERS.FirstOrDefault(t => t.USERID == UserId).AVATAR;
            return model;
        }

        /// <summary>
        /// 待我审批的请假条数
        /// </summary>
        /// <returns></returns>
        public static int GetDelayExamineLeaveCount(decimal UserId)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = (from l in db.QWGL_LEAVES

                                         join le in db.QWGL_LEAVEEXAMINES
                                         on new { id1 = l.LEAVEID, id2 = UserId } equals new { id1 = (decimal)le.LEAVEID, id2 = (decimal)le.EXUSERID }

                                         where le.EXRESULT == 0

                                         select new UserLeaveModel
                                         {}).ToList();
            return list.Count;
        }

        /// <summary>
        /// 查看今日团队请假条数
        /// </summary>
        /// <returns></returns>
        public static int GetTodayTeamCount(decimal UnitId)
        {
            Entities db = new Entities();
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToLongDateString());
            List<UserLeaveModel> list = (from l in db.QWGL_LEAVES

                                         join u in db.SYS_USERS
                                         on l.USERID equals u.USERID

                                         where u.UNITID == UnitId && l.SDATE.Value.Year == nowdate.Year && l.SDATE.Value.Month == nowdate.Month && l.SDATE.Value.Day == nowdate.Day
                                         select new UserLeaveModel
                                         {}).ToList();
            return list.Count;
        }

        /// <summary>
        /// 查看他人未审核的请假条数
        /// </summary>
        /// <returns></returns>
        public static int GetOtherLeaveListCount(decimal UserId,decimal UnitId)
        {
            Entities db = new Entities();
            List<UserLeaveModel> list = new List<UserLeaveModel>();
            decimal? leaveid = 0;
            QWGL_LEAVEEXAMINES model = new QWGL_LEAVEEXAMINES();
            list = (from l in db.QWGL_LEAVES

                    join u in db.SYS_USERS
                    on l.USERID equals u.USERID

                    where u.UNITID == UnitId

                    select new UserLeaveModel
                    {
                        LeaveId = l.LEAVEID,
                    }).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                leaveid = list[i].LeaveId;
                model = db.QWGL_LEAVEEXAMINES.FirstOrDefault(t => t.LEAVEID == leaveid && t.EXRESULT == 0 && t.EXUSERID != UserId);
                if (model == null)
                    list.Remove(list[i]);
            }

            return list.Count;
        }

        //////////////pc端

        /// <summary>
        ///获取条件查询的队员请假列表
        /// </summary>
        /// <returns></returns>
        public static List<VMUserLeaveModel> GetUserLeaveSearchList(string username,string examinename, string starttime,string endtime, string status)
        {
            Entities db = new Entities();

            IQueryable<VMUserLeaveModel> list = (from l in db.QWGL_LEAVES
                                                 join u in db.SYS_USERS
                                                 on l.USERID equals u.USERID

                                                 join le in db.QWGL_LEAVEEXAMINES
                                                 on l.LEAVEID equals le.LEAVEID

                                                 join eu in db.SYS_USERS
                                                 on le.EXUSERID equals eu.USERID

                                                 join lt in db.QWGL_LEAVETYPES
                                                 on l.LTYPEID equals lt.LTYPEID

                                                 orderby l.CREATETIME descending

                                                 select new VMUserLeaveModel
                                                 {
                                                     UserId = (decimal)l.USERID,
                                                     UserName = u.USERNAME,
                                                     LEId = le.LEID,
                                                     LeaveType = (decimal)l.LTYPEID,
                                                     LeaveTypeName = lt.LNAME,
                                                     SDate = l.SDATE,
                                                     EDate = l.EDATE,
                                                     LeaveDay = l.LEAVEDAY,
                                                     LeaveReason = l.LEAVEREASON,
                                                     IsExamine = (decimal)le.EXRESULT,
                                                     ExaminerName = eu.USERNAME
                                                 });
            if (!string.IsNullOrEmpty(username))
                list = list.Where(t => t.UserName.Contains(username));
            if (!string.IsNullOrEmpty(examinename))
                list = list.Where(t => t.ExaminerName.Contains(examinename));
            if (!string.IsNullOrEmpty(starttime))
            {
                DateTime startTime = DateTime.Parse(starttime).Date;
                list = list.Where(t => t.SDate >= startTime);
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                DateTime endTime = DateTime.Parse(endtime).Date.AddDays(1);
                list = list.Where(t => t.EDate < endTime);
            }
            if (!string.IsNullOrEmpty(status))
            {
                decimal SStatus = decimal.Parse(status);
                list = list.Where(t => t.LeaveType == SStatus);
            }

            return list.ToList();
        }

    }
}
