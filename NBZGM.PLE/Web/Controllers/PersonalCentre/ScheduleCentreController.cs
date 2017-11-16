using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.BLL.ScheduleBLLs;
using Taizhou.PLE.BLL.UserBLLs;

namespace Web.Controllers.PersonalCentre
{
    public class ScheduleCentreController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/PersonalCentre/ScheduleCentre/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 添加一个日程
        /// </summary>
        /// <returns></returns>
        public bool AddEvent()
        {
            string strStartTime = Request.Form["startTime"];
            string strEndTime = Request.Form["endTime"];
            string strScheduleType = Request.Form["scheduleType"];
            string strIsAllDayEvent = Request.Form["isAllDayEvent"];
            string strIsShare = Request.Form["isShare"];
            string strUserID = Request.Form["userID"];
            string strTitle = Request["title"];

            SCHEDULE schedule = new SCHEDULE();
            if (!string.IsNullOrWhiteSpace(strTitle))
            {
                schedule.TITLE = strTitle;
            }
            else
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(strStartTime))
            {
                schedule.STARTTIME = Convert.ToDateTime(strStartTime);
            }

            if (!string.IsNullOrWhiteSpace(strEndTime))
            {
                schedule.ENDTIME = Convert.ToDateTime(strEndTime);
            }

            if (!string.IsNullOrWhiteSpace(strScheduleType))
            {
                schedule.SCHEDULETYPEID = Convert.ToDecimal(strScheduleType);
            }

            if (!string.IsNullOrWhiteSpace(strIsShare))
            {
                schedule.ISALLDAYEVENT = Convert.ToDecimal(strIsShare);
            }

            if (!string.IsNullOrWhiteSpace(strIsAllDayEvent))
            {
                schedule.SHARETYPEID = Convert.ToDecimal(strIsAllDayEvent);
            }

