using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.CarsBLL;
using ZGM.BLL.QWGLBLLs;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.Model;

namespace ZGM.Web.Controllers.QWGL
{
    public class QWGLCarController : Controller
    {
        //
        // GET: /QWGLCar/
        public string getMap()
        {
            return getGEOMETRY();
        }
        public string getZDMember()
        {
            return GetZDMember();
        }
        public string getWeek()
        {
            return GetWeekStr();
        }
        public string GetQYWZMap()
        {
            return GetQYWZMapXY();
        }
        public ActionResult VehicleTask()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string zdid = Request["SSZDID"];
            decimal zdId = 0;
            decimal.TryParse(zdid, out zdId);


            DateTime dtNow = DateTime.Now;//获取当天时间
            DateTime.TryParse(Request["t"], out dtNow);
            if (dtNow == Convert.ToDateTime("0001/1/1 0:00:00"))
                dtNow = DateTime.Now;

            ViewBag.urlUpWeek = "/QWGLCar/VehicleTask?t=" + dtNow.AddDays(-7).ToString("yyyy-MM-dd");
            ViewBag.urlNextWeek = "/QWGLCar/VehicleTask?t=" + dtNow.AddDays(7).ToString("yyyy-MM-dd");

            ViewBag.GetTableOneMes = GetTableOneMes(dtNow);//表格日期
            ViewBag.GetTableContentMes = GetTableContentMes(dtNow, zdId);//获得表格内容


