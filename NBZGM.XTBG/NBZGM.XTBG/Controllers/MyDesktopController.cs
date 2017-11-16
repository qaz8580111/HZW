using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBZGM.XTBG.Models;
using NBZGM.XTBG.BLL;
using Newtonsoft.Json;
using NBZGM.XTBG.CustomModels;
using NBZGM.XTBG.CustomClass;
using Newtonsoft.Json.Converters;

namespace NBZGM.XTBG.Controllers
{
    public class MyDesktopController : Controller
    {
        public ActionResult Index()
        {
            UserInfo User = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            DateTime thisDt = nowDt.Date;
            DateTime nextDt = thisDt.AddDays(1);
            string UserID = string.Format(",{0},", User.UserID);
            string UnitID = string.Format(",{0},", User.UnitID);

            List<SYS_FUNCTIONS> FunctionEntities = FunctionBLL.GetFunctionsByUserID(User.UserID).Where(m => m.PARENTID == 112).ToList();
            List<string> FunctionEntitiesCode = FunctionEntities.Select(m => m.CODE).ToList();

            ViewBag.MeetingEntities = MeetingBLL.GetList().Where(m => m.USERIDS.Contains(UserID)).OrderByDescending(m => m.MEETINGID).Take(5).ToList();
            ViewBag.AnnoEntities = AnnoBLL.GetList().OrderByDescending(m => m.ANNOUNCEMENTID).Take(5).ToList();
            ViewBag.MailEntities = EmailBLL.GetList().Where(m => m.USERIDS.Contains(UserID)).OrderByDescending(m => m.EMAILID).Take(5).ToList();
            ViewBag.SmsEntities = SmsBLL.GetList().Where(m => m.RECIPIENTUSERIDS.Contains(UserID)).OrderByDescending(m => m.SMSID).Take(5).ToList();
            ViewBag.FileEntities = FileBLL.GetList().Where(m => m.RECIPIENTUSERIDS.Contains(UserID)).OrderByDescending(m => m.FILEID).Take(5).ToList();

            decimal AnnoCount = AnnoBLL.GetList().Where(m => m.ANNOUNCEMENTSCOPEID.Contains(UnitID) && (!m.USERIDS2.Contains(UserID) || m.USERIDS2 == null)).Count();
            decimal MeetingCount = MeetingBLL.GetList().Where(m => m.STARTTIME > nowDt).Count();
            decimal MailCount = EmailBLL.GetList().Where(m => m.USERIDS.Contains(UserID) && (!m.USERIDS2.Contains(UserID) || m.USERIDS2 == null)).Count();
            decimal FileCount = FileBLL.GetList().Where(m => m.RECIPIENTUSERIDS.Contains(UserID) && (!m.RECIPIENTUSERIDS2.Contains(UserID) || m.RECIPIENTUSERIDS2 == null)).Count();
            decimal ScheduleCount = ScheduleBLL.GetList().Where(m => m.CREATEUSERID == User.UserID && m.STARTTIME >= thisDt && m.STARTTIME <= nextDt).Count();

            ViewBag.UserID = UserID;
            List<VMLi> Authority = new List<VMLi>(){
                new VMLi(){ key = "MailManagement",        value = "发送邮件",pane="home"},
                new VMLi(){ key = "SMSManagement",         value = "发送短信",pane="home"},
                new VMLi(){ key = "MeetingManagement",     value = "我的会议",pane="MyMeeting",remaining=MeetingCount},
                new VMLi(){ key = "AnnouncementManagement",value = "公告通知",pane="settings1",remaining=AnnoCount},
                new VMLi(){ key = "MailManagement",        value = "我的邮件",pane="settings", remaining=MailCount},
                new VMLi(){ key = "SMSManagement",         value = "我的短信",pane="settings"},
                new VMLi(){ key = "FileManagement",        value = "我的文件",pane="MyFile",  remaining=FileCount},
                new VMLi(){ key = "MySchedule",            value = "我的日程",remaining=ScheduleCount},
            };
            List<VMLi> Authority_VIP = new List<VMLi>(){
                new VMLi(){ key = "AnnouncementManagement",value = "发布公告",pane="home"},
                new VMLi(){ key = "FileManagement",        value = "发布文件",pane="FileSend"},
                new VMLi(){ key = "MailManagement",        value = "发送邮件",pane="home"},
                new VMLi(){ key = "SMSManagement",         value = "发送短信",pane="home"},
                new VMLi(){ key = "MailManagement",        value = "我的邮件",pane="settings",remaining=MailCount},
                new VMLi(){ key = "SMSManagement",         value = "我的短信",pane="settings"},
                new VMLi(){ key = "MeetingManagement",     value = "预约会议",pane="MeetingAdd"},
                new VMLi(){ key = "TaskManagement",        value = "新建任务",pane="settings"},
            };
            List<VMLi> AuthorityUser = new List<VMLi>();
            if (User.RoleIDS.Where(m => m.ROLEID == 16).FirstOrDefault() != null)
            {
                AuthorityUser = Authority_VIP;
            }
            else
            {
                AuthorityUser = Authority;
            }
            AuthorityUser = AuthorityUser.Where(m => FunctionEntitiesCode.Contains(m.key)).ToList();
            ViewBag.FunctionEntities = FunctionEntities;
            ViewBag.Authority = AuthorityUser;
            return View();
        }
    }
}
