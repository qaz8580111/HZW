using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.Model.XZSPNewModels;
using Taizhou.PLE.Web.Process.NewXZSPProess;

namespace Web.Controllers.IntegratedService.ApprovalNewManagement.XZSPWorkflow
{
    public class NewXZSPWorkflow5Controller : Controller
    {
        //
        // GET: /XZSPWorkflow5/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewXZSPWorkflow/";
        TotalWorkflows ttwork = new TotalWorkflows();
        public ActionResult Index(string AIID, decimal ADID, string ID)
        {
            ttwork.Workflow5 = new Workflow5();
            return PartialView(THIS_VIEW_PATH + "NewXZSPWorkflow5.cshtml", ttwork.Workflow5);
        }

        [HttpPost]
        public ActionResult Commit(Workflow5 Workflow5)
        {
            NewXZSPProess newxzspproess = new NewXZSPProess();
            Workflow5.ZDZ= SessionManager.User.UserID.ToString();
            newxzspproess.XZSPWorkflow5(Workflow5);
            return RedirectToAction("NewApproval", "NewApproval");
        }
    }
}
