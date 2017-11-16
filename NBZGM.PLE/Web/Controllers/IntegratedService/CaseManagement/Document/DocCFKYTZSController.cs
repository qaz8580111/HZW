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
    //查封扣押通知书
    public class DocCFKYTZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;
            CFKYTZS cfkytzs = new CFKYTZS();

            cfkytzs.BH = DocBuildBLL.GetCFKYTZSCode();
            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    cfkytzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    cfkytzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }
            cfkytzs.WFXW = form101.AY;

            //现场检查勘验笔录
            XCJCKYBL xcjckybl = DocBuildBLL.GetXCJCCYBL(WIID);

            if (xcjckybl != null)
            {
                cfkytzs.ZFRY1 = xcjckybl.JCKYR1;
                cfkytzs.ZFRY2 = xcjckybl.JCKYR2;
            }

            List<USER> ListUser = UserBLL.GetUsersByUserUnitID(SessionManager.User.UnitID);

            //执法人员1
            List<SelectListItem> ListUser1 = ListUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME + "(" + t.ZFZBH + ")",
                Value = t.USERNAME + "," + t.ZFZBH + "," + t.USERID,
                Selected = cfkytzs.ZFRY1 == t.USERID + "," + t.USERNAME + "," + t.ZFZBH ? true : false
            }).ToList();
            //执法人员2
            List<SelectListItem> ListUser2 = ListUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME + "(" + t.ZFZBH + ")",
                Value = t.USERNAME + "," + t.ZFZBH + "," + t.USERID,
                Selected = cfkytzs.ZFRY2 == t.USERID + "," + t.USERNAME + "," + t.ZFZBH ? true : false
            }).ToList();
            ViewBag.ZFRY1 = ListUser1;
            ViewBag.ZFRY2 = ListUser2;
            return PartialView(THIS_VIEW_PATH + "CFKYTZS.cshtml", cfkytzs);
        }

        /// <summary>
        /// 根据文书编号查询查封扣押通知书
        /// </summary>
        /// <param name="WSBH">文书编号</param>
        /// <returns>查封口要通知书实体</returns>
        public JsonResult GetCFKYTZS(string WSBH)
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

            CFKYTZS cfkytzs = (CFKYTZS)Serializer.Deserialize(docinstance.ASSEMBLYNAME, docinstance.TYPENAME, docinstance.VALUE);
            var jccfkytz = new
            {
                CFKYTZSSJ = cfkytzs.CFKYTZSJ != null ? cfkytzs.CFKYTZSJ.Value.ToString("yyyy-MM-dd") : "",
                CFKYWPQDList = cfkytzs.CFKYWPQDList,
                ZFRY1 = cfkytzs.ZFRY1,
                ZFRY2 = cfkytzs.ZFRY2
            };

            return Json(jccfkytz, JsonRequestBehavior.AllowGet);
        }
    }
}
