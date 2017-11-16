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
using Taizhou.PLE.Model.CustomModels;
using Web;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    //查封扣押决定书
    public class DocCFKYJDSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;
            CFKYJDS cfkyjds = new CFKYJDS();

            cfkyjds.BH = DocBuildBLL.GetCFKYTZSCode();
            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    cfkyjds.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    cfkyjds.DSR = form101.OrgForm.FDDBRXM;
                }
            }


            //现场检查勘验笔录
            XCJCKYBL xcjckybl = DocBuildBLL.GetXCJCCYBL(WIID);

            if (xcjckybl != null)
            {
                cfkyjds.ZFRY1 = xcjckybl.JCKYR1;
                cfkyjds.ZFRY2 = xcjckybl.JCKYR2;
            }

            List<USER> ListUser = UserBLL.GetUsersByUserUnitID(SessionManager.User.UnitID);

            //执法人员1
            List<SelectListItem> ListUser1 = ListUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME,
                Value = t.USERNAME + "," + t.ZFZBH + "," + t.USERID,
                Selected = cfkyjds.ZFRY1 == t.USERID + "," + t.USERNAME + "," + t.ZFZBH ? true : false
            }).ToList();
            //执法人员2
            List<SelectListItem> ListUser2 = ListUser.Select(t => new SelectListItem
           {
               Text = t.USERNAME,
               Value = t.USERNAME + "," + t.ZFZBH + "," + t.USERID,
               Selected = cfkyjds.ZFRY2 == t.USERID + "," + t.USERNAME + "," + t.ZFZBH ? true : false
           }).ToList();
            ViewBag.ZFRY1 = ListUser1;
            ViewBag.ZFRY2 = ListUser2;
            return PartialView(THIS_VIEW_PATH + "CFKYJDS.cshtml", cfkyjds);
        }
    }
}
