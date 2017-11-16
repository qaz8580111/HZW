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


namespace Web.Controllers.IntegratedService.CaseManagement.Document
{
    //解除查封（扣押）通知书
    public class DocJCCFKYTZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;
            JCCFKYTZS jccfkytzs = new JCCFKYTZS();
            //判断当事人类型
            jccfkytzs.BH = DocBuildBLL.GetJCCFKYTZSCode();
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    jccfkytzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    jccfkytzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }
            List<SelectListItem> list = DocBuildBLL.GetCFKYTZSList(WIID).ToList().Select(t => new SelectListItem
            {
                Text = t.DOCBH,
                Value = t.DOCBH,
                Selected = false
            }).ToList();

            List<USER> ListUser = UserBLL.GetUsersByUserUnitID(SessionManager.User.UnitID);
            //执法人员1
            List<SelectListItem> ListUser1 = ListUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME + "(" + t.ZFZBH + ")",
                Value = t.USERNAME + "," + t.ZFZBH + "," + t.USERID,
                //Selected = cfkytzs.ZFRY1 == t.USERID + "," + t.USERNAME + "," + t.ZFZBH ? true : false
            }).ToList();
            //执法人员2
            List<SelectListItem> ListUser2 = ListUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME + "(" + t.ZFZBH + ")",
                Value = t.USERNAME + "," + t.ZFZBH + "," + t.USERID,
                //Selected = cfkytzs.ZFRY2 == t.USERID + "," + t.USERNAME + "," + t.ZFZBH ? true : false
            }).ToList();
            ViewBag.ZFRY1 = ListUser1;
            ViewBag.ZFRY2 = ListUser2;


            //查封（扣押）通知书
            ViewBag.CFKYTZSList = list;

            return PartialView(THIS_VIEW_PATH + "JCCFKYTZS.cshtml", jccfkytzs);
        }
    }
}
