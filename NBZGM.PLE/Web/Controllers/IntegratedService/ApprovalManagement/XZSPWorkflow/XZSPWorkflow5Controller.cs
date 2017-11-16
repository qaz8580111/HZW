using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.ExpandInfoForm101;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Web.Process.XZSPProcess;

namespace Web.Controllers.IntegratedService.ApprovalManagement.XZSPWorkflow
{
    public class XZSPWorkflow5Controller : Controller
    {
        //承办人审核申请
        // GET: /XZSPWorkflow5/

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
            //队员标识
            decimal strDYID = xzspForm.FinalForm.Form102.PQDYID;
            decimal strDYID2 = xzspForm.FinalForm.Form102.PQDYID2;
            //获取中队下的队员
            ViewBag.PQDY = UserBLL.GetUsersByUnitID(decimal.Parse(strZDID)).ToList().Find(t => t.USERID == strDYID).USERNAME;
            ViewBag.PQDY2 = UserBLL.GetUsersByUnitID(decimal.Parse(strZDID)).ToList().Find(t => t.USERID == strDYID2).USERNAME;


            //获取该大队下的承办机构(综合科)
            string strDDID = xzspForm.FinalForm.Form101.ZFDDID;
            IQueryable<USER> users = UserBLL.GetUsersByUnitID(810)
                .Where(t => t.USERPOSITIONID == (decimal)UserPositionEnum.ZHKKZ);

            ViewBag.users = users.ToList().Select(t => new SelectListItem()
            {
                Text = t.USERNAME,
                Value = t.USERID.ToString()
            }).ToList();

            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.APID = APID;
            ViewBag.WDID = WDID;
            ViewBag.APName = approvalProject.APNAME;
            ViewBag.Form101 = xzspForm.FinalForm.Form101;
            ViewBag.Form102 = xzspForm.FinalForm.Form102;
            ViewBag.Form103 = xzspForm.FinalForm.Form103;
            ViewBag.Form104 = xzspForm.FinalForm.Form104;
            ViewBag.FeedBackForm = xzspForm.FinalForm.FeedBackForm;

            return View(THIS_VIEW_PATH + "XZSPWorkflow5.cshtml", xzspForm.FinalForm.Form105);
        }

        public ActionResult Commit(Form105 form105)
        {
            HttpFileCollectionBase files = Request.Files;
            XZSPProcess xzspprocess = new XZSPProcess();
            DateTime dt = DateTime.Now;

            xzspprocess.wiID = this.Request.Form["WIID"];
            xzspprocess.aiID = this.Request.Form["AIID"];
            xzspprocess.adID = this.Request.Form["ADID"];
            xzspprocess.apID = this.Request.Form["APID"];
            xzspprocess.wdID = this.Request.Form["WDID"];
            xzspprocess.jsonExpandInfoForm = this.Request.Form["jsonKZXX"];
            string state = this.Request.Form["bc"];
            string strOriginalPath = ConfigurationManager.AppSettings["OriginalPath"];

            Dictionary<string, string> fileNameList = new Dictionary<string, string>();
            foreach (string fName in files)
            {
                fileNameList.Add(fName + "Text", string.IsNullOrWhiteSpace(this.Request.Form[fName + "Text"]) ?
                       "未命名附件" : this.Request.Form[fName + "Text"]);
            }
            List<AttachmentModel> materials = XZSPProcess.GetAttachmentList(files, strOriginalPath, fileNameList);

            XZSPProcess.XZSPFrom105Submmit(xzspprocess, materials, form105, state);
            
            if (state == "1") //保存
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
