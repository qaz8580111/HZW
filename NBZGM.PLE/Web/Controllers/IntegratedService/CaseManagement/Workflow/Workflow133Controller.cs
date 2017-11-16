using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;
using Web.ViewModels.CaseViewModels;
using Web.Workflows;


namespace Web.Controllers.IntegratedService.CaseManagement.Workflow
{
    public class Workflow133Controller : Controller
    {
        /// <summary>
        /// 文书意见
        /// </summary>
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow133/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel133 viewModel33 = new ViewModel133()
            {
                AIID = AIID,
                WIID = CaseForm.WIID,
                BZSM = "同意"
            };

            return PartialView(THIS_VIEW_PATH + "Index.cshtml", viewModel33);
        }

        public ActionResult Commit(ViewModel133 ViewModel133)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(ViewModel133.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[ViewModel133.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form133.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form133.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form133.BZSM = ViewModel133.BZSM;

            activity.Submit();

            //短信内容
            string SMStoUserNAme = this.Request.Form["ZBDYNAME"];
            string megContent = SMStoUserNAme + ",您在案件管理子系统中有一条新任务等待处理";
            //电话号码
            string phoneNumber = this.Request.Form["FSDX"];
            //发送短信
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                SMSUtility.SendMessage(phoneNumber, megContent + "[" + SessionManager.User.UserName + "]", DateTime.Now.Ticks);
            }

            return RedirectToAction("PendingCaseList", "GeneralCase");
        }

    }
}
