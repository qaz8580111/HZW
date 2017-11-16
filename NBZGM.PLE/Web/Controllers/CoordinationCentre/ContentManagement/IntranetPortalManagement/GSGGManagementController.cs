using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.DBHelper;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.BLL.ZFRYBLL;
using Taizhou.PLE.Model.GSGGModels;
using Taizhou.PLE.BLL.WorkFlowBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.BLL.CaseBLLs;

namespace Web.Controllers.CoordinationCentre.ContentManagement.IntranetPortalManagement
{
    public class GSGGManagementController : Controller
    {
     
        public const string THIS_VIEW_PATH = @"~/Views/CoordinationCentre/ContentManagement/IntranetPortalManagement/GSGGManagement/";

        /// <summary>
        /// 获得公示公告的详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowGSGGList() 
        {
            ViewBag.ZFRYCount = ZFDYCount();
            ViewBag.zfsjListCount = zfsjListCount();
            ViewBag.xzspListCount = XZSPListCount();
            ViewBag.caseListCount = CaseListCount();
            ViewBag.simpCaseCount = simpCaseCount();
            //ViewBag.CaseParkCount = CaseParkCount();
            string NotifyID=Request["NOTIFY_ID"];
            GSGGPendModels gg = DBHelper.ShowGSGGList(NotifyID);
            return View(THIS_VIEW_PATH + "GSGGManagement.cshtml",gg);
        }
        /// <summary>
        /// 获取执法队员数量
        /// </summary>
        /// <returns></returns>
        public static int ZFDYCount()
        {
            int A = ZFRYBLL.GetZFGKUSERLATESTPOSITIONSByMum();
            return A;
        }
        /// <summary>
        /// 获取一般案件数量
        /// </summary>
        /// <returns></returns>
        public static int CaseListCount()
        {
            int A = WorkflowBLL.CaseListCount();
            return A;
        }
        /// <summary>
        /// 获得行政审批的数目
        /// </summary>
        /// <returns></returns>
        public static int XZSPListCount()
        {
            int A = WorkflowInstanceBLL
                .XZSPListCount();
            return A;
        }
        /// <summary>
        /// 获得执法事件的数目
        /// </summary>
        /// <returns></returns>
        public static int zfsjListCount()
        {
            int A = ZFSJWorkflowInstanceBLL
                .zfsjListCount();
            return A;
        }
        /// <summary>
        /// 获取简易事件的条数
        /// </summary>
        /// <returns></returns>
        public static int simpCaseCount()
        {

            int A = SimpleCaseBLL
                .simpCaseCount();
            return A;
        }
        /// <summary>
        /// 获得违停案件的条数
        /// </summary>
        /// <returns></returns>
        public static int CaseParkCount()
        {
            int A = ParkingCaseBLL
                .CaseParkCount();
            return A;
        }
    }
}
