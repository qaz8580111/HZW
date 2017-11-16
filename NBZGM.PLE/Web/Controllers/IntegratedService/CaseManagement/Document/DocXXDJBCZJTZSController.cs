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

namespace Web.Controllers.IntegratedService.CaseManagement.Document
{
    /// <summary>
    /// 先行登记保存证据通知书
    /// </summary>
    public class DocXXDJBCZJTZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long Rad)
        {
            XXDJBCZJTZS xxdjbczjtzs = new XXDJBCZJTZS();
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;

            ViewBag.WICode = caseworkflow.CaseForm.WICode;
            //文书编号
            xxdjbczjtzs.BH = DocBuildBLL.XXDJBCZJTZSCode();
            //当事人
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    xxdjbczjtzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    xxdjbczjtzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }

            //案由
            xxdjbczjtzs.AY = form101.AY;

            return PartialView(THIS_VIEW_PATH + "XXDJBCZJTZS.cshtml", xxdjbczjtzs);
        }



        public JsonResult GetXXDJBCZJTZS(string WSBH)
        {
            if (string.IsNullOrWhiteSpace(WSBH))
            {
                return null;
            }
            DOCINSTANCE docinstance = DocBLL.GetDocInstanceByWSBH(WSBH);

            if (docinstance == null)
            {
                return null;
            }
            XXDJBCZJTZS xxdjbczjtzs = (XXDJBCZJTZS)Serializer.Deserialize(docinstance.ASSEMBLYNAME,
                docinstance.TYPENAME, docinstance.VALUE);

            var xxdjbczjwpcltzs = new
            {
                //登记通知时间（上一环节的文书落款时间）
                WSLKSJ = xxdjbczjtzs.WSLKSJ == null ?
                    "" : xxdjbczjtzs.WSLKSJ.Value.ToString("yyyy-MM-dd"),
                //开始保存期限
                BCKSSJ = xxdjbczjtzs.BCKSSJ != null ?
                    xxdjbczjtzs.BCKSSJ.Value.ToString("yyyy-MM-dd") : "",
                //结束保存期限
                BCJSSJ = xxdjbczjtzs.BCJSSJ != null ?
                    xxdjbczjtzs.BCJSSJ.Value.ToString("yyyy-MM-dd") : "",
                XXDJBCZJQDList = xxdjbczjtzs.XXDJBCZJQDList
            };

            return Json(xxdjbczjwpcltzs, JsonRequestBehavior.AllowGet);
        }
    }
}