            List<SelectListItem> questionDLlist = UnitBLL.GetAllUnitsByUnitTypeID(3).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.UNITNAME.ToString(),
                   Value = c.UNITID.ToString()
               }).ToList();

            ViewBag.QuestionDL = questionDLlist;

            return View();
        }
        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="dtNow">时间</param>
        /// <returns></returns>
        public string GetTableContentMes(DateTime dtNow, decimal ZDID)
        {
            //if (ZDID == 0)
            //    ZDID = SessionManager.User.UnitID;
            StringBuilder sbMes = new StringBuilder();
            IList<QWGL_CARS> carsList = QWGL_CARSBLL.GetAllCars().ToList();

            //if (ZDID == SessionManager.User.UnitID)
            //{
                #region 拼接日历视图
                if (carsList != null && carsList.Count() > 0)
                {
                    //获取要显示的周期
                    int startIndex = 0;
                    int endIndex = 0;
                    GetWeekTools.GetStartEndIndex(dtNow, ref startIndex, ref endIndex);
                    IList<QWGL_CARTASKS> UserTaskList = PatrolCarTaskBLL.GetXCJGCarTasks().ToList();

                    foreach (var item in carsList)
                    {
                        sbMes.Append("<tr>");
                        sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse; line-height:60px; background:#f2f5f7\">" + item.CARNUMBER + "</td>");
                        for (int i = startIndex; i < endIndex; i++)//遍历对应时间的勤务
                        {

                            DateTime dtnow_New = dtNow.AddDays(i);

                            DateTime dtOne = dtnow_New.Date.Date;
                            DateTime dtTwo = dtOne.AddDays(1);

                            IList<QWGL_CARTASKS> UserTaskList_Where = UserTaskList
                                .Where(a => a.CARID == item.CARID && a.SDATE >= dtOne && a.SDATE < dtTwo).ToList();

                            string onlyT = item.CARID + "_" + dtnow_New.ToString("yyyyMMdd");

                            if (UserTaskList_Where != null && UserTaskList_Where.Count() > 0)
                            {
                                sbMes.Append("<td  id=\"td_" + onlyT + "\"   style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#F0AD4E\" ><img src='/Images/images/normal.png' style='width:30px; height:30px; cursor:pointer;' title='点我修改车辆任务' onclick=\"EditUserTask('" + item.CARID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "')\"/></td>");
                            }
                            else
                            {
                                sbMes.Append("<td id=\"td_" + onlyT + "\"  style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\" ><img src='/Images/images/abnormal.png' style='width:30px; height:30px; cursor:pointer;' title='点我添加车辆任务' onclick=\"AddUserTask('" + item.CARID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "')\" /></td>");
                            }
                        }
                        sbMes.Append("</tr>");
                    }
                }
                else
                {
                    sbMes.Append("<tr>");
                    sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse; color:red;\" colspan=\"8\">没有查询到车辆</td>");
                    sbMes.Append("</tr>");
                }
                #endregion
            //}
            //else
            //{
                #region 拼接日历视图
            //    if (carsList != null && carsList.Count() > 0)
            //    {
            //        //获取要显示的周期
            //        int startIndex = 0;
            //        int endIndex = 0;
            //        GetWeekTools.GetStartEndIndex(dtNow, ref startIndex, ref endIndex);
            //        IList<QWGL_CARTASKS> UserTaskList = PatrolCarTaskBLL.GetXCJGUserTasks().ToList();

            //        foreach (var item in carsList)
            //        {
            //            sbMes.Append("<tr>");
            //            sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\">" + item.CARNUMBER + "</td>");
            //            for (int i = startIndex; i < endIndex; i++)//遍历对应时间的勤务
            //            {
            //                DateTime dtnow_New = dtNow.AddDays(i);

            //                DateTime dtOne = dtnow_New.Date.Date;
            //                DateTime dtTwo = dtOne.AddDays(1);

            //                IList<QWGL_CARTASKS> UserTaskList_Where = UserTaskList
            //                    .Where(a => a.CARID == item.CARID && a.SDATE >= dtOne && a.SDATE < dtTwo).ToList();

            //                string onlyT = item.CARID + "_" + dtnow_New.ToString("yyyyMMdd");

            //                if (UserTaskList_Where != null && UserTaskList_Where.Count() > 0)
            //                {
            //                    sbMes.Append("<td  id=\"td_" + onlyT + "\"   style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#cbbfbf\"  onmousemove=\"YesOnmousemove('" + onlyT + "')\"  onmouseout=\"YesOnmouseout('" + onlyT + "')\" >&nbsp;</td>");
            //                }
            //                else
            //                {
            //                    sbMes.Append("<td   id=\"td_" + onlyT + "\"  style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\"  onmousemove=\"NoOnmousemove('" + onlyT + "')\"  onmouseout=\"NoOnmouseout('" + onlyT + "')\">&nbsp;</td>");
            //                }
            //            }
            //            sbMes.Append("</tr>");
            //        }
            //    }
            //    else
            //    {
            //        sbMes.Append("<tr>");
            //        sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;color:red;\" colspan=\"8\">没有查询到车辆</td>");
            //        sbMes.Append("</tr>");
            //    }
                #endregion
            //}
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

            decimal parentID = UnitBLL.GetParentIDByUnitID(SessionManager.User.UnitID);
            IList<QWGL_AREAS> arealist = PatrolAreaBLL.GetPatrolAreas(2).Where(t=>t.STATE==1).ToList();
            ViewBag.SSDD = UnitBLL.GetUnitNameByUnitID(parentID);
            ViewBag.unitName = SessionManager.User.UnitName;
            ViewBag.areaID = new SelectList(arealist, "AREAID", "AREANAME");
            List<QWGL_AREAS> areaList = arealist
             .Select(c => new QWGL_AREAS
             {
                 AREANAME = c.AREANAME,
                 AREAID = c.AREAID,
                 GEOMETRY=c.GEOMETRY
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
            ViewBag.AREAID = strUserID;
            return View();
        }
        /// <summary>
        /// 获取所有车辆
        /// </summary>
        /// <returns></returns>
        private string GetZDMember()
        {
            string isEdit = Request["isEdit"];
            bool flag = false;
            if (!string.IsNullOrEmpty(isEdit))
            {
                flag = true;
            }
            string strUserID = Request["id"];
            int AREAID = 0;
            int.TryParse(strUserID, out AREAID);
            IList<QWGL_CARS> carsList = QWGL_CARSBLL.GetAllCars().ToList();
            StringBuilder ZDMemberStr = new StringBuilder();
            foreach (var item in carsList)
            {
                //if (AREAID == item.CARID)
                //{
                    if (flag)//是修改，则人员多选框不可修改
                    {
                        ZDMemberStr.Append("<span style=\"margin-right:10px\"><input name=\"ZDMemberIDs\"  type=\"hidden\" value=\"" + item.CARID + "\" checked=\"cheked\" readonly=\"readonly\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />" + item.CARNUMBER + "</span>");
                    }
                    else 
                    {
                        ZDMemberStr.Append("<span style=\"margin-right:10px\"><input name=\"ZDMemberIDs\"  type=\"checkbox\" value=\"" + item.CARID + "\" checked=\"cheked\" style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\" />" + item.CARNUMBER + "</span>");
                    }
                  
                //}
                //else
                //{
                //    if (flag)
                //    {
                //        ZDMemberStr.Append("<span style=\"margin-right:10px;display:none;\"><input  name=\"ZDMemberIDs\"  type=\"hidden\" readonly=\"readonly\" value=\"" + item.CARID + "\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>" + item.CARNUMBER + "</span>");
                //    }
                //    else
                //    {
                //        ZDMemberStr.Append("<span style=\"margin-right:10px\"><input  name=\"ZDMemberIDs\"  type=\"checkbox\" value=\"" + item.CARID + "\"  style=\"margin-bottom:2px;margin-top:0px;margin-right:5px;\"/>" + item.CARNUMBER + "</span>");
                //    }
                   
                //}
            }
            return ZDMemberStr.ToString();
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        public void ConmmitAddUserTask(QWGL_CARTASKS userTask, List<decimal> ZDMemberIDs, DateTime StartDate, DateTime EndDate, List<string> weeks)
        {
            string array = Request["AreaAddressArray"];
            string[] arraylist = array.Split(',');
            string strUserID = Request["id"];
            string date = Request["date"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            decimal? userid = 0;
            if (SessionManager.User.UserID != 0)
            {
                userid = SessionManager.User.UserID;
            }
            int ENDHOUR = EndDate.Hour;
            int ENDMINUTE = EndDate.Minute;
            int day = Math.Abs(((TimeSpan)(EndDate - StartDate)).Days);
            for (int i = 0; i < ZDMemberIDs.Count(); i++)
            {
                for (int j = 0; j < day + 1; j++)
                {
                    bool result = IsAdd(weeks, StartDate.AddDays(j));
                    if (result == true)
                    {
                        QWGL_CARTASKS task = PatrolCarTaskBLL
                       .GetQWGLCarTaskByRouteID(ZDMemberIDs[i], StartDate.AddDays(j));

                        if (task == null)
                        {
                            QWGL_CARTASKS route = new QWGL_CARTASKS
                            {
                                CARTASKID = PatrolCarTaskBLL.GetNewCARTASKID(),
                                CARID = ZDMemberIDs[i],
                                SDATE = StartDate.AddDays(j),
                                //EDATE = EndDate,
                                EDATE = StartDate.AddDays(j).Date.AddHours(ENDHOUR).AddMinutes(ENDMINUTE),
                                TASKCONTENT = userTask.TASKCONTENT,
                                CREATEDTIME = DateTime.Now,
                                CRETEUSERID = SessionManager.User.UserID
                            };
                            PatrolCarTaskBLL.AddCarTask(route);
                            if (arraylist.Length > 0 && !string.IsNullOrEmpty(arraylist[0]))
                            {
                                for (int h = 0; h < arraylist.Length; h++)
                                {
                                    //获取随机数生成ATRID主键
                                    Random r = new Random();
                                    int number = r.Next(10000, 99999);
                                    string time = DateTime.Now.ToString("yyyyMMddHHmmssfff") + number;
                                    string ATRID = time;

                                    QWGL_CARTASKRAREARS AREATASKRS = new QWGL_CARTASKRAREARS
                                    {
                                        CARTASKID = route.CARTASKID,
                                        AREAID = Convert.ToDecimal(arraylist[h])
                                    };
                                    PatrolCarTaskBLL.AddQWGL_AREATASKRS(AREATASKRS);
                                }
                            }
                        }
                    }
                }

            }
            //Response.Write("<script>alert('任务派遣成功！');window.location.href='/QWGLCar/VehicleTask'</script>");
            Response.Write("<script>window.location.href='/QWGLCar/VehicleTask/?flag=1'</script>");
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
            int userID = 0;
            int.TryParse(strUserID, out userID);
            //获取服务器时间
            string serviceTime = DateTime.Now.ToString("yyyy-MM-dd");
            string date = Request["date"];
            string type = Request["type"];
            ViewBag.ZDMemberStr = GetZDMember();
            //如果大于等于当前服务器时间
            if (Convert.ToDateTime(date) >= Convert.ToDateTime(serviceTime))
            {
                ViewBag.trueUpdate = "2";
            }
            else
            {
                ViewBag.trueUpdate = "1";
            }
            QWGL_CARTASKS task = PatrolCarTaskBLL
                     .GetQWGLCarTaskByRouteID(userID, DateTime.Parse(date));
          
            //获取多巡查区域任务表数据
            QWGL_CARTASKS listone = PatrolCarTaskBLL.GetQWGL_AREATASKRS(Convert.ToDecimal(userID), DateTime.Parse(date));
            if (listone != null)
            {
                ViewBag.listoneid = listone.QWGL_CARTASKRAREARS.FirstOrDefault().QWGL_AREAS.AREAID;
                ViewBag.listoneGEOMETRY = listone.QWGL_CARTASKRAREARS.FirstOrDefault().QWGL_AREAS.GEOMETRY;
            }
          
            decimal areaId = Convert.ToDecimal(task.CARTASKID);
            ViewBag.areaId = areaId;
           
            QWGL_CARTASKS QWGLUserTask = new QWGL_CARTASKS
            {
                CARID = userID,
                SDATE = task.SDATE,
                EDATE = task.EDATE,
                TASKCONTENT = task.TASKCONTENT,
            };
            //巡查区域
            List<QWGL_AREAS> areaList = PatrolAreaBLL.GetPatrolAreas(2).ToList().Select(c => new
           QWGL_AREAS
            {
                AREANAME = c.AREANAME,
                AREAID = c.AREAID,
                GEOMETRY=c.GEOMETRY
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
        public void CommitEditUserTask(QWGL_CARTASKS userTask, List<decimal> ZDMemberIDs, List<string> weeks)
        {
            DateTime StartDate = Convert.ToDateTime(Request["StrTime"]);
            DateTime EndDate = Convert.ToDateTime(Request["EdTime"]);
            string strUserID = Request["id"];
            int userID = 0;
            int.TryParse(strUserID, out userID);
            string date = Request["date"];
            string array = Request["AreaAddressArray"];
            string[] arraylist = array.Split(',');

            QWGL_CARTASKS listone = PatrolCarTaskBLL.GetQWGL_AREATASKRS((decimal)userID, DateTime.Parse(date));
           
                string id = listone.CARTASKID.ToString();
                //删除巡查区域表数据
                PatrolCarTaskBLL.DeleteQWGL_AREATASKRS(id);
           
            int day = Math.Abs(((TimeSpan)(EndDate - StartDate)).Days);
            for (int i = 0; i < ZDMemberIDs.Count(); i++)
            {
                for (int j = 0; j < day + 1; j++)
                {

                    QWGL_CARTASKS task = PatrolCarTaskBLL
                   .GetQWGLCarTaskByRouteID(ZDMemberIDs[i], StartDate.AddDays(j));

                    if (task != null)
                    {
                        QWGL_CARTASKS tas = new QWGL_CARTASKS
                        {
                            CARTASKID = task.CARTASKID,
                            CARID = userID,
                            SDATE = StartDate,
                            EDATE = EndDate,
                            TASKCONTENT = userTask.TASKCONTENT,
                            CREATEDTIME = DateTime.Now,
                            CRETEUSERID = SessionManager.User.UserID
                        };
                        PatrolCarTaskBLL.ModifyCarTask(tas);
                        if (arraylist.Length > 0 && !string.IsNullOrEmpty(arraylist[0]))
                        {
                            for (int h = 0; h < arraylist.Length; h++)
                            {
                                //获取随机数生成ATRID主键
                                Random r = new Random();
                                int number = r.Next(10000, 99999);
                                string time = DateTime.Now.ToString("yyyyMMddHHmmssfff") + number;
                                string ATRID = time;
                                QWGL_CARTASKRAREARS AREATASKRS = new QWGL_CARTASKRAREARS
                                {
                                    CARTASKID = tas.CARTASKID,
                                    AREAID = Convert.ToDecimal(arraylist[h])
                                };
                                PatrolCarTaskBLL.AddQWGL_AREATASKRS(AREATASKRS);
                            }
                        }

                    }

                }
            }
            //Response.Write("<script>window.location.href='/QWGLCar/VehicleTask'</script>");
            Response.Write("<script>window.location.href='/QWGLCar/VehicleTask/?flag=2'</script>");
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
                QWGL_CARTASKS listone = PatrolCarTaskBLL.GetQWGL_AREATASKRS((decimal)userID, DateTime.Parse(date));
               
                    string id = listone.CARTASKID.ToString();
                    //删除巡查区域表数据
                    PatrolCarTaskBLL.DeleteQWGL_AREATASKRS(id);
               
                PatrolCarTaskBLL.DeleteCarTask(userID, DateTime.Parse(date));
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
