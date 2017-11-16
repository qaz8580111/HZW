using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPNewModels;
using Taizhou.PLE.Web.Process.NewXZSPProess;

namespace Web.Controllers.IntegratedService.ApprovalNewManagement.XZSPWorkflow
{
    public class NewXZSPWorkflow3Controller : Controller
    {
        //
        // GET: /XZSPWorkflow3/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewXZSPWorkflow/";
        TotalWorkflows ttwork = new TotalWorkflows();

        public ActionResult Index(string AIID, decimal ADID, string ID)
        {
            PLEEntities db = new PLEEntities();
            //获取第二流程中的派遣人id
            string pqr = db.XZSPNEWTABs.SingleOrDefault(t => t.ADID == 2 && t.AIID == AIID).PQR;

            //根据第二步中的派遣人id得到他的部门ID
            decimal tt = UnitBLL.GetUnitIDByUserID(decimal.Parse(pqr));

            List<SelectListItem> ZDDY = UserBLL.GetZDRYByUnitID(tt)
                 .Select(c => new SelectListItem()
                 {
                     Text = c.USERNAME,
                     Value = c.USERID.ToString(),
                 }).ToList();

            ZDDY.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });
            ViewBag.ZDDY = ZDDY;
            return PartialView(THIS_VIEW_PATH + "NewXZSPWorkflow3.cshtml", ttwork.Workflow3);
        }

        [HttpPost]
        public ActionResult Commit(Workflow3 Workflow3)
        {
            NewXZSPProess newxzspproess = new NewXZSPProess();
            newxzspproess.XZSPWorkflow3(Workflow3);
            return RedirectToAction("NewApproval", "NewApproval");
        }

    }
}
