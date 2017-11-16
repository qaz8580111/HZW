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
    public class Workflow113Controller : Controller
    {
        /// <summary>
        /// 撤案建议
        /// </summary>

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow113/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel13 viewModel13 = new ViewModel13
            {
                AIID = AIID,
                WIID = CaseForm.WIID
            };

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel13);
        }

        public ActionResult Commit(ViewModel13 ViewModel13)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(ViewModel13.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[ViewModel13.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form113.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form113.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form113.BZSM = ViewModel13.BZSM;

            activity.Submit();

            //短信内容
            string SMStoUserNAme = this.Request.Form["FGLDUserName"];
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