            if (!string.IsNullOrWhiteSpace(strUserID))
            {
                schedule.CREATEDUSERID = Convert.ToDecimal(strUserID);
            }
            schedule.CREATEDITME = DateTime.Now;
            return ScheduleBLL.AddSchedule(schedule);
        }

        /// <summary>
        /// 获取日程数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEventSources()
        {
            string strStartTime = Request.Form["start"];
            string strEndTime = Request.Form["end"];

            string strUserID = Request.Form["userID"];
            //部门共享ID
            string strShareUserIDs = Request.Form["shareUserID"];
            //全局共享
            string strIsAllShare = Request.Form["isAllshare"];
            string viewMode = Request.Form["viewMode"];
            //判断是否获取自己的日程
            bool isMyself = true;

            List<USER> userList = new List<USER>();

            if (string.IsNullOrWhiteSpace(strUserID))
            {
                return null;
            }

            decimal userID = Convert.ToDecimal(strUserID);
            List<decimal> shareUserIDs = new List<decimal>();

            string[] arryUserIDs = null;
            if (!string.IsNullOrEmpty(strShareUserIDs))
            {
                isMyself = false;
                arryUserIDs = strShareUserIDs.Split(',');
                foreach (string ID in arryUserIDs)
                {
                    if (string.IsNullOrWhiteSpace(ID))
                    {
                        continue;
                    }
                    else
                    {
                        decimal dUserID = Convert.ToDecimal(ID);
                        if (dUserID != userID && dUserID > 0)
                        {
                            shareUserIDs.Add(dUserID);
                        }
                        else
                        {
                            if (dUserID == -2)
                            {
                                userList = UserBLL.GetAllUsers().ToList();
                                userList.RemoveAll(t => t.USERID == userID);
                            }

                            if (dUserID == userID)
                            {
                                isMyself = true;
                            }
                        }
                    }
                }
            }

            DateTime startTime = Convert.ToDateTime(strStartTime);
            DateTime endTime = Convert.ToDateTime(strEndTime);

            int intervalTime = (endTime).Subtract(startTime).Days;
            List<EventModel> scheduleResult = new List<EventModel>();

            //由于需要请求次数过多,直接获取者一段时间的所有日程
            List<SCHEDULE> cacheSchedule = ScheduleBLL.GetScheduleList
                (null, null, startTime, endTime).ToList();

            for (int span = 1; span <= intervalTime; span++)
            {
                List<SCHEDULE> scheduleList = new List<SCHEDULE>();

                DateTime minTime = startTime.AddDays(span - 1); ;
                DateTime maxTime = minTime.AddDays(1);

                //添加个人日程
                if (isMyself)
                {
                    List<SCHEDULE> tempScheduleList = new List<SCHEDULE>();

                    tempScheduleList = cacheSchedule.Where(t => t.OWNER
                        == userID).Where(t => t.STARTTIME >= minTime && t.ENDTIME
                            < maxTime).ToList();

                    scheduleList.AddRange(tempScheduleList);
                }

                //添加部门共享日程
                foreach (decimal ID in shareUserIDs)
                {
                    List<SCHEDULE> tempScheduleList = new List<SCHEDULE>();

                    tempScheduleList = cacheSchedule.Where(t => t.OWNER
                        == ID && t.SHARETYPEID == 1).Where(t => t.STARTTIME >= minTime && t.ENDTIME
                            < maxTime).ToList();

                    scheduleList.AddRange(tempScheduleList);
                }

                //添加全部全局共享日程
                foreach (USER user in userList)
                {
                    List<SCHEDULE> tempScheduleList = new List<SCHEDULE>();

                    tempScheduleList = cacheSchedule.Where(t => t.OWNER
                        == user.USERID && t.SHARETYPEID == 2).Where(t => t.STARTTIME >= minTime && t.ENDTIME
                            < maxTime).ToList();

                    scheduleList.AddRange(tempScheduleList);
                }

                if (scheduleList.Count() > 5  &&  viewMode == "month")
                {
                    scheduleList = scheduleList.OrderBy(t => t.SCHEDULEID).Take(4).ToList();
                    for (int i = 0; i < 4; i++)
                    {
                        scheduleResult.Add(new EventModel
                        {
                            title = scheduleList[i].TITLE,
                            evtStart = scheduleList[i].STARTTIME,
                            evtEnd = scheduleList[i].ENDTIME,
                            ID = scheduleList[i].SCHEDULEID,
                            type = scheduleList[i].SCHEDULETYPEID,
                            draggable = false
                        });
                    }

                    DateTime dt = Convert.ToDateTime(scheduleList[0].ENDTIME);
                    dt.AddHours(-dt.Hour + 23);
                    dt.AddMinutes(-dt.Minute + 59);
                    dt.AddSeconds(-dt.Second + 59);
                    scheduleResult.Add(new EventModel
                    {
                        draggable = false,
                        title = "更多...",
                        evtStart = dt,
                        evtEnd = dt,
                        ID = -1,
                        type = -1
                    });
                }
                else
                {
                    var temp1 = from m in scheduleList
                                select new EventModel
                                {
                                    title = m.TITLE,
                                    evtStart = m.STARTTIME,
                                    evtEnd = m.ENDTIME,
                                    ID = m.SCHEDULEID,
                                    type = m.SCHEDULETYPEID
                                };
                    scheduleResult.AddRange(temp1.ToList());
                }
            }

            //查询所有跨天的日程
            List<SCHEDULE> crossDay = new List<SCHEDULE>();

            if (isMyself)
            {
                List<SCHEDULE> tempScheduleList = new List<SCHEDULE>();

                tempScheduleList = cacheSchedule.Where(t => t.CREATEDUSERID
                  == userID).Where(t => endTime >= t.STARTTIME
                 &&  startTime <= t.ENDTIME  &&  Convert.ToDateTime(t.STARTTIME).Day !=
               Convert.ToDateTime(t.ENDTIME).Day).ToList();

                crossDay.AddRange(tempScheduleList);
            }

            foreach (decimal ID in shareUserIDs)
            {
                List<SCHEDULE> tempScheduleList = new List<SCHEDULE>();

                tempScheduleList = cacheSchedule.Where(t => t.CREATEDUSERID
                    == ID && t.SHARETYPEID == 1).Where(t => endTime >= t.STARTTIME
                && startTime <= t.ENDTIME && Convert.ToDateTime(t.STARTTIME).Day !=
                Convert.ToDateTime(t.ENDTIME).Day).ToList();

                crossDay.AddRange(tempScheduleList);
            }

            foreach (USER user in userList)
            {
                List<SCHEDULE> tempScheduleList = new List<SCHEDULE>();

                tempScheduleList = cacheSchedule.Where(t => t.CREATEDUSERID
                  == user.USERID && t.SHARETYPEID == 2).Where(t => endTime >= t.STARTTIME
              && startTime <= t.ENDTIME && Convert.ToDateTime(t.STARTTIME).Day !=
              Convert.ToDateTime(t.ENDTIME).Day).ToList();

                crossDay.AddRange(tempScheduleList);
            }

            var temp2 = from m in crossDay
                        select new EventModel
                        {
                            title = m.TITLE,
                            evtStart = m.STARTTIME,
                            evtEnd = m.ENDTIME,
                            ID = m.SCHEDULEID,
                            type = m.SCHEDULETYPEID
                        };
            scheduleResult.AddRange(temp2.ToList());

            return Json(scheduleResult);
        }

        /// <summary>
        /// 根据日程ID获取日程
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEventByID()
        {
            string eventID = Request.QueryString["eventID"];
            if (string.IsNullOrEmpty(eventID))
            {
                return null;
            }
            SCHEDULE schedule = ScheduleBLL.GetScheduleByScheduleID(Convert.ToDecimal(eventID));
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获得用户节点数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUsersNode()
        {
            string userID = Request.QueryString["userID"];
            if (string.IsNullOrWhiteSpace(userID))
            {
                return null;
            }

            List<UserTreeModel> userNodes = new List<UserTreeModel>();
            userNodes.Add(new UserTreeModel
            {
                id = "-1",
                pId = null,
                open = true,
                type = "父节点",
                value = "null",
                name = "部门共享",
                @checked = true
            });


            userNodes.Add(new UserTreeModel
            {
                id = "-2",
                pId = null,
                type = "父节点",
                value = "null",
                name = "公开共享",
                @checked = false
            });

            USER user = UserBLL.GetUserByUserID(Convert.ToDecimal(userID));
            List<USER> userList = UserBLL.GetTotalUsersByUnitID(user.UNITID).ToList();

            foreach (var u in userList)
            {
                userNodes.Add(new UserTreeModel
                {
                    id = u.USERID.ToString(),
                    pId = "-1",
                    type = "子节点",
                    value = u.USERID.ToString(),
                    name = u.USERNAME,
                    @checked = u.USERID == user.USERID ? true : false
                });
            }
            return Json(userNodes, JsonRequestBehavior.AllowGet);
        }
    }
}
