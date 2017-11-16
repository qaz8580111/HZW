using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.Web.Process.NewXZSPProess;
using Taizhou.PLE.Model.XZSPNewModels;
using Taizhou.PLE.BLL.NewXZSPWorkflows;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Model;
namespace Web.Controllers.IntegratedService.ApprovalNewManagement.XZSPWorkflow
{
    public class NewXZSPWorkflow2Controller : Controller
    {
        //
        // GET: /XZSPWorkflow2/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewXZSPWorkflow/";
        TotalWorkflows ttwork = new TotalWorkflows();
        public ActionResult Index(string AIID, decimal ADID, string ID)
        {

            PLEEntities db = new PLEEntities();
            //获取第一流程中的派遣人id
            string pqr = db.XZSPNEWTABs.SingleOrDefault(t => t.ADID == 1 && t.AIID == AIID).PQR;

            //根据第一步中的派遣人id得到他的部门ID
            decimal tt = UnitBLL.GetUnitIDByUserID(decimal.Parse(pqr));

            //根据部门ID得到父类部门ID
            decimal FID = UnitBLL.GetParentIDByUnitID(UnitBLL.GetUnitIDByUserID(decimal.Parse(pqr)));

            //根据父类ID获取所有的中队
            List<SelectListItem> ZSYDDYZD = UnitBLL.GetZDUnitsByParentID(FID)
                .Select(c => new SelectListItem()
                {
                    Text = c.UNITNAME,
                    Value = c.UNITID.ToString(),
                }).ToList();
            ZSYDDYZD.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });
            ViewBag.ZSYDDYZD = ZSYDDYZD;
            return PartialView(THIS_VIEW_PATH + "NewXZSPWorkflow2.cshtml", ttwork.Workflow2);
        }

        [HttpPost]
        public ActionResult Commit(Workflow2 Workflow2)
        {
            NewXZSPProess newxzspproess = new NewXZSPProess();
            newxzspproess.XZSPWorkflow2(Workflow2);
            return RedirectToAction("NewApproval", "NewApproval");
        }

        /// <summary>
        /// 根据中队获取中队人员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetZDry(decimal ZDID)
        {
            IQueryable<USER> unit = UserBLL.GetAllUsers().Where(a => a.UNITID == ZDID&&a.USERPOSITIONID==8);
            var list = from result in unit
                       select new
                       {
                           Value = result.USERID,
                           Text = result.USERNAME
                       };

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
