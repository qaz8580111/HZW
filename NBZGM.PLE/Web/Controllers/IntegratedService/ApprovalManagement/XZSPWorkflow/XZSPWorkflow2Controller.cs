using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.ExpandInfoForm101;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Web.Process.XZSPProcess;

namespace Web.Controllers.IntegratedService.ApprovalManagement.XZSPWorkflow
{
    public class XZSPWorkflow2Controller : Controller
    {
        //执法中队派遣核查
        // GET: /XZSPWorkflow2/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/XZSPWorkflow/";

        public ActionResult Index(string WIID, string AIID, string ADID, string APID, string WDID, XZSPForm xzspForm)
        {
            XZSPAPPROVALPROJECT approvalProject = ApprovalProjectBLL
               .GetApprovalProjectByAPID(decimal.Parse(APID));
            //申请项目标识
            decimal projectID = approvalProject.PROJECTID.Value;
            //申请事项标识
            decimal apid = decimal.Parse(APID);
            //获取所有的审批项目
            ViewBag.APList = ProjectNameBLL.GetAllProjectName().Find(t => t.PROJECTID == projectID).PROJECTNAME;
            //获取所有的审批事项
            ViewBag.APSXList = ApprovalProjectBLL
                .GetApprovalProjectByProjectID(projectID).ToList().Find(t => t.APID == apid).APNAME;

            //直属一大队
            ViewBag.ZSYDD = UnitBLL.GetUnitNameByUnitID(Convert.ToDecimal(xzspForm.FinalForm.Form101.ZFDDID));

            //直属中队
            ViewBag.ZSYDDYZD = UnitBLL.GetUnitsByParentID(Convert.ToDecimal(xzspForm.FinalForm.Form101.ZFDDID)).Find(t => t.UNITID.ToString() == xzspForm.FinalForm.Form101.ZFZDID).UNITNAME;

            //中队标识
            string strZDID = xzspForm.FinalForm.Form101.ZFZDID;
            //获取中队下的队员
            List<SelectListItem> PQDY = UserBLL
                .GetUsersByUnitID(decimal.Parse(strZDID)).ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.USERNAME,
                    Value = c.USERID.ToString()
                }).ToList();

            List<SelectListItem> PQDY2 = UserBLL
                .GetUsersByUnitID(decimal.Parse(strZDID)).ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.USERNAME,
                    Value = c.USERID.ToString()
                }).ToList();

            ViewBag.PQDY = PQDY;
            ViewBag.PQDY2 = PQDY2;
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.APID = APID;
            ViewBag.WDID = WDID;
            ViewBag.APName = approvalProject.APNAME;
            ViewBag.Form101 = xzspForm.FinalForm.Form101;

            return View(THIS_VIEW_PATH + "XZSPWorkflow2.cshtml", xzspForm.FinalForm.Form102);
        }

        public ActionResult Commit(Form102 form102)
        {
            XZSPProcess xzspprocess = new XZSPProcess();
            xzspprocess.wiID = this.Request.Form["WIID"];
            xzspprocess.aiID = this.Request.Form["AIID"];
            xzspprocess.adID = this.Request.Form["ADID"];
            xzspprocess.apID = this.Request.Form["APID"];
            xzspprocess.wdID = this.Request.Form["WDID"];
            string state = this.Request.Form["bc"].ToString();

            XZSPProcess.XZSPFrom102Submmit(xzspprocess, form102, state);

            if (state == "1")
            {
                return RedirectToAction("XZSPWorkflowProcess", "XZSPWorkflow",
                 new
                 {
                     WIID = xzspprocess.wiID,
                     APID = xzspprocess.apID,
                     WDID = xzspprocess.wdID
                 });
            }
            return RedirectToAction("Approval", "Approval");
        }
    }
}
