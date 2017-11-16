using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
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
    /// <summary>
    /// 主办队员做出处罚决定
    /// </summary>
    public class Workflow116Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow116/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel16 viewModel16 = new ViewModel16
            {
                AIID = AIID,
                WIID = CaseForm.WIID,
                ZBDYYJ = "同意"
            };

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel16);
        }

        public ActionResult Commit(ViewModel16 viewModel16)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel16.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel16.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form116.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form116.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form116.ZBDYYJ = viewModel16.ZBDYYJ;
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
