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
    public class Workflow127Controller : Controller
    {
        //
        // GET: /Workflow127/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow127/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            return PartialView(THIS_VIEW_PATH + "Index.cshtml");
        }


        public ActionResult Commit(ViewModel27 ViewModel27)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(ViewModel27.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[ViewModel27.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form127.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form127.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form127.BZSM = ViewModel27.BZSM;

            activity.Submit();

            return RedirectToAction("PendingCaseList", "GeneralCase");
        }
    }
}
