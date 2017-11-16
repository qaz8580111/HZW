using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Common.XZSP;
using Taizhou.PLE.BLL.XZSPBLLs;
namespace Web.Controllers.IntegratedService.ApprovalNewManagement
{
    public class NewApprovalController : Controller
    {
        //
        // GET: /Approval/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewApproval/";
        public ActionResult Index()
        {
            return View(); 
        } 
        /// <summary>
        /// 行政审批新的待审批列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ApprovalNewList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<XZSPNEWDBTAB> instances = ActivityInstanceBLL
                 .GetPendNewActivityList(SessionManager.User.UserID.ToString())
                 .OrderBy(t => t.ADID);
            List<XZSPNEWDBTAB> XZSPNewPenddingTask = instances
             .Skip((int)iDisplayStart.Value)
             .Take((int)iDisplayLength.Value).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in XZSPNewPenddingTask
                       select new
                       {
                           ADID = t.ADID,
                           AIID = t.AIID,
                           SEQNO = seqno++,
                           ADName = t.ADName,
                           EventTitle=t.EventTitle,
                           EventDescription=t.EventDescription,
                           CreateTime = string.Format("{0:MM-dd HH:mm:ss}", t.CreateTime),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CreateTime)
                       };
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = instances.Count(),
                iTotalDisplayRecords = instances.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 事项审批
        /// </summary>
        public ActionResult NewApproval()
        {
            return View(THIS_VIEW_PATH + "NewApproval.cshtml");
        }

        /// <summary>
        /// 行政审批新的已办审批列表
        /// </summary>
        /// <returns></returns>
        public JsonResult ApprovalNewAchivedList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<XZSPNEWDBTAB> instances = ActivityInstanceBLL
                 .GetPendNewAchivedList(SessionManager.User.UserID.ToString())
                 .OrderBy(t => t.ADID);
            List<XZSPNEWDBTAB> XZSPNewPenddingTask = instances
             .Skip((int)iDisplayStart.Value)
             .Take((int)iDisplayLength.Value).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in XZSPNewPenddingTask
                       select new
                       {
                           ADID = t.ADID,
                           AIID = t.AIID,
                           SEQNO = seqno++,
                           ADName = t.ADName,
                           EventTitle = t.EventTitle,
                           EventDescription = t.EventDescription,
                           CreateTime = string.Format("{0:MM-dd HH:mm:ss}", t.CreateTime),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CreateTime)
                       };
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = instances.Count(),
                iTotalDisplayRecords = instances.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult NewAchived() 
        {
            return View(THIS_VIEW_PATH + "NewAchived.cshtml");
        }
    }
}
