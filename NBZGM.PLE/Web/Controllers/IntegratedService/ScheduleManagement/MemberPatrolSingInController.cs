using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.XCJGBLLs;
using Taizhou.PLE.Common.Enums.XCJGEnums;
using Taizhou.PLE.Model;
using Web.ViewModels;

namespace Web.Controllers.IntegratedService.ScheduleManagement
{
    public class MemberPatrolSingInController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/MemberPatrolSingIn/";

        //
        // GET: /执法队员巡查签到管理/
        public ActionResult Index()
        {
            List<SelectListItem> unitList = UnitBLL.GetAllUnits().ToList()
               .Select(c => new SelectListItem
               {
                   Text = c.UNITNAME,
                   Value = c.UNITID.ToString()
               }).ToList();

            ViewBag.unitList = unitList;

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }
        /// <summary>
        /// 分页显示签到区域列表
        /// </summary>
        public JsonResult GetSingIn(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            string addressName = Request.QueryString["AddressName"];

            string Operating = "<div>";
            Operating += @"<a class='btn btn-primary btn-small' href='/MemberPatrolSingIn/EditSingIn?routeID={0}' title='修改签到区域'><i class='icon-edit padding-null'></i></a>";
            Operating += @"<a class='btn btn-danger btn-small' href='#' title='删除签到区域' onclick='DeleteRoute({0})'><i class='icon-trash padding-null'></i></a>";
            Operating += "</div>";

            IQueryable<XCJGSIGNIN> singin = PatrolSingInBLL
               .GetXCJGSingIns();

            if (!string.IsNullOrWhiteSpace(addressName))
            {
                singin = singin.Where(t => t.ADDRESSNAME.Contains(addressName));
            }

            List<XCJGSIGNIN> singinListC =
                (from m in singin
                     .OrderByDescending(t => t.SIGNINID)
                 select m).ToList();

            List<XCJGSIGNIN> singinList =
                (from m in singin
                     .OrderByDescending(t => t.SIGNINID)
                     .Skip((int)iDisplayStart)
                     .Take((int)iDisplayLength)
                 select m).ToList();
            var results = from c in singinList
                          select new
                          {
                              NumId = ++iDisplayStart,
                              AddressName = c.ADDRESSNAME,
                              SingInDate = Convert.ToDateTime(c.SIGNINDATE).ToString("yyyy-MM-dd"),
                              SingInDateStart = c.STARTHOUR + ":" + c.STARTMINUTE,
                              SingInDateEnd = c.ENDHOUR + ":" + c.ENDMINUTE,
                              operating = string.Format(Operating, c.SIGNINID)
                          };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = singinListC.Count(),
                iTotalDisplayRecords = singinListC.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加路线
        /// </summary>
        public ActionResult AddSingIn()
        {
            decimal parentID = UnitBLL
                .GetParentIDByUnitID(SessionManager.User.UnitID);

            ViewBag.SSDD = UnitBLL.GetUnitNameByUnitID(parentID);
            ViewBag.unitName = SessionManager.User.UnitName;

            IList<USER> userList = UserBLL.GetAllUsers().ToList();
            ViewBag.userList = userList;

            return View(THIS_VIEW_PATH + "AddSingIn.cshtml");
        }

        /// <summary>
        /// 提交添加路线表单
        /// </summary>
        [HttpPost]
        public ActionResult CommitAddSingIn(XCJGSIGNIN singin)
        {
            string StartDate = Request["startDate"];
            string EndDate = Request["endDate"];
            DateTime endDate = DateTime.Parse(EndDate);
            DateTime startDate = DateTime.Parse(StartDate);

            string USERIDS = Request["USERIDS_Name"];
            if (!string.IsNullOrEmpty(USERIDS))
                USERIDS = "," + USERIDS + ",";

            XCJGSIGNIN model = new XCJGSIGNIN
            {
                SSZDID = SessionManager.User.UnitID,
                ADDRESSNAME = singin.ADDRESSNAME,
                //SIGNINDATE = startDate.Date,
                SIGNINDATE = DateTime.Now,
                STARTHOUR = startDate.Hour,
                STARTMINUTE = startDate.Minute,
                ENDHOUR = endDate.Hour,
                ENDMINUTE = endDate.Minute,
                GEOMETRY = singin.GEOMETRY,
                SIGNINTYPEID = singin.SIGNINTYPEID,
                USERIDS = USERIDS,
            };
            PatrolSingInBLL.AddSingIn(model);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 修改路线
        /// </summary>
        public ActionResult EditSingIn()
        {
            string strRouteID = this.Request.QueryString["routeID"];
            XCJGSIGNIN singin = PatrolSingInBLL.GetXCJGSingInByID(decimal.Parse(strRouteID));
            if (singin != null)
            {
                XCJGSIGNIN model = new XCJGSIGNIN
                {
                    SSZDID = singin.SSZDID,
                    ADDRESSNAME = singin.ADDRESSNAME,
                    SIGNINDATE = Convert.ToDateTime(singin.SIGNINDATE),
                    STARTHOUR = singin.STARTHOUR,
                    STARTMINUTE = singin.STARTMINUTE,
                    ENDHOUR = singin.ENDHOUR,
                    ENDMINUTE = singin.ENDMINUTE,
                    GEOMETRY = singin.GEOMETRY,
                    SIGNINTYPEID = singin.SIGNINTYPEID,
                    USERIDS = singin.USERIDS, 
                    SIGNINID=singin.SIGNINID

                };
                //ViewBag.startDate = Convert.ToDateTime(singin.SIGNINDATE).ToString("yyyy-MM-dd") + " "
                //    + singin.STARTHOUR + ":" + singin.STARTMINUTE;
                ViewBag.startDate = singin.STARTHOUR + ":" + singin.STARTMINUTE;
                ViewBag.endDate = singin.ENDHOUR + ":" + singin.ENDMINUTE;

                IList<USER> userList = UserBLL.GetAllUsers().ToList();
                ViewBag.userList = userList;

                return View(THIS_VIEW_PATH + "EditSingIn.cshtml", model);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// 提交修改路线表单
        /// </summary>
        [HttpPost]
        public ActionResult CommitEditSingIn(XCJGSIGNIN XCJGSinin)
        {
            string StartDate = Request["startDate"];
            string EndDate = Request["endDate"];
            DateTime endDate = DateTime.Parse(EndDate);
            DateTime startDate = DateTime.Parse(StartDate);

            string USERIDS = Request["USERIDS_Name"];
            if (!string.IsNullOrEmpty(USERIDS))
                USERIDS = "," + USERIDS + ",";

            XCJGSIGNIN singin = new XCJGSIGNIN
            {
                SSZDID = XCJGSinin.SSZDID,
                ADDRESSNAME = XCJGSinin.ADDRESSNAME,
                SIGNINID = XCJGSinin.SIGNINID,
                //SIGNINDATE = startDate.Date,
                SIGNINDATE = DateTime.Now,
                STARTHOUR = startDate.Hour,
                STARTMINUTE = startDate.Minute,
                ENDHOUR = endDate.Hour,
                ENDMINUTE = endDate.Minute,
                GEOMETRY = XCJGSinin.GEOMETRY,
                SIGNINTYPEID = XCJGSinin.SIGNINTYPEID,
                USERIDS = USERIDS
            };

            PatrolSingInBLL.ModifySingIn(singin);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 签到统计界面
        /// </summary>
        /// <returns></returns>
        public ActionResult SingInStatistics()
        {
            return View(THIS_VIEW_PATH + "SingInStatistics.cshtml");
        }

        /// <summary>
        /// 签到统计
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult GetSingInStatistics(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            IQueryable<SIGNRESUL> singin = new SIGNRESULBLL().GetSIGNRESUL();
            DateTime dtStar = DateTime.Now.AddDays(1).Date;
            DateTime dtEnd = DateTime.Now.AddMonths(-1).Date;
            if (!DateTime.TryParse(Request["SIGNINSDATA"], out dtStar))//获取一个月的数据
            {
                dtStar = DateTime.Now.AddDays(1).Date;
                singin = singin.Where(a => a.SIGNINSDATA > dtEnd && a.SIGNINSDATA < dtStar);
            }
            else//查询时间
            {
                dtStar = dtStar.AddDays(1);
                dtEnd = dtStar.AddDays(-2);
                singin = singin.Where(a => a.SIGNINSDATA > dtEnd && a.SIGNINSDATA < dtStar);
            }
            IList<SIGNRESUL> singinListAll = singin.ToList();
            string userName = Request["userName"];
            if (!string.IsNullOrEmpty(userName))
            {
                IList<USER> listUser = UserBLL.GetAllUsers().ToList();
                singinListAll = (from sg in singinListAll
                                 from ub in listUser
                                 where sg.USERID == ub.USERID
                                 && ub.USERNAME.Contains(userName + "")
                                 select sg).ToList();
            }

            singin = singin.OrderByDescending(a => a.SIGNINSDATA);
            int count = singin.Count();
            List<SIGNRESUL> singinList = singinListAll.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();
            var results = from c in singinList
                          .ToList()
                          select new
                          {
                              NumId = ++iDisplayStart,
                              SIGNINSDATA = ((DateTime)c.SIGNINSDATA).ToString("yyyy-MM-dd"),
                              ADDRESSNAME = GetSingInAddress((DateTime)c.SIGNINSDATA, (decimal)c.USERID),
                              USERNAME = UserBLL.GetUserNameByUserID((decimal)c.USERID),
                              AMTOPUNCH = c.AMTOPUNCH,
                              AMCLOCKOUT = c.AMCLOCKOUT,
                              PMTOPUNCH = c.PMTOPUNCH,
                              PMCLOCKOUT = c.PMCLOCKOUT,
                              RESTTIMES = c.RESTTIMES,
                              DRIVINGDISTANC = c.DRIVINGDISTANC,
                              BJCS = "开发中"
                          };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取签到区域名称
        /// </summary>
        /// <param name="SIGNINSDATA">签到时间</param>
        /// <param name="USERID">签到人员编号</param>
        /// <returns></returns>
        private string GetSingInAddress(DateTime SIGNINSDATA, decimal USERID)
        {
            string ADDRESSNAME = "未定义签到区域";

            DateTime dtS = SIGNINSDATA.Date;
            DateTime dtE = dtS.AddDays(1);

            IList<XCJGSIGNIN> List = PatrolSingInBLL.GetXCJGSingIns()
                .Where(a => a.SIGNINDATE >= dtS
                    && a.SIGNINDATE < dtE).ToList();

            XCJGSIGNIN xcSigModel = List.FirstOrDefault(a => a.USERIDS.Contains("," + USERID + ","));
            if (xcSigModel != null)
                ADDRESSNAME = xcSigModel.ADDRESSNAME;

            return ADDRESSNAME;

        }

        /// <summary>
        /// 根据路线标识删除路线
        /// </summary>
        public bool DeleteSingIn()
        {
            string strZDID = Request.QueryString["ZDID"];
            decimal id = 0;

            if (decimal.TryParse(strZDID, out id))
            {
                PatrolSingInBLL.DeleteSingIn(id);
                return true;
            }

            return false;
        }

        #region 获取时间
        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <param name="hours">选中的时间</param>
        private string GetHoursMes()
        {
            string ID = Request["id"];
            int id = 0;
            int.TryParse(ID, out id);


            decimal? startHour = 0;
            if (id != 0)
            {
                XCJGSIGNIN modle = PatrolSingInBLL.GetXCJGSingInByID(id);
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
            string ID = Request["id"];
            int id = 0;
            int.TryParse(ID, out id);


            decimal? endHour = 0;
            if (id != 0)
            {
                XCJGSIGNIN modle = PatrolSingInBLL.GetXCJGSingInByID(id);
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
            string ID = Request["id"];
            int id = 0;
            int.TryParse(ID, out id);


            decimal? startMinute = 0;
            if (id != 0)
            {
                XCJGSIGNIN modle = PatrolSingInBLL.GetXCJGSingInByID(id);
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
            string ID = Request["id"];
            int id = 0;
            int.TryParse(ID, out id);


            decimal? endMinute = 0;
            if (id != 0)
            {
                XCJGSIGNIN modle = PatrolSingInBLL.GetXCJGSingInByID(id);
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
        #endregion

    }
}
