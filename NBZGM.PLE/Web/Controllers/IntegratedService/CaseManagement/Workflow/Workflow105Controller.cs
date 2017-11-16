using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.BLL.UserBLLs;
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
    public class Workflow105Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow105/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;

            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel5 viewModel5 = new ViewModel5
            {
                AIID = AIID,
                WIID = CaseForm.WIID
            };

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel5);
        }

        public ActionResult Commit(ViewModel5 viewModel5)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel5.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel5.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            Form105 form5 = caseForm.FinalForm.Form105;
            Form102 form2 = caseForm.FinalForm.Form102;

            form5.Description = viewModel5.DCQZYJ;
            form5.ProcessUser = SessionManager.User;
            form5.ProcessTime = DateTime.Now;

            activity.Submit();

            //短信内容
            string SMStoUserNAme = this.Request.Form["XBDYNAME"];
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
