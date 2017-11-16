using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Workflows;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.BLL.CaseBLLs;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    /// <summary>
    /// 责令停止违法（章）行为通知书
    /// </summary>
    public class DocZLTZWFZXWTZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long Rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow casewordflow = new CaseWorkflow(WIID);
                form101 = casewordflow.CaseForm.FinalForm.Form101;
                ViewBag.WICode = casewordflow.CaseForm.WICode;
            }
            ZLTZWFZXWTZS zltzwfzxwtzs = new ZLTZWFZXWTZS();
            zltzwfzxwtzs.BH = DocBuildBLL.GetZLTZWZFTZSCode();
            zltzwfzxwtzs.FASJ = form101.FASJ;
            //当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    zltzwfzxwtzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    zltzwfzxwtzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }

            if (form101.IllegalForm != null)
            {
                zltzwfzxwtzs.WFDGD = form101.IllegalForm.WeiZe;
                zltzwfzxwtzs.FLGJ = form101.IllegalForm.FaZe;
            }

            zltzwfzxwtzs.WFXW = form101.AY;
            zltzwfzxwtzs.WFXWandFADD = string.Format("{0},进行了{1}", form101.FADD, zltzwfzxwtzs.WFXW);
            return PartialView(THIS_VIEW_PATH + "ZLTZWFZXWTZS.cshtml", zltzwfzxwtzs);
        }

        public ActionResult Edit(string WIID, string DDID, string AIID,
            string ADID, string DIID, long rad)
        {
            ViewBag.WIID = WIID;
            ViewBag.DDID = DDID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.DIID = DIID;

            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;

            //根据文书标示获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            ZLTZWFZXWTZS zltzwfzxwtzs = (ZLTZWFZXWTZS)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditZLTZWFZXWTZS.cshtml", zltzwfzxwtzs);
        }

        //责令停止违法(章)行为通知书
        [HttpPost]
        public ActionResult CommitDocumentZLTZWFZXWTZS(ZLTZWFZXWTZS _zltzwfzxwtzs)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string WICode = this.Request.Form["WICode"];
            string ddid = this.Request.Form["DOCDDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];

            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            if (docTypeID == 1)
            {

                savePDFFilePath = DocBuildBLL.DocBuildZLTZWFZXWTZS(
                    SessionManager.User.RegionName, WICode, _zltzwfzxwtzs);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_zltzwfzxwtzs),
                    ASSEMBLYNAME = _zltzwfzxwtzs.GetType().Assembly.FullName,
                    TYPENAME = _zltzwfzxwtzs.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "责令停止违法(章)行为通知书",
                    DOCBH = _zltzwfzxwtzs.BH
                };

            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, WICode, "责令停止违法(章)行为通知书",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "责令停止违法(章)行为通知书"
                };
            }

            //添加文书
            DocBLL.AddDocInstance(docInstance, false);

            return RedirectToAction("WorkflowProcess", "Workflow",
                new
                {
                    WIID = wiid,
                    AIID = aiid,
                    DDID = ddid,
                    DIID = docInstance.DOCINSTANCEID
                });
        }

        //修改责令停止违法(章)行为通知书
        [HttpPost]
        public ActionResult CommitEditDocumentZLTZWFZXWTZS(ZLTZWFZXWTZS _zltzwfzxwtzs)
        {
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFile = DocBuildBLL.DocBuildZLTZWFZXWTZS(SessionManager.User.RegionName,
                ajbh, _zltzwfzxwtzs);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFile,
                VALUE = Serializer.Serialize(_zltzwfzxwtzs),
                DOCNAME = "责令停止违法(章)行为通知书"
            };

            //修改责令停止违法(章)行为通知书
            DocBLL.EditDocInstance(docInstance);

            return RedirectToAction("WorkflowProcess", "Workflow",
                new
                {
                    WIID = wiid,
                    AIID = aiid,
                    DDID = ddid,
                    DIID = docInstance.DOCINSTANCEID
                });
        }

    }
}
