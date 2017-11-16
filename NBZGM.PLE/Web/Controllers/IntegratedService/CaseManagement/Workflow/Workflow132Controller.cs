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
using Web;
using Web.ViewModels.CaseViewModels;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow
{
    /// <summary>
    /// 符合听证条件，当事人提出听证申请
    /// </summary>
    public class Workflow132Controller : Controller
    {
        // GET: /Workflow130/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow132/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            return PartialView(THIS_VIEW_PATH + "Index.cshtml");
        }


        public ActionResult Commit(ViewModel32 ViewModel32)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(ViewModel32.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[ViewModel32.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form132.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form132.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form132.BZSM = ViewModel32.BZSM;

            activity.Submit();

            return RedirectToAction("PendingCaseList", "GeneralCase");
        }
    }
}
