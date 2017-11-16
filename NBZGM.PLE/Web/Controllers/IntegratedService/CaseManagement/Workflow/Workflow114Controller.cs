using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;
using Web.ViewModels.CaseViewModels;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow
{
    public class Workflow114Controller : Controller
    {
        /// <summary>
        /// 行政处罚事先告知书
        /// </summary>

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow114/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel14 viewModel14 = new ViewModel14
            {
                AIID = AIID,
                WIID = CaseForm.WIID
            };

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel14);
        }

        public ActionResult Commit(ViewModel14 viewModel14)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel14.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel14.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form114.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form114.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form114.BZSM = viewModel14.BZSM;

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
