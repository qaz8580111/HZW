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
using Taizhou.PLE.Model.CustomModels;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{

    /// <summary>
    /// 先行登记保存证据物品处理通知书
    /// </summary>
    public class DocXXDJBCZJWPCLTZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long Rad)
        {
            XXDJBCZJWPCLTZS xxdjbczjwpcltzs = new XXDJBCZJWPCLTZS();
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;

            //文书编号
            xxdjbczjwpcltzs.BH = DocBuildBLL.XXDJBCZJWPCLTZSCode();

            //当事人
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    xxdjbczjwpcltzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    xxdjbczjwpcltzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }

            //先行登记保存证据通知书下拉框
            List<SelectListItem> list = DocBuildBLL.GetXXDJBCZJTZS(WIID).Select(t => new SelectListItem
            {
                Text = t.DOCBH,
                Value = t.DOCBH,
                Selected = false
            }).ToList();

            ViewBag.XXDJBCZJTZSList = list;

            return PartialView(THIS_VIEW_PATH + "XXDJBCZJWPCLTZS.cshtml", xxdjbczjwpcltzs);
        }
    }
}
