using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.XCJGBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseWorkflowModels;

namespace Web.Controllers.IntegratedService.ScheduleManagement
{
    public class MemberPatrolTaskController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/MemberPatrolTask/";

        //
        // GET: /执法队员巡查任务管理/

        public ActionResult Index()
        {
            string zdid = Request["SSZDID"];
            decimal zdId = 0;
            decimal.TryParse(zdid, out zdId);


            DateTime dtNow = DateTime.Now;//获取当天时间
            DateTime.TryParse(Request["t"], out dtNow);
            if (dtNow == Convert.ToDateTime("0001/1/1 0:00:00"))
                dtNow = DateTime.Now;
            ViewBag.urlUpWeek = "/MemberPatrolTask/Index?t=" + dtNow.AddDays(-7).ToString("yyyy-MM-dd");
            ViewBag.urlNextWeek = "/MemberPatrolTask/Index?t=" + dtNow.AddDays(7).ToString("yyyy-MM-dd");


            ViewBag.GetTableOneMes = GetTableOneMes(dtNow);
            ViewBag.GetTableContentMes = GetTableContentMes(dtNow, zdId);

            List<SelectListItem> unitList = new List<SelectListItem>();
            unitList.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "请选择中队",
            });

            List<SelectListItem> DAIDList = UnitBLL.GetUnitByUnitTypeID(4)
               .Select(c => new SelectListItem
               {
                   Text = c.UNITNAME,
                   Value = c.UNITID.ToString()
               }).ToList();
            DAIDList.Insert(0, new SelectListItem()
            {
                Selected = true,
                Text = "请选择大队",
                Value = "0"
            });
            if (zdId > 0)
            {
                decimal ddid = UnitBLL.GetParentIDByUnitID(zdId);
                DAIDList.FirstOrDefault(t => t.Value == ddid.ToString()).Selected = true;
                unitList = UnitBLL.GetZDUnitsByParentID(ddid).Select(t => new SelectListItem()
                {
                    Text = t.UNITNAME,
                    Value=t.UNITID.ToString(),
                    Selected = t.UNITID == zdId?true:false
                }).ToList();
            }


            ViewBag.unitList = unitList;
            ViewBag.DAIDList = DAIDList;
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        #region ajax获取值

        public string GetHour()
        {
            return GetHoursMes();
        }
        public string GetEndHour()
        {
            return GetEndHoursMes();
        }
        public string GetMinute()
        {
            return GetMinutesMes();
        }
        public string GetEndMinute()
        {
            return GetEndMinutesMes();
        }
        public string getMap()
        {
            return getGEOMETRY();
        }
        public string getRouteMap()
        {
            return getRouteGEOMETRY();
        }
        public string getZDMember() {
            return GetZDMember();
        }
        public string getWeek() {
            return GetWeekStr();
        }

        #endregion

        #region 获取日历

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="dtNow">时间</param>
        /// <returns></returns>
        public string GetTableContentMes(DateTime dtNow, decimal ZDID)
        {
            //string SSZDID = Request["SSZDID"];
            //decimal ZDID=0;
            //decimal.TryParse(SSZDID, out ZDID);
            if (ZDID == 0)
                ZDID = SessionManager.User.UnitID;


            StringBuilder sbMes = new StringBuilder();
            IList<USER> userList = UserBLL.GetAllUsers().Where(a => a.UNITID == ZDID).ToList();

            if (ZDID == SessionManager.User.UnitID)
            {
                #region 拼接日历视图
                if (userList != null && userList.Count() > 0)
                {
                    //获取要显示的周期
                    int startIndex = 0;
                    int endIndex = 0;
                    GetStartEndIndex(dtNow, ref startIndex, ref endIndex);
                    IList<XCJGUSERTASK> UserTaskList = PatrolUserTaskBLL.GetXCJGUserTasks().ToList();

                    foreach (var item in userList)
                    {
                        sbMes.Append("<tr>");
                        sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\">" + item.USERNAME + "</td>");
                        for (int i = startIndex; i < endIndex; i++)//遍历对应时间的勤务
                        {

                            DateTime dtnow_New = dtNow.AddDays(i);

                            DateTime dtOne = dtnow_New.Date.Date;
                            DateTime dtTwo = dtOne.AddDays(1);

                            IList<XCJGUSERTASK> UserTaskList_Where = UserTaskList
                                .Where(a => a.USERID == item.USERID && a.TASKDATE >= dtOne && a.TASKDATE < dtTwo).ToList();

                            string onlyT = item.USERID + "_" + dtnow_New.ToString("yyyyMMdd");

                            if (UserTaskList_Where != null && UserTaskList_Where.Count() > 0)
                            {
                                sbMes.Append("<td  id=\"td_" + onlyT + "\"   style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#cbbfbf\"  onmousemove=\"YesOnmousemove('" + onlyT + "')\"  onmouseout=\"YesOnmouseout('" + onlyT + "')\"  onclick=\"EditUserTask('" + item.USERID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "')\">&nbsp;</td>");
                            }
                            else
                            {
                                sbMes.Append("<td id=\"td_" + onlyT + "\"  style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\"  onmousemove=\"NoOnmousemove('" + onlyT + "')\"  onmouseout=\"NoOnmouseout('" + onlyT + "')\"  onclick=\"AddUserTask('" + item.USERID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "')\">&nbsp;</td>");
                            }
                        }
                        sbMes.Append("</tr>");
                    }
                }
                else
                {
                    sbMes.Append("<tr>");
                    sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse; color:red;\" colspan=\"8\">没有查询到队员</td>");
                    sbMes.Append("</tr>");
                }
                #endregion
            }
            else
            {
                #region 拼接日历视图
                if (userList != null && userList.Count() > 0)
                {
                    //获取要显示的周期
                    int startIndex = 0;
                    int endIndex = 0;
                    GetStartEndIndex(dtNow, ref startIndex, ref endIndex);
                    IList<XCJGUSERTASK> UserTaskList = PatrolUserTaskBLL.GetXCJGUserTasks().ToList();

                    foreach (var item in userList)
                    {
                        sbMes.Append("<tr>");
                        sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\">" + item.USERNAME + "</td>");
                        for (int i = startIndex; i < endIndex; i++)//遍历对应时间的勤务
                        {
                            DateTime dtnow_New = dtNow.AddDays(i);

                            DateTime dtOne = dtnow_New.Date.Date;
                            DateTime dtTwo = dtOne.AddDays(1);

                            IList<XCJGUSERTASK> UserTaskList_Where = UserTaskList
                                .Where(a => a.USERID == item.USERID && a.TASKDATE >= dtOne && a.TASKDATE < dtTwo).ToList();

                            string onlyT = item.USERID + "_" + dtnow_New.ToString("yyyyMMdd");

                            if (UserTaskList_Where != null && UserTaskList_Where.Count() > 0)
                            {
                                sbMes.Append("<td  id=\"td_" + onlyT + "\"   style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#cbbfbf\"  onmousemove=\"YesOnmousemove('" + onlyT + "')\"  onmouseout=\"YesOnmouseout('" + onlyT + "')\" >&nbsp;</td>");
                            }
                            else
                            {
                                sbMes.Append("<td   id=\"td_" + onlyT + "\"  style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\"  onmousemove=\"NoOnmousemove('" + onlyT + "')\"  onmouseout=\"NoOnmouseout('" + onlyT + "')\">&nbsp;</td>");
                            }
                        }
                        sbMes.Append("</tr>");
                    }
                }
                else
                {
                    sbMes.Append("<tr>");
                    sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;color:red;\" colspan=\"8\">没有查询到队员</td>");
                    sbMes.Append("</tr>");
                }
                #endregion
            }
            return sbMes.ToString();
        }





        /// <summary>
        /// 获取日期表
        /// </summary>
        /// <param name="dtNow">时间</param>
        /// <returns></returns>
        public string GetTableOneMes(DateTime dtNow)
        {
            StringBuilder sbMes = new StringBuilder();
            sbMes.Append("<tr>");
            sbMes.Append("<th style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;\">&nbsp;</th>");

            int startIndex = 0;
            int endIndex = 0;
            GetStartEndIndex(dtNow, ref startIndex, ref endIndex);

            for (int i = startIndex; i < endIndex; i++)
            {
                string dayWeek;
                string MD = GetMDT(dtNow, i, out dayWeek);
                sbMes.Append("<th style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;\">" + MD + "(" + dayWeek.Replace("星期", "") + ")</th>");
            }

            sbMes.Append("</tr>");
            return sbMes.ToString();
        }

        /// <summary>
        /// 获取起始时间和结束时间
        /// </summary>
        /// <param name="dtNow">时间</param>
        /// <param name="startIndex">起始编号</param>
        /// <param name="endIndex">结束编号</param>
        private static void GetStartEndIndex(DateTime dtNow, ref int startIndex, ref int endIndex)
        {
            switch (dtNow.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    startIndex = 0;
                    endIndex = 7;
                    break;
                case DayOfWeek.Tuesday:
                    startIndex = -1;
                    endIndex = 6;
                    break;
                case DayOfWeek.Wednesday:
                    startIndex = -2;
                    endIndex = 5;
                    break;
                case DayOfWeek.Thursday:
                    startIndex = -3;
                    endIndex = 4;
                    break;
                case DayOfWeek.Friday:
                    startIndex = -4;
                    endIndex = 3;
                    break;
                case DayOfWeek.Saturday:
                    startIndex = -5;
                    endIndex = 2;
                    break;
                case DayOfWeek.Sunday:
                    startIndex = -6;
                    endIndex = 1;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 返回月.日
        /// </summary>
        /// <param name="dtnow">时间</param>
        /// <param name="type">以当前为星期一计算前7天和后7天【-7,-6,-5,-4,-3,-2,-1,0,1,2,3,4,5,6，7】</param>
        /// <returns></returns>
        private string GetMDT(DateTime dtnow, int type, out string dayWeek)
        {
            DateTime dtnow_New = dtnow.AddDays(type);
            dayWeek = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dtnow_New.DayOfWeek);
            return dtnow_New.Month + "." + dtnow_New.Day;
        }
        #endregion

        #region 获取时间
        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <param name="hours">选中的时间</param>
        private string GetHoursMes()
        {
            string id = Request["id"];
            string date = Request["date"];
            int ID = 0;
            int.TryParse(id, out ID);

            decimal? startHour = 0;
            if (ID != 0 && date != null)
            {
                XCJGUSERTASK modle = PatrolUserTaskBLL.GetXCJGUserTaskByRouteID(ID, DateTime.Parse(date));
                if (modle != null)
                    startHour = modle.STARTHOUR;
            }

            StringBuilder HoursStr = new StringBuilder();
            for (int i = 0; i < 24; i++)
            {
                string value = i.ToString();
                if (i < 10)
                    value = "0" + i.ToString();
                //if (i == 24)
                //    value = "00";
                if (i == startHour)
                {
                    HoursStr.Append("<option value=\"" + i + "\" selected=\"selected\">" + value + "</option>");
                }
                else
                {
                    HoursStr.Append("<option value=\"" + i + "\">" + value + "</option>");
                }

            }
            return HoursStr.ToString();
        }
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="hours">选中的时间</param>
        private string GetEndHoursMes()
        {
            string id = Request["id"];
            string date = Request["date"];
            int ID = 0;
            int.TryParse(id, out ID);

            decimal? endHour = 0;
            if (ID != 0 && date != null)
            {
                XCJGUSERTASK modle = PatrolUserTaskBLL.GetXCJGUserTaskByRouteID(ID, DateTime.Parse(date));
                if (modle != null)
                    endHour = modle.ENDHOUR;
            }

            StringBuilder HoursStr = new StringBuilder();
            for (int i = 0; i < 24; i++)
            {
                string value = i.ToString();
                if (i < 10)
                    value = "0" + i.ToString();
                if (i == 24)
                    value = "00";
                if (i == endHour)
                {
                    HoursStr.Append("<option value=\"" + i + "\" selected=\"selected\">" + value + "</option>");
                }
                else
                {
                    HoursStr.Append("<option value=\"" + i + "\">" + value + "</option>");
                }

            }
            return HoursStr.ToString();
        }
        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="minutes">选中的时间</param>
        private string GetMinutesMes()
        {
            string id = Request["id"];
            string date = Request["date"];
            int ID = 0;
            int.TryParse(id, out ID);

            decimal? startMinute = 0;
            if (ID != 0 && date != null)
            {
                XCJGUSERTASK modle = PatrolUserTaskBLL.GetXCJGUserTaskByRouteID(ID, DateTime.Parse(date));
                if (modle != null)
                    startMinute = modle.STARTMINUTE;
            }

            StringBuilder MinutesStr = new StringBuilder();
            for (int i = 0; i < 60; i++)
            {
                string value = i.ToString();
                if (i < 10)
                    value = "0" + i.ToString();
                if (i == 60)
                    value = "00";
                if (i == startMinute)
                {
                    MinutesStr.Append("<option value=\"" + i + "\" selected=\"selected\">" + value + "</option>");
                }
                else
                {
                    MinutesStr.Append("<option value=\"" + i + "\">" + value + "</option>");
                }
            }
            return MinutesStr.ToString();
        }
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="minutes">选中的时间</param>
        private string GetEndMinutesMes()
        {
            string id = Request["id"];
            string date = Request["date"];
            int ID = 0;
            int.TryParse(id, out ID);


            decimal? endMinute = 0;
            if (ID != 0 && date != null)
            {
                XCJGUSERTASK modle = PatrolUserTaskBLL.GetXCJGUserTaskByRouteID(ID, DateTime.Parse(date));
                if (modle != null)
                    endMinute = modle.ENDMINUTE;
            }

            StringBuilder MinutesStr = new StringBuilder();
            for (int i = 0; i < 60; i++)
            {
                string value = i.ToString();
                if (i < 10)
                    value = "0" + i.ToString();
                if (i == 60)
                    value = "00";
                if (i == endMinute)
                {
                    MinutesStr.Append("<option value=\"" + i + "\" selected=\"selected\">" + value + "</option>");
                }
                else
                {
                    MinutesStr.Append("<option value=\"" + i + "\">" + value + "</option>");
                }
            }
            return MinutesStr.ToString();
        }
        #endregion

        #region 获取所在中队成员

        private string GetWeekStr()
        {
            StringBuilder sbMes = new StringBuilder();
            string date = Request["date"];
            DateTime Date=DateTime.Parse(date);
            if (Date.DayOfWeek.ToString() == "Monday")
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Monday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"checked=\"checked\" />周一</span>");
            }
            else {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Monday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周一</span>");
            }
            if (Date.DayOfWeek.ToString() == "Tuesday")
            {
                sbMes.Append("<span style=\"margin-right:10px\"> <input name=\"weeks\" type=\"checkbox\"  value=\"Tuesday\"  checked=\"checked\"style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />周二 </span>");
            }
            else {
                sbMes.Append("<span style=\"margin-right:10px\"> <input name=\"weeks\" type=\"checkbox\"  value=\"Tuesday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周二 </span>");
            }
            if (Date.DayOfWeek.ToString() == "Wednesday")
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Wednesday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\" />周三</span> ");
            }
            else
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Wednesday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周三 </span>");
            } if (Date.DayOfWeek.ToString() == "Thursday")
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Thursday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\"/>周四</span> ");
            }
            else
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Thursday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周四</span> ");
            } if (Date.DayOfWeek.ToString() == "Friday")
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Friday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\" />周五</span> ");
            }
            else
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Friday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周五 </span>");
            } if (Date.DayOfWeek.ToString() == "Saturday")
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Saturday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\"/>周六</span> ");
            }
            else
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Saturday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周六 </span>");
            } if (Date.DayOfWeek.ToString() == "Sunday")
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Sunday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"checked=\"checked\"/>周日</span> ");
            }
            else
            {
                sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Sunday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周日 </span>");
            }
            return sbMes.ToString();
        }

        /// <summary>
        /// 获取所在中队所有队员
        /// </summary>
        /// <returns></returns>
        private string GetZDMember()
        {
            string strUserID = Request["id"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            decimal ZDID = SessionManager.User.UnitID;
            IList<USER> userList = UserBLL.GetAllUsers().Where(a => a.UNITID == ZDID).ToList();
            StringBuilder ZDMemberStr = new StringBuilder();
            foreach (var item in userList)
            {
                if (userID ==item.USERID)
                {
                    ZDMemberStr.Append("<span style=\"margin-right:10px\"><input id=\"ZDMemberIDs\" name=\"ZDMemberIDs\"  type=\"checkbox\" value=\"" + item.USERID + "\" checked=\"cheked\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />" + item.USERNAME + "</span>");
                }
                else
                {
                    ZDMemberStr.Append("<span style=\"margin-right:10px\"><input id=\"ZDMemberIDs\" name=\"ZDMemberIDs\"  type=\"checkbox\" value=\"" + item.USERID + "\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>" + item.USERNAME + "</span>");
                }
            }
            return ZDMemberStr.ToString();
            //onclick=\"getZDMemberID("+item.USERID+")\"
        }

       
        
        #endregion

        #region 添加任务
        /// <summary>
        /// 添加巡查任务
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUserTask()
        {
            string strUserID = Request["id"];
            decimal parentID = UnitBLL
                .GetParentIDByUnitID(SessionManager.User.UnitID);

            IList<Taizhou.PLE.Model.XCJGAREA> arealist = PatrolAreaBLL.GetPatrolAreas(SessionManager.User.UserID, 1).ToList();
            IList<Taizhou.PLE.Model.XCJGROUTE> routelist = PatrolRouteBLL.GetPatrolRoutes(SessionManager.User.UserID, 1).ToList();
            ViewBag.SSDD = UnitBLL.GetUnitNameByUnitID(parentID);
            ViewBag.unitName = SessionManager.User.UnitName;
            ViewBag.areaID = new SelectList(arealist, "AREAID", "AREANAME");

            ViewBag.routeID = new SelectList(routelist, "ROUTEID", "ROUTENAME");

            List<SelectListItem> areaList = PatrolAreaBLL.GetPatrolAreas(SessionManager.User.UserID, 1).ToList()
              .Select(c => new SelectListItem
              {
                  Text = c.AREANAME,
                  Value = c.AREAID.ToString()
              }).ToList();
            List<SelectListItem> areaListAll = new List<SelectListItem>();
            areaListAll.Add(new SelectListItem() { Text = "请选择", Value = "0", Selected = true });
            if (areaList != null && areaList.Count > 0)

                areaListAll.AddRange(areaList);
            ViewBag.areaList_add = areaListAll;

            List<SelectListItem> routeList = PatrolRouteBLL.GetPatrolRoutes(SessionManager.User.UserID, 1).ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.ROUTENAME,
                    Value = c.ROUTEID.ToString()
                }).ToList();
            List<SelectListItem> routeListALL = new List<SelectListItem>();
            //routeListALL.Add(new SelectListItem() { Text = "请选择", Value = "0", Selected = true });
            if (routeList != null && routeList.Count > 0)
                routeListALL.AddRange(routeList);
            ViewBag.routeList = routeListALL;
            return View(THIS_VIEW_PATH + "AddUserTask.cshtml");
        }
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="userTask"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConmmitAddUserTask(XCJGUSERTASK userTask, List<decimal> ZDMemberIDs, DateTime StartDate, DateTime EndDate, List<string> weeks)
        {

            string strUserID = Request["id"];
            string date = Request["date"];
            int userID = 0;
            int.TryParse(strUserID, out userID);

            decimal parentID = UnitBLL
                    .GetParentIDByUnitID(SessionManager.User.UnitID);
            int day = Math.Abs(((TimeSpan)(EndDate - StartDate)).Days);
            for (int i = 0; i < ZDMemberIDs.Count(); i++)
            {
                for (int j = 0; j < day + 1; j++)
                {
                    bool result = IsAdd(weeks, StartDate.AddDays(j));

                    if (result == true)
                    {
                        XCJGUSERTASK task = PatrolUserTaskBLL
                   .GetXCJGUserTaskByRouteID(ZDMemberIDs[i], StartDate.AddDays(j));
                        if (task == null)
                        {
                            XCJGUSERTASK route = new XCJGUSERTASK
                            {
                                USERID = ZDMemberIDs[i],
                                SSZDID = SessionManager.User.UnitID,
                                SSQJID = 40,
                                TASKDATE = StartDate.AddDays(j),
                                STARTHOUR = userTask.STARTHOUR,
                                STARTMINUTE = userTask.STARTMINUTE,
                                ENDHOUR = userTask.ENDHOUR,
                                ENDMINUTE = userTask.ENDMINUTE,
                                AREAID = userTask.AREAID,
                                ROUTEID = userTask.ROUTEID,
                                JOBCONTENT = userTask.JOBCONTENT,
                                CREATEDTIME=DateTime.Now
                            };

                            PatrolUserTaskBLL.AddUserTask(route);
                        }
                        else
                        {
                            XCJGUSERTASK model = new XCJGUSERTASK
                            {
                                USERID = ZDMemberIDs[i],
                                TASKDATE = StartDate.AddDays(j),
                                SSZDID = userTask.SSZDID,
                                SSQJID = 40,
                                STARTHOUR = userTask.STARTHOUR,
                                STARTMINUTE = userTask.STARTMINUTE,
                                ENDHOUR = userTask.ENDHOUR,
                                ENDMINUTE = userTask.ENDMINUTE,
                                AREAID = userTask.AREAID,
                                ROUTEID = userTask.ROUTEID,
                                JOBCONTENT = userTask.JOBCONTENT
                            };
                            PatrolUserTaskBLL.ModifyUserTask(model);
                        
                        }
                    }
                }
               
            }
           
            return RedirectToAction("Index");
        }

        public bool IsAdd(List<string> weeks,DateTime time) 
        {
            bool result = false;
            if (weeks != null)
            {
                foreach (string item in weeks)
                {
                    if (time.DayOfWeek.ToString()==item) {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region 修改任务
        public ActionResult EditUserTask()
        {
            string strUserID = Request["id"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            string date = Request["date"];
            string type = Request["type"];
            ViewBag.ZDMemberStr = GetZDMember();
            XCJGUSERTASK task = PatrolUserTaskBLL
                .GetXCJGUserTaskByRouteID(userID, DateTime.Parse(date));

            decimal areaId = Convert.ToDecimal(task.AREAID);
            XCJGAREA areaModel = PatrolAreaBLL.GetXCJGAreaByAreaID(areaId);
            if (areaModel != null)
            {
                ViewBag.areaMap = areaModel.GEOMETRY;
            }

            decimal routeId = Convert.ToDecimal(task.ROUTEID);
            XCJGROUTE routeModel = PatrolRouteBLL.GetXCJGRouteByRouteID(routeId);
            if (routeModel != null)
            {
                ViewBag.routeMap = routeModel.GEOMETRY;
            }

            XCJGUSERTASK userTask = new XCJGUSERTASK
            {
                USERID = userID,
                TASKDATE = DateTime.Parse(date),
                SSZDID = task.SSZDID,
                SSQJID = task.SSQJID,
                STARTHOUR = task.STARTHOUR,
                STARTMINUTE = task.STARTMINUTE,
                ENDHOUR = task.ENDHOUR,
                ENDMINUTE = task.ENDMINUTE,
                AREAID = task.AREAID,
                ROUTEID = task.ROUTEID,
                JOBCONTENT = task.JOBCONTENT
                
            };
            //获取所有的单位
            //List<SelectListItem> unitList = UnitBLL.GetAllUnits().ToList()
            //    .Select(c => new SelectListItem
            //    {
            //        Text = c.UNITNAME,
            //        Value = c.UNITID.ToString()
            //    }).ToList();

            //ViewBag.unitList = unitList;

            //获得大队名称
            string unitList = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == userTask.SSQJID).UNITNAME;
            ViewBag.unitList = unitList;

            //获得中队名称
            string unitName = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == userTask.SSZDID).UNITNAME;
            ViewBag.unitName = unitName;

            //巡查路线
            List<SelectListItem> areaList = PatrolAreaBLL.GetPatrolAreas(SessionManager.User.UserID, 1).ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.AREANAME,
                    Value = c.AREAID.ToString()
                }).ToList();
            ViewBag.areaList = areaList;

            //巡查区域
            List<SelectListItem> routeList = PatrolRouteBLL.GetPatrolRoutes(SessionManager.User.UserID, 1).ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.ROUTENAME,
                    Value = c.ROUTEID.ToString()
                }).ToList();
            ViewBag.routeList = routeList;
            List<SelectListItem> DAIDList = UnitBLL.GetAllUnits().Where(a => a.UNITTYPEID == 4).ToList()
            .Select(c => new SelectListItem
            {
                Text = c.UNITNAME,
                Value = c.UNITID.ToString()
            }).ToList();

            ViewBag.DAIDList = DAIDList;



            return View(THIS_VIEW_PATH + "EditUserTask.cshtml", userTask);
        }

        /// <summary>
        /// 提交修改任务
        /// </summary>
        [HttpPost]
        public ActionResult CommitEditUserTask(XCJGUSERTASK userTask)
        {
            string strUserID = Request["id"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            string date = Request["date"];


            XCJGUSERTASK task = new XCJGUSERTASK
            {
                USERID = userID,
                TASKDATE = DateTime.Parse(date),
                SSZDID = userTask.SSZDID,
                SSQJID = 40,
                STARTHOUR = userTask.STARTHOUR,
                STARTMINUTE = userTask.STARTMINUTE,
                ENDHOUR = userTask.ENDHOUR,
                ENDMINUTE = userTask.ENDMINUTE,
                AREAID = userTask.AREAID,
                ROUTEID = userTask.ROUTEID,
                JOBCONTENT = userTask.JOBCONTENT
            };

            PatrolUserTaskBLL.ModifyUserTask(task);

            return RedirectToAction("Index");
        }
        #endregion

        #region 删除任务
        /// <summary>
        /// 删除任务
        /// </summary>
        public bool DeleteUserTask()
        {
            string strUserID = Request["id"];
            decimal userID = 0;
            string date = Request["date"];
            if (decimal.TryParse(strUserID, out userID))
            {
                PatrolUserTaskBLL.DeleteUserTask(userID, DateTime.Parse(date));
                return true;
            }

            return false;
        }
        #endregion

        public string getGEOMETRY()
        {
            string AREAID = Request.QueryString["AREAID"];
            decimal aresId = 0;
            decimal.TryParse(AREAID, out aresId);
            XCJGAREA model = PatrolAreaBLL.GetXCJGAreaByAreaID(aresId);
            string GEOMETRY = null;
            if (model != null)
            {
                GEOMETRY = model.GEOMETRY;
            }
            return GEOMETRY;
        }

        public string getRouteGEOMETRY()
        {
            string ROUTEID = Request.QueryString["ROUTEID"];
            decimal routeId = 0;
            decimal.TryParse(ROUTEID, out routeId);
            XCJGROUTE model = PatrolRouteBLL.GetXCJGRouteByRouteID(routeId);
            string GEOMETRY = null;
            if (model != null)
            {
                GEOMETRY = model.GEOMETRY;
            }
            return GEOMETRY;
        }

        public ActionResult GetGeometryByAreaID()
        {
            return View(THIS_VIEW_PATH + "GetGeometryByAreaID.cshtml");
        }

    }
}
