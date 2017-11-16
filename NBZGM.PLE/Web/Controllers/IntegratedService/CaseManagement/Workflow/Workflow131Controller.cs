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
    /// //要求陈述申辩
    /// </summary>
    public class Workflow131Controller : Controller
    {

        // GET: /Workflow130/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow131/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            return PartialView(THIS_VIEW_PATH + "Index.cshtml");
        }


        public ActionResult Commit(ViewModel31 ViewModel31)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(ViewModel31.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[ViewModel31.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form131.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form131.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form131.BZSM = ViewModel31.BZSM;

            activity.Submit();

            return RedirectToAction("PendingCaseList", "GeneralCase");
        }

    }
}
