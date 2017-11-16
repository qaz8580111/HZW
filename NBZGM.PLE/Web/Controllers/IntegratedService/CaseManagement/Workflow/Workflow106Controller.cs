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
    public class Workflow106Controller : Controller
    {

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow106/";

        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;

            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel6 viewModel6 = new ViewModel6
            {
                AIID = AIID,
                WIID = CaseForm.WIID
            };

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel6);
        }

        public ActionResult Commit(ViewModel6 viewModel6)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel6.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel6.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            Form106 form6 = caseForm.FinalForm.Form106;

            form6.Approved = viewModel6.Approved;
            form6.ProcessUser = SessionManager.User;
            form6.ProcessTime = DateTime.Now;
            form6.QRYJ = viewModel6.QRYJ;

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
