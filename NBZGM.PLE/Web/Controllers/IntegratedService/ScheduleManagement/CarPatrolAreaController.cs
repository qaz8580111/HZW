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
    public class CarPatrolAreaController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/CarPatrolArea/";

        //
        // GET: /执法车辆巡查区域管理/

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 分页显示执法车辆巡查区域列表
        /// </summary>
        public JsonResult GetCarAreas(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            string areaName = Request.QueryString["areaName"];

            string drawLine1 = @"
<a href='/CarPatrolArea/EditArea?areaID={0}'
title='绘制区域'>已绘制</a>
";
            string drawLine2 = @"
<a href='/CarPatrolArea/EditArea?areaID={0}'
title='绘制区域'>未绘制</a>
";
            string Operating = "<div>";

            Operating += @"
<a class='btn btn-primary btn-small' 
href='/CarPatrolArea/EditArea?areaID={0}' 
title='修改区域'><i class='icon-edit padding-null'></i></a>
";
            Operating += @"
<a class='btn btn-danger btn-small' href='#' 
title='删除区域' onclick='DeleteArea({0})'>
<i class='icon-trash padding-null'></i></a>
";
            Operating += "</div>";

            IQueryable<XCJGAREA> areas = PatrolAreaBLL
                .GetPatrolAreas(SessionManager.User.UserID,
                (decimal)AreaOwnerType.Car);

            if (!string.IsNullOrWhiteSpace(areaName))
            {
                areas = areas.Where(t => t.AREANAME.Contains(areaName));
            }

            List<XCJGAREA> resultListC =
                (from m in areas
                     .OrderByDescending(t => t.AREAID)
                 select m).ToList();

            List<XCJGAREA> resultList =
                (from m in areas
                     .OrderByDescending(t => t.AREAID)
                     .Skip((int)iDisplayStart)
                     .Take((int)iDisplayLength)
                 select m).ToList();

            var results = from c in resultList
                          select new
                          {
                              areaName = c.AREANAME,
                              areaDescription = c.AREADESCRIPTION,
                              draw = string.IsNullOrEmpty(c.GEOMETRY) ?
                              string.Format(drawLine2, c.AREAID) :
                              string.Format(drawLine1, c.AREAID),
                              operating = string.Format(Operating, c.AREAID)
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
        /// 添加区域
        /// </summary>
        public ActionResult AddArea()
        {
           
                decimal parentID = UnitBLL
                    .GetParentIDByUnitID(SessionManager.User.UnitID);
                ViewBag.SSDD = UnitBLL.GetUnitNameByUnitID(parentID);
                ViewBag.unitName = SessionManager.User.UnitName;
            return View(THIS_VIEW_PATH + "AddArea.cshtml");
        }

        /// <summary>
        /// 提交添加区域表单
        /// </summary>
        [HttpPost]
        public ActionResult CommitAddArea(VMArea vmArea)
        {
            decimal parentID = UnitBLL
                .GetParentIDByUnitID(SessionManager.User.UnitID);

            XCJGAREA area = new XCJGAREA
            {
                SSDDID = parentID,
                SSZDID = SessionManager.User.UnitID,
                AREANAME = vmArea.AreaName,
                AREADESCRIPTION = vmArea.AreaDescription,
                GEOMETRY = vmArea.Geometry,
                AREAOWNERTYPE = (decimal)AreaOwnerType.Car
            };

            PatrolAreaBLL.AddArea(area);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 修改区域
        /// </summary>
        public ActionResult EditArea()
        {
            string strAreaID = this.Request.QueryString["areaID"];

            XCJGAREA area = PatrolAreaBLL
                .GetXCJGAreaByAreaID(decimal.Parse(strAreaID));
            if (area != null)
            {
                VMArea vmArea = new VMArea
                {
                    AreaID = area.AREAID,
                    SSDDID = area.SSDDID,
                    SSZDID = area.SSZDID,
                    AreaName = area.AREANAME,
                    AreaDescription = area.AREADESCRIPTION,
                    Geometry = area.GEOMETRY
                };

                ////获取所有的单位
                //List<SelectListItem> unitList = UnitBLL.GetAllUnits().ToList()
                //    .Select(c => new SelectListItem
                //    {
                //        Text = c.UNITNAME,
                //        Value = c.UNITID.ToString()
                //    }).ToList();

                //ViewBag.unitList = unitList;

                //获得大队名称
                string unitList = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == vmArea.SSDDID).UNITNAME;
                ViewBag.unitList = unitList;

                //获得中队名称
                string unitName = UnitBLL.GetAllUnits().ToList().FirstOrDefault(t => t.UNITID == vmArea.SSZDID).UNITNAME;
                ViewBag.unitName = unitName;

                return View(THIS_VIEW_PATH + "EditArea.cshtml", vmArea);
            }
            else {
                return RedirectToAction("Index");
            }
         

           
        }

        /// <summary>
        /// 提交修改区域表单
        /// </summary>
        [HttpPost]
        public ActionResult CommitEditArea(VMArea vmArea)
        {
            XCJGAREA area = new XCJGAREA
            {
                AREAID = vmArea.AreaID,
                AREANAME = vmArea.AreaName,
                GEOMETRY = vmArea.Geometry,
                AREADESCRIPTION = vmArea.AreaDescription
            };

            PatrolAreaBLL.ModifyArea(area);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 根据区域标识删除区域
        /// </summary>
        public string  DeleteArea()
        {
            string strAreaID = this.Request.QueryString["areaID"];
            decimal areaID = 0;
            decimal.TryParse(strAreaID, out areaID);
            IQueryable<XCJGCARTASK> task = PartrolCarTaskBLL.GetXCJGCARTASKByAREAID(areaID);
            if (task.Count()==0)
            {
                if (strAreaID!= null && strAreaID!="")
                {
                    PatrolAreaBLL.DeleteArea(areaID);
                    return "1";
                }
                else {
                    return "2";
                }
            }
            else
            {
                return "0";
            }
           

           
        }

        public string DeleteAREATask(decimal areaID)
        {
            IQueryable<XCJGCARTASK> task = PartrolCarTaskBLL.GetXCJGCARTASKByAREAID(areaID);
            if (task.Count() > 0)
            {
                foreach (var item in task)
                {
                    PartrolCarTaskBLL.DeleteCarTaskByAREAID(Convert.ToDecimal(item.AREAID));
                }
                PatrolAreaBLL.DeleteArea(areaID);
                return "1";
            }
            else
            {
                return "2";
            }

        }
    }
}
