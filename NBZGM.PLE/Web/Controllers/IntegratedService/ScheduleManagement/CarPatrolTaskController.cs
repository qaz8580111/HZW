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
    public class CarPatrolTaskController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/CarPatrolTask/";

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
            ViewBag.urlUpWeek = "/CarPatrolTask/Index?t=" + dtNow.AddDays(-7).ToString("yyyy-MM-dd");
            ViewBag.urlNextWeek = "/CarPatrolTask/Index?t=" + dtNow.AddDays(7).ToString("yyyy-MM-dd");

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
                    Value = t.UNITID.ToString(),
                    Selected = t.UNITID == zdId ? true : false
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
        #endregion

        #region 获取日历

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="dtNow">时间</param>
        /// <returns></returns>
        public string GetTableContentMes(DateTime dtNow, decimal ZDID)
        {
            //    string SSZDID = Request["SSZDID"];
            //    decimal ZDID = 0;
            //    decimal.TryParse(SSZDID, out ZDID);
            if (ZDID == 0)
                ZDID = SessionManager.User.UnitID;
            StringBuilder sbMes = new StringBuilder();
            IQueryable<ZFGKCAR> CarList = GetAllCars().Where(a => a.UNITID == ZDID);


            if (CarList != null && CarList.Count() > 0)
            {
                //获取要显示的周期
                int startIndex = 0;
                int endIndex = 0;
                GetStartEndIndex(dtNow, ref startIndex, ref endIndex);
                IList<XCJGCARTASK> CarTaskList = Taizhou.PLE.BLL.XCJGBLLs.PartrolCarTaskBLL.GetXCJGCarTasks().ToList();

                foreach (var item in CarList)
                {
                    sbMes.Append("<tr>");
                    sbMes.Append("<td style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\">" + item.CARNO + "</td>");
                    for (int i = startIndex; i < endIndex; i++)//遍历对应时间的勤务
                    {

                        DateTime dtnow_New = dtNow.AddDays(i);

                        DateTime dtOne = dtnow_New.Date.Date;
                        DateTime dtTwo = dtOne.AddDays(1);
                        IList<XCJGCARTASK> CarTaskList_Where = CarTaskList
                            .Where(a => a.CARID == item.CARID && a.TASKDATE >= dtOne && a.TASKDATE < dtTwo).ToList();


                        string onlyT = item.CARID + "_" + dtnow_New.ToString("yyyyMMdd");

                        if (CarTaskList_Where != null && CarTaskList_Where.Count() > 0)
                        {
                            sbMes.Append("<td  id=\"td_" + onlyT + "\"   style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#cbbfbf\"  onmousemove=\"YesOnmousemove('" + onlyT + "')\"  onmouseout=\"YesOnmouseout('" + onlyT + "')\"  onclick=\"EditCarTask('" + item.CARID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "')\">&nbsp;</td>");
                        }
                        else
                        {
                            sbMes.Append("<td id=\"td_" + onlyT + "\"  style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd; border-collapse: collapse;background:#f2f5f7\"  onmousemove=\"NoOnmousemove('" + onlyT + "')\"  onmouseout=\"NoOnmouseout('" + onlyT + "')\"  onclick=\"AddCarTask('" + item.CARID + "','" + dtnow_New.ToString("yyyy-MM-dd") + "')\">&nbsp;</td>");
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
            sbMes.Append("<th style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd;\">&nbsp;</th>");

            int startIndex = 0;
            int endIndex = 0;
            GetStartEndIndex(dtNow, ref startIndex, ref endIndex);

            for (int i = startIndex; i < endIndex; i++)
            {
                string dayWeek;
                string MD = GetMDT(dtNow, i, out dayWeek);
                sbMes.Append("<th style=\"text-align: center; width: 12%; height:30px; border:1px solid #ddd;\">" + MD + "(" + dayWeek.Replace("星期", "") + ")</th>");
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
                XCJGCARTASK modle = PartrolCarTaskBLL.GetXCJGCARTASKByRouteID(ID, DateTime.Parse(date));
                if (modle != null)
                    startHour = modle.STARTHOUR;
            }

            StringBuilder HoursStr = new StringBuilder();
            for (int i = 0; i < 24; i++)
            {
                string value = i.ToString();
                if (i < 10)
                    value = "0" + i.ToString();
                if (i == 24)
                    value = "00";
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
                XCJGCARTASK modle = PartrolCarTaskBLL.GetXCJGCARTASKByRouteID(ID, DateTime.Parse(date));
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
                XCJGCARTASK modle = PartrolCarTaskBLL.GetXCJGCARTASKByRouteID(ID, DateTime.Parse(date));
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
                XCJGCARTASK modle = PartrolCarTaskBLL.GetXCJGCARTASKByRouteID(ID, DateTime.Parse(date));
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

        #region 添加任务
        /// <summary>
        /// 添加巡查任务
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCarTask()
        {
            string strUserID = Request["id"];
            decimal parentID = UnitBLL
                .GetParentIDByUnitID(SessionManager.User.UnitID);

            IList<Taizhou.PLE.Model.XCJGAREA> arealist = PatrolAreaBLL.GetPatrolAreas(SessionManager.User.UserID, 2).ToList();
            IList<Taizhou.PLE.Model.XCJGROUTE> routelist = PatrolRouteBLL.GetPatrolRoutes(SessionManager.User.UserID, 2).ToList();
            ViewBag.SSDD = UnitBLL.GetUnitNameByUnitID(parentID);
            ViewBag.unitName = SessionManager.User.UnitName;
            ViewBag.areaID = new SelectList(arealist, "AREAID", "AREANAME");

            ViewBag.routeID = new SelectList(routelist, "ROUTEID", "ROUTENAME");

            List<SelectListItem> areaList = PatrolAreaBLL.GetPatrolAreas(SessionManager.User.UserID, 2).ToList()
              .Select(c => new SelectListItem
              {
                  Text = c.AREANAME,
                  Value = c.AREAID.ToString()
              }).ToList();
            List<SelectListItem> areaListAll = new List<SelectListItem>();
            //areaListAll.Add(new SelectListItem() { Text = "请选择", Value = "0", Selected = true });
            if (areaList != null && areaList.Count > 0)
                areaListAll.AddRange(areaList);
            ViewBag.areaList = areaListAll;


            List<SelectListItem> routeList = PatrolRouteBLL.GetPatrolRoutes(SessionManager.User.UserID, 2).ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.ROUTENAME,
                    Value = c.ROUTEID.ToString()
                }).ToList();
            ViewBag.routeList = routeList;

            //获取大队名称
            //List<SelectListItem> DAIDList = UnitBLL.GetAllUnits().Where(a => a.UNITTYPEID == 4).ToList()
            // .Select(c => new SelectListItem
            // {
            //     Text = c.UNITNAME,
            //     Value = c.UNITID.ToString()
            // }).ToList();

            //ViewBag.DAIDList = DAIDList;
            return View(THIS_VIEW_PATH + "AddCarTask.cshtml");
        }
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="userTask"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConmmitAddCarTask(XCJGCARTASK carTask)
        {

            string strCarId = Request["id"];
            string date = Request["date"];
            int CarID = 0;
            int.TryParse(strCarId, out CarID);

            decimal parentID = UnitBLL
                    .GetParentIDByUnitID(SessionManager.User.UnitID);

            XCJGCARTASK route = new XCJGCARTASK
            {
                CARID = CarID,
                SSZDID = SessionManager.User.UnitID,
                SSQJID = 40,
                TASKDATE = DateTime.Parse(date),

                STARTHOUR = carTask.STARTHOUR,
                STARTMINUTE = carTask.STARTMINUTE,
                ENDHOUR = carTask.ENDHOUR,
                ENDMINUTE = carTask.ENDMINUTE,
                AREAID = carTask.AREAID,
                ROUTEID = carTask.ROUTEID,
                JOBCONTENT = carTask.JOBCONTENT
            };

            Taizhou.PLE.BLL.XCJGBLLs.PartrolCarTaskBLL.AddCarTask(route);
            return RedirectToAction("Index");
        }
        #endregion

        #region 修改任务
        public ActionResult EditCarTask()
        {
            string strCarID = Request["id"];
            int CarID = 0;
            int.TryParse(strCarID, out CarID);
            string date = Request["date"];

            XCJGCARTASK task = PartrolCarTaskBLL.GetXCJGCARTASKByRouteID(CarID, DateTime.Parse(date));
            if (task != null)
            {
                decimal id = Convert.ToDecimal(task.AREAID);
                XCJGAREA areamodel = PatrolAreaBLL.GetXCJGAreaByAreaID(id);
                ViewBag.areaMap = areamodel.GEOMETRY;

                decimal routeid = Convert.ToDecimal(task.ROUTEID);
                XCJGROUTE routemodel = PatrolRouteBLL.GetXCJGRouteByRouteID(routeid);

                if (routemodel != null)
                {
                    ViewBag.routeMap = routemodel.GEOMETRY;
                }
                else
                {
                    ViewBag.routeMap = "";
                }


                XCJGCARTASK carTask = new XCJGCARTASK
                {
                    CARID = CarID,
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

                //获取大队名称
                string unitList = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == carTask.SSQJID).UNITNAME;
                ViewBag.unitList = unitList;

                //获得中队名称
                string unitName = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == carTask.SSZDID).UNITNAME;
                ViewBag.unitNanme = unitName;

                List<SelectListItem> areaList = PatrolAreaBLL.GetPatrolAreas(SessionManager.User.UserID, 2).ToList()
                    .Select(c => new SelectListItem
                    {
                        Text = c.AREANAME,
                        Value = c.AREAID.ToString()
                    }).ToList();
                ViewBag.areaList = areaList;
                List<SelectListItem> routeList = PatrolRouteBLL.GetPatrolRoutes(SessionManager.User.UserID, 2).ToList()
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

                return View(THIS_VIEW_PATH + "EditCarTask.cshtml", carTask);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// 提交修改任务
        /// </summary>
        [HttpPost]
        public ActionResult CommitEditCarTask(XCJGCARTASK carTask)
        {
            string strCarID = Request["id"];
            int carID = 0;
            int.TryParse(strCarID, out carID);
            string date = Request["date"];
            XCJGCARTASK task = new XCJGCARTASK
            {
                CARID = carID,
                TASKDATE = DateTime.Parse(date),
                SSZDID = carTask.SSZDID,
                SSQJID = 40,
                STARTHOUR = carTask.STARTHOUR,
                STARTMINUTE = carTask.STARTMINUTE,
                ENDHOUR = carTask.ENDHOUR,
                ENDMINUTE = carTask.ENDMINUTE,
                AREAID = carTask.AREAID,
                ROUTEID = carTask.ROUTEID,
                JOBCONTENT = carTask.JOBCONTENT
            };

            Taizhou.PLE.BLL.XCJGBLLs.PartrolCarTaskBLL.ModifyCarTask(task);
            return RedirectToAction("Index");
        }
        #endregion

        #region 删除任务
        /// <summary>
        /// 删除任务
        /// </summary>
        public bool DeleteCarTask()
        {
            string strCarID = Request["id"];
            decimal carID = 0;
            string date = Request["date"];
            if (decimal.TryParse(strCarID, out carID))
            {
                Taizhou.PLE.BLL.XCJGBLLs.PartrolCarTaskBLL.DeleteCarTask(carID, DateTime.Parse(date));
                return true;
            }

            return false;
        }
        #endregion

        //获取所有的CAR
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<ZFGKCAR> GetAllCars()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<ZFGKCAR> results = db.ZFGKCARS;


            return results;
        }

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
