using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MemberPatrolRouteController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/MemberPatrolRoute/";

        //
        // GET: /执法队员巡查路线管理/

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 分页显示执法队员巡查路线列表
        /// </summary>
        public JsonResult GetMemberRoutes(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            string routeName = Request.QueryString["routeName"];
            string Operating = "<div>";

            Operating += @"
<a class='btn btn-primary btn-small' 
href='/MemberPatrolRoute/EditRoute?routeID={0}' 
title='修改路线'><i class='icon-edit padding-null'></i></a>
";
            Operating += @"
<a class='btn btn-danger btn-small' href='#' 
title='删除路线' onclick='DeleteRoute({0})'>
<i class='icon-trash padding-null'></i></a>
";
            Operating += "</div>";

            IQueryable<XCJGROUTE> routes = PatrolRouteBLL
                .GetPatrolRoutes(SessionManager.User.UserID,
                (decimal)RouteOwnerType.Member);

            if (!string.IsNullOrWhiteSpace(routeName))
            {
                routes = routes.Where(t => t.ROUTENAME.Contains(routeName));
            }

            List<XCJGROUTE> resultListC =
                (from m in routes
                     .OrderByDescending(t => t.ROUTEID)
                 select m).ToList();

            List<XCJGROUTE> resultList =
                (from m in routes
                     .OrderByDescending(t => t.ROUTEID)
                     .Skip((int)iDisplayStart)
                     .Take((int)iDisplayLength)
                 select m).ToList();

            var results = from c in resultList
                          select new
                          {
                             
                              routeName = c.ROUTENAME,
                              routeDescription = c.ROUTEDESCRIPTION,
                              operating = string.Format(Operating, c.ROUTEID)
                          };

            return Json(new
            {
                sEcho = secho,
                //iTotalRecords = resultList.Count(),
                //iTotalDisplayRecords = resultList.Count(),
                iTotalRecords = resultListC.Count(),
                iTotalDisplayRecords = resultListC.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加路线
        /// </summary>
        public ActionResult AddRoute()
        {
            decimal parentID = UnitBLL
                .GetParentIDByUnitID(SessionManager.User.UnitID);

            ViewBag.SSDD = UnitBLL.GetUnitNameByUnitID(parentID);
            ViewBag.unitName = SessionManager.User.UnitName;

            return View(THIS_VIEW_PATH + "AddRoute.cshtml");
        }

        /// <summary>
        /// 提交添加路线表单
        /// </summary>
        [HttpPost]
        public ActionResult CommitAddRoute(VMRoute vmRoute)
        {
            decimal parentID = UnitBLL
                .GetParentIDByUnitID(SessionManager.User.UnitID);

            XCJGROUTE route = new XCJGROUTE
            {
                SSDDID = parentID,
                SSZDID = SessionManager.User.UnitID,
                ROUTENAME = vmRoute.RouteName,
                ROUTEDESCRIPTION = vmRoute.RouteDescription,
                GEOMETRY = vmRoute.Geometry,
                ROUTEOWNERTYPE = (decimal)RouteOwnerType.Member

            };

            PatrolRouteBLL.AddRoute(route);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 修改路线
        /// </summary>
        public ActionResult EditRoute()
        {
            string strRouteID = this.Request.QueryString["routeID"];

            XCJGROUTE route = PatrolRouteBLL
                .GetXCJGRouteByRouteID(decimal.Parse(strRouteID));

            VMRoute vmRoute = new VMRoute
            {
                RouteID = route.ROUTEID,
                SSDDID = route.SSDDID,
                SSZDID = route.SSZDID,
                RouteName = route.ROUTENAME,
                RouteDescription = route.ROUTEDESCRIPTION,
                Geometry = route.GEOMETRY
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
            string unitList = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == vmRoute.SSDDID).UNITNAME;
            ViewBag.unitList = unitList;

            //获得中队名称
            string unitName = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == vmRoute.SSZDID).UNITNAME;
            ViewBag.unitNanme = unitName;

            return View(THIS_VIEW_PATH + "EditRoute.cshtml", vmRoute);
        }

        /// <summary>
        /// 提交修改路线表单
        /// </summary>
        [HttpPost]
        public ActionResult CommitEditRoute(VMRoute vmRoute)
        {
            XCJGROUTE route = new XCJGROUTE
            {
                ROUTEID = vmRoute.RouteID,
                ROUTENAME = vmRoute.RouteName,
                GEOMETRY = vmRoute.Geometry,
                ROUTEDESCRIPTION = vmRoute.RouteDescription
            };

            PatrolRouteBLL.ModifyRoute(route);

            return RedirectToAction("Index");
        }


        /// <summary>
        /// 根据路线标识删除路线
        /// </summary>
        public int ifDeleteRoute()
        {
            string strRouteID = Request.QueryString["routeID"];
            decimal routeID = 0;
            decimal.TryParse(strRouteID, out routeID);
            IQueryable<XCJGUSERTASK> task = PatrolUserTaskBLL.GetXCJGUserTasks().Where(a => a.ROUTEID == routeID);
            if (task != null && task.Count() > 0)
            {
                return 1;
            }
            else
            {
                DeleteRoute(strRouteID);
                return 0;
            }

        }

        /// <summary>
        /// 根据路线标识删除路线
        /// </summary>
        public bool DeleteRoute(string routeID)
        {
            // string strRouteID = Request.QueryString["routeID"];
            decimal routeIDInt = 0;
            decimal.TryParse(routeID, out routeIDInt);
            IQueryable<XCJGUSERTASK> task = PatrolUserTaskBLL.GetXCJGUserTasks().Where(a => a.ROUTEID == routeIDInt);
            if (task != null && task.Count() > 0)
            {
                foreach (var item in task)
                {
                    PatrolUserTaskBLL.DeleteUserTask(item.USERID, item.TASKDATE);
                }
                PatrolRouteBLL.DeleteRoute(routeIDInt);
                return true;
            }
            else
            {
                PatrolRouteBLL.DeleteRoute(routeIDInt);
                return false;
            }
        }
    }
}
