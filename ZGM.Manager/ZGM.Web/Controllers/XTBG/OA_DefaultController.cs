using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.XTBG;
using ZGM.BLL.XTBGBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Model.PhoneModel;
using ZGM.Model.ViewModels;
using ZGM.Model.XTBGModels;

namespace ZGM.Web.Controllers.XTBG
{
    public class OA_DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<PHAnnouncement> list_Notice = GetNOTICES();
            ViewBag.list_Notice = list_Notice;//9条公告

            List<ConferenceList> list_Meetings = GetMeetingS();
            ViewBag.list_Meetings = list_Meetings;//7条会议

            List<VMOAFile> list_Files = GetFiles();
            ViewBag.list_Files = list_Files;//7条文件

            List<TasksListModel> list_Task = GetTask();
            ViewBag.list_Task = list_Task;//7条任务
            return View();
        }

        /// <summary>
        /// 初始加载日程
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEventSources()
        {
            string strStartTime = Request.Form["start"];
            string strEndTime = Request.Form["end"];
            string viewMode = Request.Form["viewMode"];

            DateTime startTime = Convert.ToDateTime(strStartTime);
            DateTime endTime = Convert.ToDateTime(strEndTime);
            int intervalTime = (endTime).Subtract(startTime).Days;
            List<EventModel> scheduleResult = new List<EventModel>();
            List<OA_SCHEDULES> cacheSchedule = OA_ScheduleBLL.GetScheduleList(SessionManager.User.UserID, startTime, endTime).ToList();
            var temp1 = from m in cacheSchedule
                        select new EventModel
                        {
                            title = m.TITLE,
                            evtStart = m.STARTTIME,
                            evtEnd = m.ENDTIME,
                            ID = m.SCHEDULEID,
                            type = m.SCHEDULESOURCE
                        };
            scheduleResult.AddRange(temp1.ToList());

            return Json(scheduleResult);
        }

        /// <summary>
        /// 添加日程
        /// </summary>
        /// <returns></returns>
        public int AddEvent()
        {
            string strStartTime = Request["start"];
            string strEndTime = Request["end"];
            string strScheduleType = Request["rctype"];

            string strContent = Request["rccontent"];
            string strTitle = Request["rctitle"];

            OA_SCHEDULES model = new OA_SCHEDULES();
            model.TITLE = strTitle;
            model.SHARETYPEID = 0;
            model.SCHEDULESOURCE = strScheduleType;
            model.CONTENT = strContent;
            model.OWNER = SessionManager.User.UserID;
            model.CREATEDUSERID = SessionManager.User.UserID;
            model.CREATEDITME = DateTime.Now;
            if (!string.IsNullOrEmpty(strStartTime))
            {
                model.STARTTIME = Convert.ToDateTime(strStartTime);
            }
            if (!string.IsNullOrEmpty(strEndTime))
            {
                model.ENDTIME = Convert.ToDateTime(strEndTime);
            }
            // int days = (model.ENDTIME.Value - model.STARTTIME.Value).Days;
            int result = 0;

            // for (int i = 0; i <= days; i++)
            //{
            // model.STARTTIME = Convert.ToDateTime(strStartTime).AddDays(i);
            //  model.ENDTIME = Convert.ToDateTime(strStartTime).AddDays(i).Date.AddHours(model.ENDTIME.Value.Hour).AddMinutes(model.ENDTIME.Value.Minute).AddSeconds(model.ENDTIME.Value.Second);
            if (OA_ScheduleBLL.AddScedule(model) == true)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
            // }
            return result;
        }

        /// <summary>
        /// 修改日程
        /// </summary>
        /// <returns></returns>
        public int EditEvent()
        {
            string SCHEDULEID = Request["id"];
            if (string.IsNullOrEmpty(SCHEDULEID))
            {
                return 0;
            }
            // else
            // {
            //    OA_ScheduleBLL.DelScedule(decimal.Parse(SCHEDULEID));//删除当前日程，然后添加日程
            // }

            string strStartTime = Request["start"];
            string strEndTime = Request["end"];
            string strScheduleType = Request["rctype"];

            string strContent = Request["rccontent"];
            string strTitle = Request["rctitle"];

            OA_SCHEDULES model = new OA_SCHEDULES();
            model.SCHEDULEID = decimal.Parse(SCHEDULEID);
            model.TITLE = strTitle;
            model.SHARETYPEID = 0;
            model.SCHEDULESOURCE = strScheduleType;
            model.CONTENT = strContent;
            model.OWNER = SessionManager.User.UserID;
            model.CREATEDUSERID = SessionManager.User.UserID;
            model.CREATEDITME = DateTime.Now;
            if (!string.IsNullOrEmpty(strStartTime))
            {
                model.STARTTIME = Convert.ToDateTime(strStartTime);
            }
            if (!string.IsNullOrEmpty(strEndTime))
            {
                model.ENDTIME = Convert.ToDateTime(strEndTime);
            }
            //int days = (model.ENDTIME.Value - model.STARTTIME.Value).Days;
            int result = 0;
            // for (int i = 0; i <= days; i++)
            // {
            //    model.STARTTIME = Convert.ToDateTime(strStartTime).AddDays(i);
            //    model.ENDTIME = Convert.ToDateTime(strStartTime).AddDays(i).Date.AddHours(model.ENDTIME.Value.Hour).AddMinutes(model.ENDTIME.Value.Minute).AddSeconds(model.ENDTIME.Value.Second);
            if (OA_ScheduleBLL.EditScedule(model) == true)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
            //  }
            return result;
        }

        /// <summary>
        /// 根据日程ID删除当前日程
        /// </summary>
        /// <returns></returns>
        public int DeleteEventByID()
        {
            string SCHEDULEID = Request["id"];

            if (string.IsNullOrEmpty(SCHEDULEID))
            {
                return 0;
            }
            else
            {
                bool flag = OA_ScheduleBLL.DelScedule(decimal.Parse(SCHEDULEID));//删除当前日程
                if (flag)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

        }



        /// <summary>
        /// 根据日程ID获取日程详情
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEventByID()
        {
            string eventID = Request.QueryString["eventID"];
            if (string.IsNullOrEmpty(eventID))
            {
                return null;
            }
            OA_SCHEDULES schedule = OA_ScheduleBLL.GetScheduleByScheduleID(Convert.ToDecimal(eventID));
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取公告
        /// </summary>
        /// <returns></returns>
        public List<PHAnnouncement> GetNOTICES()
        {
            List<PHAnnouncement> list = OA_NoticeBLL.GetNoticeByDefault(SessionManager.User.UserID).OrderByDescending(t => t.CREATEDTIME).Take(9).ToList();
            return list;
        }

        /// <summary>
        /// 获取待参与会议
        /// </summary>
        /// <returns></returns>
        public List<ConferenceList> GetMeetingS()
        {
            //List<ConferenceList> list = OA_MEETINGSBLL.GetMeetingByDefalt(SessionManager.User.UserID).OrderByDescending(t => t.STIME).Take(7).ToList();  //以前方法
            List<ConferenceList> list = OA_MEETINGSBLL.GetMeetinglistAll(SessionManager.User.UserID).Where(a => a.ISCANCEL == 1 && a.STIME > DateTime.Now).OrderByDescending(t => t.STIME).Take(7).ToList();
            return list;
        }

        /// <summary>
        /// 获取首页文件
        /// </summary>
        /// <returns></returns>
        public List<VMOAFile> GetFiles()
        {
            List<VMOAFile> list = OA_FileBLL.GetFilesByDefault(SessionManager.User.UserID).Where(a => a.Status == 0).OrderByDescending(t => t.CREATETIME).Take(7).ToList();
            return list;
        }

        /// <summary>
        /// 获取首页待办任务
        /// </summary>
        /// <returns></returns>
        public List<TasksListModel> GetTask()
        {
            List<TasksListModel> list = OA_TASKSBLL.GetAllEvent(SessionManager.User.UserID).Where(wf => wf.status == 1).OrderByDescending(t => t.createtime).Take(7).ToList();
            return list;
        }

    }
}
