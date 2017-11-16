using NBZGM.XTBG.BLL;
using NBZGM.XTBG.CustomClass;
using NBZGM.XTBG.CustomModels;
using NBZGM.XTBG.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NBZGM.XTBG.Controllers
{
    public class MyScheduleController : Controller
    {
        /// <summary>
        /// Newtonsoft时间格式化
        /// </summary>
        IsoDateTimeConverter timeFormat = new IsoDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm"
        };

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ScheduleAdd(string date)
        {
            ViewBag.date = date;
            return View();
        }
        public ActionResult ScheduleUpdate(decimal id, decimal? typeid)
        {
            ViewBag.typeid = typeid;
            if (typeid == 0 || typeid == null)
            {
                ViewBag.schedule = ScheduleBLL.GetSingle(id);
            }
            else
            {
                ViewBag.meeting = MeetingBLL.GetSingle(id);
            }
            return View();
        }
        public JsonResult Add(VMSchedule vmSchedule)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_SCHEDULE schedule = new XTBG_SCHEDULE()
            {
                STARTTIME = vmSchedule.ScheduleStartTime,
                ENDTIME = vmSchedule.ScheduleEndTime,
                CREATETIME = nowDt,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                SCHEDULETITLE = vmSchedule.ScheduleTitle,
                SCHEDULECONTENT = vmSchedule.ScheduleContent,
                SCHEDULETYPE = 1,
                STATUSID = 1,
            };
            ScheduleBLL.Insert(schedule);
            return Json(new { StatusID = 1 });
        }
        public JsonResult Remove(VMSchedule vmSchedule)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            ScheduleBLL.Delete(vmSchedule.ScheduleID.Value);
            return Json(new { StatusID = 1 });
        }
        public JsonResult Update(VMSchedule vmSchedule)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_SCHEDULE schedule = new XTBG_SCHEDULE()
            {
                SCHEDULEID = vmSchedule.ScheduleID.Value,
                STARTTIME = vmSchedule.ScheduleStartTime,
                ENDTIME = vmSchedule.ScheduleEndTime,
                CREATETIME = nowDt,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                SCHEDULETITLE = vmSchedule.ScheduleTitle,
                SCHEDULECONTENT = vmSchedule.ScheduleContent,
                SCHEDULETYPE = 1,
                STATUSID = 1,
            };
            ScheduleBLL.Update(schedule);
            return Json(new { StatusID = 1 });
        }
        public string GetScheduleList(VMScheduleQuery sq)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);

            IQueryable<XTBG_SCHEDULE> entities = ScheduleBLL.GetList();
            entities = entities.Where(m => m.CREATEUSERID == user.UserID);
            if (sq.start != null)
            {
                entities = entities.Where(m => m.STARTTIME >= sq.start);
            }
            if (sq.end != null)
            {
                sq.end = sq.end.Value.AddDays(1);
                entities = entities.Where(m => m.ENDTIME < sq.end);
            }
            var data = entities.Select(m => new schedule
            {
                id = m.SCHEDULEID,
                title = m.SCHEDULETITLE,
                start = m.STARTTIME,
                end = m.ENDTIME,
                //backgroundColor = "red",
            }).ToList();

            IQueryable<XTBG_MEETING> MeetingEntities = MeetingBLL.GetList();
            MeetingEntities = MeetingEntities.Where(m => m.USERIDS.Contains(UserID));
            if (sq.start != null)
            {
                MeetingEntities = MeetingEntities.Where(m => m.STARTTIME >= sq.start);
            }
            if (sq.end != null)
            {
                sq.end = sq.end.Value.AddDays(1);
                MeetingEntities = MeetingEntities.Where(m => m.ENDTIME < sq.end);
            }
            var meetingData = MeetingEntities.Select(m => new schedule
            {
                id = m.MEETINGID,
                typeid = 1,
                title = "会议：" + m.MEETINGNAME,
                start = m.STARTTIME,
                end = m.ENDTIME,
                backgroundColor = "red",
            }).ToList();

            return JsonConvert.SerializeObject(data.Concat(meetingData), Formatting.Indented, timeFormat);
        }
    }
    public class schedule
    {
        public decimal id { get; set; }
        public decimal typeid { get; set; }
        public string backgroundColor { get; set; }
        public string title { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
    }
}
