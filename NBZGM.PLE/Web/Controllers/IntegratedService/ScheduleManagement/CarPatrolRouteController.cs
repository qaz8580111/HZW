using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.XCJGBLLs;
using Taizhou.PLE.Common.Enums.XCJGEnums;
using Taizhou.PLE.Model;
using Web.ViewModels;

namespace Web.Controllers.IntegratedService.ScheduleManagement
{
    public class CarPatrolRouteController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/CarPatrolRoute/";

        //
        // GET: /执法车辆巡查路线管理/

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 分页显示执法队员巡查路线列表
        /// </summary>
        public JsonResult GetCarRoutes(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            string routeName = Request.QueryString["routeName"];

            string drawLine1 = @"
<a href='/CarPatrolRoute/EditRoute?routeID={0}'
title='绘制路线'>已绘制</a>
";
            string drawLine2 = @"
<a href='/CarPatrolRoute/EditRoute?routeID={0}'
title='绘制路线'>未绘制</a>
";
            string Operating = "<div>";

            Operating += @"
<a class='btn btn-primary btn-small' 
href='/CarPatrolRoute/EditRoute?routeID={0}' 
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
                (decimal)RouteOwnerType.Car);

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
                              draw = string.IsNullOrEmpty(c.GEOMETRY) ?
                              string.Format(drawLine2, c.ROUTEID) :
                              string.Format(drawLine1, c.ROUTEID),
                              operating = string.Format(Operating, c.ROUTEID)
                          };

            return Json(new
            {
                sEcho = secho,
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
                ROUTEOWNERTYPE = (decimal)RouteOwnerType.Car
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
            if (route != null)
            {
                VMRoute vmRoute = new VMRoute
                {
                    RouteID = route.ROUTEID,
                    SSDDID = route.SSDDID,
                    SSZDID = route.SSZDID,
                    RouteName = route.ROUTENAME,
                    Geometry = route.GEOMETRY,
                    RouteDescription = route.ROUTEDESCRIPTION,

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
                string unitList = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == vmRoute.SSDDID).UNITNAME;
                ViewBag.unitList = unitList;

                //获得中队名称
                string unitName = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == vmRoute.SSZDID).UNITNAME;
                ViewBag.unitName = unitName;

                return View(THIS_VIEW_PATH + "EditRoute.cshtml", vmRoute);
            }
            else {
                return RedirectToAction("Index");
            }
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
        public string  DeleteRoute()
        {
            string strRouteID = Request.QueryString["routeID"];
            decimal routeID = 0;
            decimal.TryParse(strRouteID, out routeID);
            IQueryable<XCJGCARTASK> task = PartrolCarTaskBLL.GetXCJGCARTASKByRouteID(routeID);
            int dd = task.Count();
            if (dd==0)
            {
                if (strRouteID != null && strRouteID!="")
                {
                    PatrolRouteBLL.DeleteRoute(routeID);
                    return "1";
                }
                else
                {
                    return "2";
                }
               
            }
            else
            {
                return "0";
            } 
        }

        public string DeleteCarTask(decimal routeID)
        {
            IQueryable<XCJGCARTASK> task = PartrolCarTaskBLL.GetXCJGCARTASKByRouteID(routeID);
            if (task.Count() > 0)
            {
                foreach (var item in task)
                {
                    PartrolCarTaskBLL.DeleteCarTask(item.CARID);
                }
                PatrolRouteBLL.DeleteRoute(routeID);
                return "1";
            }
            else
            {
                return "2";
            }
            
        }
    }
}
