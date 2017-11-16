using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.XCJGBLLs;
using Taizhou.PLE.BLL.UserBLLs;

namespace Web.Controllers.IntegratedService.ScheduleManagement
{
    public class MemberPatrolRestController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/MemberPatrolRest/";

        //
        // GET: /MemberPatrolRest/
        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public JsonResult GetMemberRest(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            string restName = Request.QueryString["restName"];
            string Operating = "<div>";
            Operating += @"<a class='btn btn-primary btn-small' href='/MemberPatrolRest/EditRest?restId={0}' title='修改区域'><i class='icon-edit padding-null'></i></a>";
            Operating += @"<a class='btn btn-danger btn-small' href='javascript:void(0)' title='删除区域' onclick=DeleteRest('{1}')><i class='icon-trash padding-null'></i></a>";
            Operating += "</div>";

            IQueryable<XCJGREST> areas = new PatrolRestBLL().GetList();

            if (!string.IsNullOrWhiteSpace(restName))
            {
                areas = areas.Where(t => t.RESTNAME.Contains(restName));
            }

            List<XCJGREST> resultListC = areas.OrderByDescending(a => a.CREATETIME).ToList();

            List<XCJGREST> resultList = resultListC
                .Skip((int)iDisplayStart)
                .Take((int)iDisplayLength)
                .ToList();

            var results = from c in resultList
                          select new
                          {
                              NumId = ++iDisplayStart,
                              RESTNAME = c.RESTNAME,
                              areaDescription = c.RESTREMARK,
                              operating = string.Format(Operating, c.RESTID, c.RESTID)
                          };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = resultListC.Count(),
                iTotalDisplayRecords = resultListC.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MemberPatrolRest/Create
        public ActionResult AddRest()
        {
            IList<USER> userList = UserBLL.GetAllUsers().ToList();
            ViewBag.userList = userList;

            return View(THIS_VIEW_PATH + "AddRest.cshtml");
        }

        [HttpPost]
        public ActionResult CommitAddRest()
        {
            string USERIDS = Request["USERIDS_Name"];
            if (!string.IsNullOrEmpty(USERIDS))
                USERIDS = "," + USERIDS + ",";

            XCJGREST area = new XCJGREST
            {
                RESTID = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                RESTNAME = Request["RestName"],
                RESTREMARK = Request["RestDescription"],
                GEOMETRY = Request["Geometry"],
                CREATETIME = DateTime.Now,
                USERIDS = USERIDS
            };
            new PatrolRestBLL().Add(area);

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        [HttpPost]
        public ActionResult CommitEditRest()
        {
            string USERIDS = Request["USERIDS_Name"];
            if (!string.IsNullOrEmpty(USERIDS))
                USERIDS = "," + USERIDS + ",";

            XCJGREST area = new XCJGREST
            {
                RESTID = Request["RESTID"],
                RESTNAME = Request["RestName"],
                RESTREMARK = Request["RestDescription"],
                GEOMETRY = Request["Geometry"],
                USERIDS = USERIDS
            };
            new PatrolRestBLL().Update(area);

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        //
        // GET: /MemberPatrolRest/Edit/5
        public ActionResult EditRest(string restId)
        {
            XCJGREST xcjgModel = new PatrolRestBLL().GetSingle(restId);
            if (xcjgModel == null)
            {
                xcjgModel = new XCJGREST();
            }
            ViewBag.RESTID = xcjgModel.RESTID;
            ViewBag.RESTNAME = xcjgModel.RESTNAME;
            ViewBag.RESTREMARK = xcjgModel.RESTREMARK;
            ViewBag.GEOMETRY = xcjgModel.GEOMETRY;
            ViewBag.USERIDS = xcjgModel.USERIDS;

            IList<USER> userList = UserBLL.GetAllUsers().ToList();
            ViewBag.userList = userList;

            return View(THIS_VIEW_PATH + "EditRest.cshtml");
        }

        //
        // GET: /MemberPatrolRest/Delete/5
        public string Delete(string restId)
        {
            XCJGREST xcjgModel = new PatrolRestBLL().GetSingle(restId);
            if (xcjgModel != null)
            {
                new PatrolRestBLL().Delete(restId);
                return "删除成功";
            }
            else
            {
                return "删除失败";
            }
        }


        public ActionResult RestStatistics()
        {
            return View(THIS_VIEW_PATH + "RestStatistics.cshtml");
        }

        public JsonResult GetRestStatistics(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            string userName = Request.QueryString["userName"];
            string Operating = "<div>";
            Operating += @"<a class='btn btn-primary btn-small' href='javascript:void(0)' title='有效' onclick=DealRestAlarm('{0}',1)>有效</a>";
            Operating += @"<a class='btn btn-danger btn-small' href='javascript:void(0)' title='无效' onclick=DealRestAlarm('{1}',0)>无效</a>";
            Operating += "</div>";

            IQueryable<RestAlarmViewModel> areas = new PatrolRestAlarmBLL().GetListRestAlarmV();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                areas = areas.Where(t => t.UserName.Contains(userName));
            }
            int count = areas.Count();

            IList<RestAlarmViewModel> resultList = areas
                .OrderByDescending(a => a.ALARMTIME)
                .Skip((int)iDisplayStart)
                .Take((int)iDisplayLength)
                .ToList();

            var results = from c in resultList
                          select new
                          {
                              RALARMID = c.RALARMID,
                              USERID = c.USERID,
                              USERNAME = c.UserName,
                              ALARMTIME = Convert.ToDateTime(c.ALARMTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                              LONLAT = c.LONLAT,
                              ALARMTYPE = c.ALARMTYPE == 1 ? "越界报警" : c.ALARMTYPE == 1 ? "休息报警" : "位置报警",
                              ISVALID = c.ISVALID == 0 ? "无效" : (c.ISVALID == 1 ? "有效" : "未处理"),
                              DEALTIME = c.DEALTIME == null ? "--" : Convert.ToDateTime(c.DEALTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                              operating = (c.ISVALID == 0 || c.ISVALID == 1) ? "--" : string.Format(Operating, c.RALARMID, c.RALARMID)
                          };
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        public string DealRestAlarm(string RALARMID, decimal ISVALID)
        {
            new PatrolRestAlarmBLL().Update(RALARMID, ISVALID);
            return "处理成功";
        }

    }
}
