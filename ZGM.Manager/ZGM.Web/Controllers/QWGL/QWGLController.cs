using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.GroupBLL;
using ZGM.BLL.QWGLBLLs;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.Model;
using ZGM.Model.WeeksModels;

namespace ZGM.Web.Controllers.QWGL
{
    public class QWGLController : Controller
    {
        //
        // GET: /QWGL/
        public string getZDMember()
        {
            string unitID = Request["unitID"];
            decimal GroupID = decimal.Parse(Request["GroupID"]);
            return GetZDMember(unitID, GroupID);
        }
        public string getMap()
        {
            return getGEOMETRY();
        }
        public string getWeek()
        {
            return GetWeekStr();
        }
        public string GetQYWZMap()
        {
            return GetQYWZMapXY();
        }
        public ActionResult TeamTask()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string zdid = Request["SSZDID"];

            decimal zdId = 0;
            decimal.TryParse(zdid, out zdId);


            DateTime dtNow = DateTime.Now;//获取当天时间
            DateTime.TryParse(Request["t"], out dtNow);
            if (dtNow == Convert.ToDateTime("0001/1/1 0:00:00"))
                dtNow = DateTime.Now;

            ViewBag.urlUpWeek = "/QWGL/TeamTask?t=" + dtNow.AddDays(-7).ToString("yyyy-MM-dd");
            ViewBag.urlNextWeek = "/QWGL/TeamTask?t=" + dtNow.AddDays(7).ToString("yyyy-MM-dd");

            ViewBag.GetTableOneMes = GetTableOneMes(dtNow);//表格日期
            ViewBag.GetTableContentMes = GetTableContentMes(dtNow, zdId, 0);//获得表格内容
            ViewBag.zdId = zdId;

