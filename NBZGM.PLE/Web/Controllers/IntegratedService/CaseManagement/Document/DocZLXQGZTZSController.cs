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
    /// 责令限期改正通知书
    /// </summary>
    public class DocZLXQGZTZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long Rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
                form101 = caseworkflow.CaseForm.FinalForm.Form101;
                ViewBag.WICode = caseworkflow.CaseForm.WICode;
            }
            ZLXQGZTZS zlxqgztzs = new ZLXQGZTZS();

            zlxqgztzs.WSBH = DocBuildBLL.GetZLXQGZTZSCode();
            //当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    zlxqgztzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    zlxqgztzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }

            zlxqgztzs.FASJ = form101.FASJ;
            zlxqgztzs.WFXW = form101.AY;
            zlxqgztzs.FADD = form101.FADD;
            if (form101.IllegalForm != null)
            {
                zlxqgztzs.WFDEGD = form101.IllegalForm.WeiZe;
                zlxqgztzs.ZLGZYJ = form101.IllegalForm.FaZe;
            }

            return PartialView(THIS_VIEW_PATH + "ZLXQGZTZS.cshtml", zlxqgztzs);
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

            //根据文书标识获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            ZLXQGZTZS zlxqgztzs = (ZLXQGZTZS)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditZLXQGZTZS.cshtml", zlxqgztzs);
        }

        //添加责令限期改正通知书
        [HttpPost]
        public ActionResult CommitDocumentZLXQGZTZS(ZLXQGZTZS _zlxqgztzs)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string WICode = this.Request.Form["WICode"];
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            if (docTypeID == 1)
            {
                savePDFFilePath = DocBuildBLL.DocBuildZLXQGZTZS(
                    SessionManager.User.RegionName, WICode, _zlxqgztzs);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_zlxqgztzs),
                    ASSEMBLYNAME = _zlxqgztzs.GetType().Assembly.FullName,
                    TYPENAME = _zlxqgztzs.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCBH = _zlxqgztzs.WSBH,
                    DOCNAME = "责令限期改正通知书"
                };
            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(SessionManager
                    .User.RegionName, WICode, "责令限期改正通知书", this.Request.Files);

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
                    DOCNAME = "责令限期改正通知书"
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

        //修改责令限期改正通知书
        [HttpPost]
        public ActionResult CommitEditDocumentZLXQGZTZS(ZLXQGZTZS _zlxqgztzs)
        {
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string docId = this.Request.Form["DIID"];
            string ajbh = this.Request.Form["WICode"];

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildZLXQGZTZS(SessionManager.User.RegionName,
                ajbh, _zlxqgztzs);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_zlxqgztzs),
                DOCNAME = "责令限期改正通知书"
            };

            //修改文书
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
