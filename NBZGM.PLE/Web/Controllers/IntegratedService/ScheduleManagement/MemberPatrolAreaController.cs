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
    public class MemberPatrolAreaController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ScheduleManagement/MemberPatrolArea/";

        //
        // GET: /执法队员巡查区域管理/

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 分页显示执法队员巡查区域列表
        /// </summary>
        public JsonResult GetMemberAreas(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            string areaName = Request.QueryString["areaName"];

            string drawLine1 = @"
<a href='/MemberPatrolArea/EditArea?areaID={0}'
title='绘制区域'>已绘制</a>
";
            string drawLine2 = @"
<a href='/MemberPatrolArea/EditArea?areaID={0}'
title='绘制区域'>未绘制</a>
";
            string Operating = "<div>";

            Operating += @"
<a class='btn btn-primary btn-small' 
href='/MemberPatrolArea/EditArea?areaID={0}' 
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
                (decimal)AreaOwnerType.Member);

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

            IList<USER> userList = UserBLL.GetAllUsers().ToList();
            ViewBag.userList = userList;

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

            string USERIDS = Request["USERIDS_Name"];
            if (!string.IsNullOrEmpty(USERIDS))
                USERIDS = "," + USERIDS + ",";

            XCJGAREA area = new XCJGAREA
            {
                SSDDID = parentID,
                SSZDID = SessionManager.User.UnitID,
                AREANAME = vmArea.AreaName,
                AREADESCRIPTION = vmArea.AreaDescription,
                GEOMETRY = vmArea.Geometry,
                AREAOWNERTYPE = (decimal)AreaOwnerType.Member,
                XCTIME = vmArea.XCTIME,
                USERIDS = USERIDS
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

            VMArea vmArea = new VMArea
            {
                AreaID = area.AREAID,
                SSDDID = area.SSDDID,
                SSZDID = area.SSZDID,
                AreaName = area.AREANAME,
                AreaDescription = area.AREADESCRIPTION,
                Geometry = area.GEOMETRY,
                XCTIME = Convert.ToDateTime(area.XCTIME),
                USERIDS = area.USERIDS
            };
            IList<USER> userList = UserBLL.GetAllUsers().ToList();
            ViewBag.userList = userList;

            return View(THIS_VIEW_PATH + "EditArea.cshtml", vmArea);
        }

        /// <summary>
        /// 提交修改区域表单
        /// </summary>
        [HttpPost]
        public ActionResult CommitEditArea(VMArea vmArea)
        {
            string USERIDS = Request["USERIDS_Name"];
            if (!string.IsNullOrEmpty(USERIDS))
                USERIDS = "," + USERIDS + ",";

            XCJGAREA area = new XCJGAREA
            {
                AREAID = vmArea.AreaID,
                AREANAME = vmArea.AreaName,
                GEOMETRY = vmArea.Geometry,
                AREADESCRIPTION = vmArea.AreaDescription,
                XCTIME = vmArea.XCTIME,
                USERIDS = USERIDS
            };

            PatrolAreaBLL.ModifyArea(area);

            return RedirectToAction("Index");
        }


        public int Delete()
        {
            string AreaID = this.Request.QueryString["areaID"];
            decimal areaID = 0;
            decimal.TryParse(AreaID, out areaID);
            IQueryable<XCJGUSERTASK> task = PatrolUserTaskBLL.GetXCJGUserTasks().Where(a => a.AREAID == areaID);
            if (task != null && task.Count() > 0)
            {
                return 1;
            }
            else
            {
                PatrolAreaBLL.DeleteArea(areaID);
                return 0;
            }


        }


        /// <summary>
        /// 根据区域标识删除区域
        /// </summary>
        public bool DeleteArea()
        {
            string strAreaID = this.Request.QueryString["areaID"];

            decimal areaID = 0;
            decimal.TryParse(strAreaID, out areaID);
            IQueryable<XCJGUSERTASK> task = PatrolUserTaskBLL.GetXCJGUserTasks().Where(a => a.AREAID == areaID);
            if (task != null)
            {
                foreach (var item in task)
                {
                    PatrolUserTaskBLL.DeleteUserTask(item.USERID, item.TASKDATE);
                }
                PatrolAreaBLL.DeleteArea(areaID);
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
