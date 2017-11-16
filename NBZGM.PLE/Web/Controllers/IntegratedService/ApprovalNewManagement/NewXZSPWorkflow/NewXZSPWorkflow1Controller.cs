using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.Model.XZSPNewModels;
using Taizhou.PLE.Web.Process.NewXZSPProess;
using Taizhou.PLE.Model;

namespace Web.Controllers.IntegratedService.ApprovalNewManagement.XZSPWorkflow
{
    public class NewXZSPWorkflow1Controller : Controller
    {
        //
        // GET: /XZSPWorkflow1/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewXZSPWorkflow/";
        [HttpGet]
        public ActionResult Index(string ADID, string AIID, string ID)
        {
            List<SelectListItem> ZSYDD = UnitBLL.GetUnitByUnitTypeID(6).Select(t => new SelectListItem()
            {
                Text = t.UNITNAME,
                Value = t.UNITID.ToString()
            }).ToList();
            ZSYDD.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });
            ViewBag.ZSYDD = ZSYDD;
            Workflow1 Workflow1 = new Workflow1();
            Workflow1.ADID = decimal.Parse(ADID);
            Workflow1.AIID = AIID;
            Workflow1.ID = ID;
            return PartialView(THIS_VIEW_PATH + "NewXZSPWorkflow1.cshtml", Workflow1);
        }

        [HttpPost]
        public ActionResult Commit(Workflow1 Workflow1)
        {
            NewXZSPProess newxzspproess = new NewXZSPProess();
            newxzspproess.XZSPWorkflow1(Workflow1);
            return RedirectToAction("NewApproval", "NewApproval");
        }

        /// <summary>
        /// 根据大队获取大队人员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDDry(decimal DDID)
        {
            IQueryable<USER> unit = UserBLL.GetAllUsers().Where(a=>a.UNITID == DDID);
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