            List<SelectListItem> questionDLlist = UnitBLL.GetAllUnitsByUnitTypeID(3).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.UNITNAME.ToString(),
                   Value = c.UNITID.ToString()
               }).ToList();
            //小组列表
            List<SelectListItem> userGroupList = new GroupBLL().GetMajorProjectsLists()
                 .ToList().Select(c => new SelectListItem
                 {
                     Text = c.NAME,
                     Value = c.ID.ToString()
                 }).ToList();
            userGroupList.Insert(0, new SelectListItem
            {
                Text = "请选择分组",
                Value = ""
            });
            ViewBag.QuestionDL = questionDLlist;
            ViewBag.userGroupList = userGroupList;
            return View();
        }

        public string GetTableContentMesAjax()
        {
            //string ZDID = Request["SSZDID"];
            DateTime dtNow = DateTime.Now;//获取当天时间
            DateTime.TryParse(Request["time"], out dtNow);
            if (dtNow == Convert.ToDateTime("0001/1/1 0:00:00"))
                dtNow = DateTime.Now;

            decimal ZDID = 0;
            decimal.TryParse(Request["SSZDID"], out ZDID);
            decimal GroupID = 0;
            decimal.TryParse(Request["GroupID"], out GroupID);
            return GetTableContentMes(dtNow, ZDID, GroupID);
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="dtNow">时间</param>
        /// <returns></returns>
        public string GetTableContentMes(DateTime dtNow, decimal ZDID, decimal GroupID)
        {
            if (ZDID == 0)
                ZDID = SessionManager.User.UnitID;
            StringBuilder sbMes = new StringBuilder();
            List<SYS_USERS> userList = new List<SYS_USERS>();
            if (GroupID == 0)
                userList = UserBLL.GetAllUsers().Where(a => a.UNITID == ZDID).ToList();
            else
                userList = UserBLL.GetAllUsers().Where(a => a.UNITID == ZDID && a.GROUPID == GroupID).ToList();
            if (ZDID == SessionManager.User.UnitID)
            {
                #region 拼接日历视图
                if (userList != null && userList.Count() > 0)
                {
                    //获取要显示的周期
                    int startIndex = 0;
                    int endIndex = 0;
                    GetWeekTools.GetStartEndIndex(dtNow, ref startIndex, ref endIndex);
                    IList<QWGL_USERTASKS> UserTaskList = PatrolUserTaskBLL.GetXCJGUserTasks().ToList();

                    foreach (var item in userList)
                    {
                        sbMes.Append("<tr>");
                        sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse; line-height:60px; background:#f2f5f7\">" + item.USERNAME + "</td>");
                        for (int i = startIndex; i < endIndex; i++)//遍历对应时间的勤务
                        {

                            DateTime dtnow_New = dtNow.AddDays(i);

                            DateTime dtOne = dtnow_New.Date.Date;
                            DateTime dtTwo = dtOne.AddDays(1);

                            IList<QWGL_USERTASKS> UserTaskList_Where = UserTaskList
                                .Where(a => a.USERID == item.USERID && a.SDATE >= dtOne && a.SDATE < dtTwo).ToList();

                            string onlyT = item.USERID + "_" + dtnow_New.ToString("yyyyMMdd");

                            if (UserTaskList_Where != null && UserTaskList_Where.Count() > 0)
                            {
                                sbMes.Append("<td  id=\"td_" + onlyT + "\"   style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#F0AD4E\" ><img src='/Images/images/normal.png' style='width:30px; height:30px; cursor:pointer;' title='点我修改队员任务' onclick=\"EditUserTask('" + item.USERID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "','" + item.UNITID + "','" + GroupID + "')\"/></td>");
                            }
                            else
                            {
                                sbMes.Append("<td id=\"td_" + onlyT + "\"  style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\" ><img src='/Images/images/abnormal.png' style='width:30px; height:30px; cursor:pointer;' title='点我添加队员任务' onclick=\"AddUserTask('" + item.USERID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "','" + item.UNITID + "','" + GroupID + "')\" /></td>");
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
                    GetWeekTools.GetStartEndIndex(dtNow, ref startIndex, ref endIndex);
                    IList<QWGL_USERTASKS> UserTaskList = PatrolUserTaskBLL.GetXCJGUserTasks().ToList();

                    foreach (var item in userList)
                    {
                        sbMes.Append("<tr>");
                        sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse; line-height:60px; background:#f2f5f7\">" + item.USERNAME + "</td>");
                        for (int i = startIndex; i < endIndex; i++)//遍历对应时间的勤务
                        {

                            DateTime dtnow_New = dtNow.AddDays(i);

                            DateTime dtOne = dtnow_New.Date.Date;
                            DateTime dtTwo = dtOne.AddDays(1);

                            IList<QWGL_USERTASKS> UserTaskList_Where = UserTaskList
                                .Where(a => a.USERID == item.USERID && a.SDATE >= dtOne && a.SDATE < dtTwo).ToList();

                            string onlyT = item.USERID + "_" + dtnow_New.ToString("yyyyMMdd");

                            if (UserTaskList_Where != null && UserTaskList_Where.Count() > 0)
                            {
                                sbMes.Append("<td  id=\"td_" + onlyT + "\"   style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#F0AD4E\" ><img src='/Images/images/normal.png' style='width:30px; height:30px; cursor:pointer;' title='点我修改队员任务' onclick=\"EditUserTask('" + item.USERID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "','" + item.UNITID + "','" + GroupID + "')\"/></td>");
                            }
                            else
                            {
                                sbMes.Append("<td id=\"td_" + onlyT + "\"  style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\" ><img src='/Images/images/abnormal.png' style='width:30px; height:30px; cursor:pointer;' title='点我添加队员任务' onclick=\"AddUserTask('" + item.USERID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "','" + item.UNITID + "','" + GroupID + "')\" /></td>");
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
            GetWeekTools.GetStartEndIndex(dtNow, ref startIndex, ref endIndex);

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

        /// <summary>
        /// 队员巡查任务管理页面AddUserTask
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUserTask()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string strUserID = Request["id"];
            ViewBag.groupid = Request["groupid"];
            ViewBag.datetime = Request["date"];
            decimal UnitId = decimal.Parse(UserBLL.GetUnitID(decimal.Parse(strUserID)).ToString());
            decimal parentID = UnitBLL.GetParentIDByUnitID(UnitId);
            ViewBag.UnitId = UnitId;
            IList<QWGL_AREAS> arealist = PatrolAreaBLL.GetPatrolAreas(1).Where(t => t.STATE == 1).ToList();
            ViewBag.SSDD = UnitBLL.GetUnitNameByUnitID(parentID);
            ViewBag.unitName = UnitBLL.GetUnitNameByUnitID(UnitId); //SessionManager.User.UnitName;
            ViewBag.areaID = new SelectList(arealist, "AREAID", "AREANAME");
            List<QWGL_AREAS> areaList = arealist
             .Select(c => new QWGL_AREAS
             {
                 AREANAME = c.AREANAME,
                 AREAID = c.AREAID,
                 GEOMETRY = c.GEOMETRY
             }).ToList();
            if (areaList != null && areaList.Count > 0)
            {
                ViewBag.areaList_add = areaList;
            }
            else
            {
                List<QWGL_AREAS> areaListone = new List<QWGL_AREAS>();
                QWGL_AREAS QWGLA = new QWGL_AREAS()
                {
                    AREAID = 0,
                    AREANAME = "暂无巡查区域!"
                };
                areaListone.Add(QWGLA);
                ViewBag.areaList_add = areaListone;
            }
            ViewBag.userid = strUserID;
            //小组列表
            List<SelectListItem> userGroupList = new GroupBLL().GetMajorProjectsLists()
                 .ToList().Select(c => new SelectListItem
                 {
                     Text = c.NAME,
                     Value = c.ID.ToString()
                 }).ToList();
            userGroupList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = "0"
            });
            ViewBag.userGroupList = userGroupList;

            List<QWGL_SIGNINAREAS> list_signin = AreaBLL.GetSearchSignInArea(null);

            ViewBag.list_signin = list_signin;

            return View();
        }
        /// <summary>
        /// 获取所在中队所有队员
        /// </summary>
        /// <returns></returns>
        private string GetZDMember(string unitID, decimal GroupID)
        {
            string isEdit = Request["isEdit"];
            bool flag = false;
            if (!string.IsNullOrEmpty(isEdit))
            {
                flag = true;
            }
            string strUserID = Request["id"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            int ZDID = 0;
            int.TryParse(unitID, out ZDID);
            List<SYS_USERS> userList = userList = UserBLL.GetAllUsers().Where(a => a.UNITID == ZDID).ToList();
            List<SYS_USERS> list = new List<SYS_USERS>();
            if (GroupID != 0)
                list = UserBLL.GetAllUsers().Where(a => a.UNITID == ZDID && a.GROUPID == GroupID).ToList();
            string ids = "";
            StringBuilder ZDMemberStr = new StringBuilder();
            if (flag)
            {
                foreach (var item in userList)
                {
                    if (userID == item.USERID)
                    {

                        ZDMemberStr.Append("<span style=\"margin-right:10px\"><input  name=\"ZDMemberIDs\"  type=\"hidden\" value=\"" + item.USERID + "\" checked=\"cheked\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />" + item.USERNAME + "</span>");

                    }
                    else
                    {

                        ZDMemberStr.Append("<span style=\"margin-right:10px;display:none;\"><input  name=\"ZDMemberIDs\"  type=\"hidden\" value=\"" + item.USERID + "\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>" + item.USERNAME + "</span>");

                    }
                }
            }
            else
            {
                foreach (var item in userList)
                {
                    if (GroupID == 0)
                    {

                        ZDMemberStr.Append("<span style=\"margin-right:10px\"><input  name=\"ZDMemberIDs\"  type=\"checkbox\" value=\"" + item.USERID + "\" checked=\"cheked\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />" + item.USERNAME + "</span>");

                    }
                    else
                    {
                        foreach (var ite in list)
                        {
                            if (item.USERID == ite.USERID)
                            {

                                ZDMemberStr.Append("<span style=\"margin-right:10px\"><input  name=\"ZDMemberIDs\"  type=\"checkbox\" value=\"" + item.USERID + "\" checked=\"cheked\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />" + item.USERNAME + "</span>");
                            }
                        }
                    }
                }
            }


            return ZDMemberStr.ToString();
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        public void ConmmitAddUserTask(QWGL_USERTASKS userTask, List<decimal> ZDMemberIDs, DateTime StartDate, DateTime EndDate, List<string> weeks)
        {

            string signin = Request["hid_Signin"];

            string array = Request["AreaAddressArray"];
            string[] arraylist = array.Split(',');
            string strUserID = Request["id"];
            string date = Request["date"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            decimal? userid = 0;

            int ENDHOUR = EndDate.Hour;
            int ENDMINUTE = EndDate.Minute;

            if (SessionManager.User.UserID != 0)
            {
                userid = SessionManager.User.UserID;
            }

            int day = Math.Abs(((TimeSpan)(EndDate - StartDate)).Days);
            for (int i = 0; i < ZDMemberIDs.Count(); i++)
            {
                for (int j = 0; j < day + 1; j++)
                {
                    bool result = IsAdd(weeks, StartDate.AddDays(j));
                    if (result == true)
                    {
                        QWGL_USERTASKS task = PatrolUserTaskBLL
                       .GetQWGLUserTaskByRouteID(ZDMemberIDs[i], StartDate.AddDays(j));

                        if (task == null)
                        {
                            QWGL_USERTASKS route = new QWGL_USERTASKS
                            {
                                USERTASKID = PatrolUserTaskBLL.GetNewUSERTASKID(),
                                USERID = ZDMemberIDs[i],
                                // CRETEUSERID = SessionManager.User.UnitID,
                                SDATE = StartDate.AddDays(j),
                                //EDATE = EndDate,
                                EDATE = StartDate.AddDays(j).Date.AddHours(ENDHOUR).AddMinutes(ENDMINUTE),
                                TASKCONTENT = userTask.TASKCONTENT,
                                CREATEDTIME = DateTime.Now,
                                CRETEUSERID = SessionManager.User.UserID
                            };
                            PatrolUserTaskBLL.AddUserTask(route);
                            if (arraylist.Length > 0 && !string.IsNullOrEmpty(arraylist[0]))
                            {
                                for (int h = 0; h < arraylist.Length; h++)
                                {

                                    QWGL_USERTASKAREARS AREATASKRS = new QWGL_USERTASKAREARS
                                    {
                                        USERTASKID = route.USERTASKID,
                                        AREAID = Convert.ToDecimal(arraylist[h])
                                    };
                                    PatrolUserTaskBLL.AddQWGL_AREATASKRS(AREATASKRS);
                                }
                            }
                            if (!string.IsNullOrEmpty(signin))
                            {
                                decimal? areid = decimal.Parse(signin);
                                QWGL_USERSIGNINTASKS usertask = new QWGL_USERSIGNINTASKS
                                {
                                    AREAID = areid,
                                    USERTASKID = route.USERTASKID,
                                    USERID = route.USERID,
                                    SIGNINDAY = StartDate.AddDays(j).Date,
                                    CREATETIME = DateTime.Now
                                };
                                PatrolUserTaskBLL.AddQWGL_USERSIGNINTASKS(usertask);
                            }
                        }
                    }
                }
            }
            //Response.Write("<script>alert('任务派遣成功！');window.location.href='/QWGL/TeamTask'</script>");
            Response.Write("<script>window.location.href='/QWGL/TeamTask/?flag=1&&t=" + Request["datetime"] + "&&SSZDID=" + Request["unitID"] + "'</script>");
        }

        public bool IsAdd(List<string> weeks, DateTime time)
        {
            bool result = false;
            if (weeks != null)
            {
                foreach (string item in weeks)
                {
                    if (time.DayOfWeek.ToString() == item)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 队员巡查任务管理页面EditUserTask
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUserTask()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string strUserID = Request["id"];
            ViewBag.datetime = Request["date"];
            ViewBag.groupid = Request["groupid"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            decimal UnitId = decimal.Parse(UserBLL.GetUnitID(decimal.Parse(strUserID)).ToString());
            ViewBag.UnitId = UnitId;
            ViewBag.unitName = UnitBLL.GetUnitNameByUnitID(UnitId);
            //获取服务器时间
            string serviceTime = DateTime.Now.ToString("yyyy-MM-dd");
            string date = Request["date"];
            string type = Request["type"];

            //如果大于等于当前服务器时间
            if (Convert.ToDateTime(date) >= Convert.ToDateTime(serviceTime))
            {
                ViewBag.trueUpdate = "2";
            }
            else
            {
                ViewBag.trueUpdate = "1";
            }

            //获取所有的签到区域
            List<QWGL_SIGNINAREAS> list_signin = AreaBLL.GetSearchSignInArea(null);
            ViewBag.list_signin = list_signin;

            QWGL_USERSIGNINTASKS areasg_model = PatrolUserTaskBLL.GetSGIDAreaid(userID, Convert.ToDateTime(date).Date);
            if (areasg_model != null)
            {
                ViewBag.areasgid = areasg_model.AREAID;
                ViewBag.GEOMETRY = areasg_model.QWGL_SIGNINAREAS.GEOMETRY;
            }


            QWGL_USERTASKS task = PatrolUserTaskBLL
                     .GetQWGLUserTaskByRouteID(userID, DateTime.Parse(date));
            // string AREAIDCount = null;
            //获取多巡查区域任务表数据
            //List<QWGL_USERTASKS> listone = PatrolUserTaskBLL.GetQWGL_AREATASKRS(Convert.ToDecimal(userID), DateTime.Parse(date));
            QWGL_USERTASKS listone = PatrolUserTaskBLL.GetQWGL_AREATASKRS(Convert.ToDecimal(userID), DateTime.Parse(date));
            if (listone != null)
            {
                ViewBag.listoneid = listone.QWGL_USERTASKAREARS.FirstOrDefault().QWGL_AREAS.AREAID;
                ViewBag.listoneGEOMETRY = listone.QWGL_USERTASKAREARS.FirstOrDefault().QWGL_AREAS.GEOMETRY;
            }

            //foreach (QWGL_USERTASKS xc in listone)
            //{
            //    decimal? AREAID = xc.USERTASKID;
            //    AREAIDCount += AREAID.ToString() + ",";
            //}
            //  ViewBag.minAREAID = listone.Min<QWGL_USERTASKS>(c => c.USERTASKID);
            // ViewBag.AREAIDCount = AREAIDCount;
            //string AREAIDCount1 = null;
            //if (task != null)
            //{
            //    List<QWGL_USERTASKAREARS> listtwo = PatrolUserTaskBLL.GetQWGLUserTaskByAREAID(task.USERTASKID);
            //    foreach (QWGL_USERTASKAREARS xc in listtwo)
            //    {
            //        decimal? AREAID = xc.AREAID;
            //        AREAIDCount1 += AREAID.ToString() + ",";
            //    }
            //}


            //ViewBag.AREAIDCount1 = AREAIDCount1;
            //decimal areaId = Convert.ToDecimal(task.USERTASKID);
            //ViewBag.areaId = areaId;
            // 根据区域标识获取区域对象
            //QWGL_AREAS areaModel = PatrolAreaBLL.GetQWGLAreaByAreaID(areaId);
            //if (areaModel != null)
            //{
            //    ViewBag.areaMap = areaModel.GEOMETRY;
            //}
            QWGL_USERTASKS QWGLUserTask = new QWGL_USERTASKS
            {
                USERID = userID,
                SDATE = task.SDATE,
                EDATE = task.EDATE,
                TASKCONTENT = task.TASKCONTENT,
            };
            //巡查区域
            List<QWGL_AREAS> areaList = PatrolAreaBLL.GetPatrolAreas(1).ToList().Select(c => new
           QWGL_AREAS
            {
                AREANAME = c.AREANAME,
                AREAID = c.AREAID,
                GEOMETRY = c.GEOMETRY
            }).ToList();

            if (areaList != null && areaList.Count > 0)
            {
                ViewBag.areaList = areaList; ;
            }
            else
            {
                List<QWGL_AREAS> areaListone = new List<QWGL_AREAS>();
                QWGL_AREAS QWGLA = new QWGL_AREAS()
                {
                    AREAID = 0,
                    AREANAME = "暂无巡查区域!"
                };
                areaListone.Add(QWGLA);
                ViewBag.areaList = areaListone;
            }




            return View(QWGLUserTask);
        }
        /// <summary>
        /// 提交修改任务
        /// </summary>
        /// <param name="userTask"></param>
        /// <param name="ZDMemberIDs"></param>
        /// <param name="weeks"></param>
        [HttpPost]
        public void CommitEditUserTask(QWGL_USERTASKS userTask, List<decimal> ZDMemberIDs, List<string> weeks)
        {
            string signin = Request["hid_Signin"];

            DateTime StartDate = Convert.ToDateTime(Request["StrTime"]);
            DateTime EndDate = Convert.ToDateTime(Request["EdTime"]);
            string strUserID = Request["id"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            string date = Request["date"];
            string array = Request["AreaAddressArray"];
            string[] arraylist = array.Split(',');

            QWGL_USERTASKS listone = PatrolUserTaskBLL.GetQWGL_AREATASKRS((decimal)userID, DateTime.Parse(date));
            //  foreach (QWGL_USERTASKS xc in listone)
            // {
            string id = listone.USERTASKID.ToString();
            //删除巡查区域表数据
            PatrolUserTaskBLL.DeleteQWGL_AREATASKRS(id);
            // }


            int day = Math.Abs(((TimeSpan)(EndDate - StartDate)).Days);
            for (int i = 0; i < ZDMemberIDs.Count(); i++)
            {
                for (int j = 0; j < day + 1; j++)
                {

                    QWGL_USERTASKS task = PatrolUserTaskBLL
                   .GetQWGLUserTaskByRouteID(ZDMemberIDs[i], StartDate.AddDays(j));

                    if (task != null && task.USERID == userID)
                    {
                        QWGL_USERTASKS tas = new QWGL_USERTASKS
                        {
                            USERTASKID = task.USERTASKID,
                            USERID = userID,
                            SDATE = StartDate,
                            EDATE = EndDate,
                            TASKCONTENT = userTask.TASKCONTENT,
                            CREATEDTIME = DateTime.Now,
                            CRETEUSERID = SessionManager.User.UserID
                        };
                        PatrolUserTaskBLL.ModifyUserTask(tas);

                        if (!string.IsNullOrEmpty(signin))
                        {
                            decimal? areid = decimal.Parse(signin);
                            QWGL_USERSIGNINTASKS usersignintask = new QWGL_USERSIGNINTASKS
                            {
                                AREAID = areid,
                                USERTASKID = tas.USERTASKID,
                                CREATETIME = DateTime.Now
                            };
                            PatrolUserTaskBLL.ModifyUserSIGNINTask(usersignintask);
                        }



                        if (arraylist.Length > 0 && !string.IsNullOrEmpty(arraylist[0]))
                        {
                            for (int h = 0; h < arraylist.Length; h++)
                            {
                                //获取随机数生成ATRID主键
                                Random r = new Random();
                                int number = r.Next(10000, 99999);
                                string time = DateTime.Now.ToString("yyyyMMddHHmmssfff") + number;
                                string ATRID = time;
                                QWGL_USERTASKAREARS AREATASKRS = new QWGL_USERTASKAREARS
                                {
                                    USERTASKID = tas.USERTASKID,
                                    AREAID = Convert.ToDecimal(arraylist[h])
                                };
                                PatrolUserTaskBLL.AddQWGL_AREATASKRS(AREATASKRS);
                            }
                        }

                    }

                }
            }
            //Response.Write("<script>window.location.href='/QWGL/TeamTask/?flag=2'</script>");
            Response.Write("<script>window.location.href='/QWGL/TeamTask/?flag=2&&t=" + Request["datetime"] + "&&SSZDID=" + Request["unitID"] + "'</script>");

        }

        //获取星期
        private string GetWeekStr()
        {
            string isEdit = Request["isEdit"];
            bool flag = false;
            if (!string.IsNullOrEmpty(isEdit))
            {
                flag = true;
            }
            StringBuilder sbMes = new StringBuilder();
            string date = Request["date"];
            DateTime Date = DateTime.Parse(date);
            if (flag)//是修改，则人员多选框不可修改
            {
                if (Date.DayOfWeek.ToString() == "Monday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Monday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"checked=\"checked\" />周一</span>");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"hidden\"  value=\"Monday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周一</span>");
                }
                if (Date.DayOfWeek.ToString() == "Tuesday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"> <input name=\"weeks\" type=\"checkbox\"  value=\"Tuesday\"  checked=\"checked\"style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />周二 </span>");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"> <input name=\"weeks\" type=\"hidden\"  value=\"Tuesday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周二 </span>");
                }
                if (Date.DayOfWeek.ToString() == "Wednesday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Wednesday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\" />周三</span> ");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"hidden\"  value=\"Wednesday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周三 </span>");
                } if (Date.DayOfWeek.ToString() == "Thursday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Thursday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\"/>周四</span> ");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"hidden\"  value=\"Thursday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周四</span> ");
                } if (Date.DayOfWeek.ToString() == "Friday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Friday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\" />周五</span> ");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"hidden\"  value=\"Friday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周五 </span>");
                } if (Date.DayOfWeek.ToString() == "Saturday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Saturday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" checked=\"checked\"/>周六</span> ");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"hidden\"  value=\"Saturday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周六 </span>");
                } if (Date.DayOfWeek.ToString() == "Sunday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Sunday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"checked=\"checked\"/>周日</span> ");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"hidden\"  value=\"Sunday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周日 </span>");
                }
            }
            else
            {
                if (Date.DayOfWeek.ToString() == "Monday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Monday\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"checked=\"checked\" />周一</span>");
                }
                else
                {
                    sbMes.Append("<span style=\"margin-right:10px\"><input name=\"weeks\" type=\"checkbox\"  value=\"Monday\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>周一</span>");
                }
                if (Date.DayOfWeek.ToString() == "Tuesday")
                {
                    sbMes.Append("<span style=\"margin-right:10px\"> <input name=\"weeks\" type=\"checkbox\"  value=\"Tuesday\"  checked=\"checked\"style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />周二 </span>");
                }
                else
                {
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
            }



            return sbMes.ToString();
        }

        private string GetQYWZMapXY()
        {
            string AREAID = Request.QueryString["AREAID"];
            decimal aresId = 0;
            decimal.TryParse(AREAID, out aresId);
            QWGL_AREAS model = PatrolAreaBLL.GetQWGLAreaByAreaID(aresId);
            string GEOMETRY = null;
            if (model != null)
            {
                GEOMETRY = model.GEOMETRY;
            }
            return GEOMETRY;
        }
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
                QWGL_USERTASKS listone = PatrolUserTaskBLL.GetQWGL_AREATASKRS((decimal)userID, DateTime.Parse(date));
                // foreach (QWGL_USERTASKS xc in listone)
                // {
                string id = listone.USERTASKID.ToString();
                //删除巡查区域表数据
                PatrolUserTaskBLL.DeleteQWGL_AREATASKRS(id);
                // }
                PatrolUserTaskBLL.DeleteUserSIGNINTask(userID, DateTime.Parse(date).Date);
                PatrolUserTaskBLL.DeleteUserTask(userID, DateTime.Parse(date));

                return true;
            }

            return false;
        }
        /// <summary>
        /// 地图
        /// </summary>
        /// <returns></returns>
        public string getGEOMETRY()
        {
            string AREAID = Request.QueryString["AREAID"];
            string[] ids = AREAID.Split(',');
            decimal aresId = 0;
            if (!string.IsNullOrEmpty(ids[0]))
            {
                decimal.TryParse(ids[0], out aresId);
            }

            QWGL_AREAS model = PatrolAreaBLL.GetQWGLAreaByAreaID(aresId);
            string GEOMETRY = null;
            if (model != null)
            {
                GEOMETRY = model.GEOMETRY;
            }
            return GEOMETRY;
        }
    }
}
