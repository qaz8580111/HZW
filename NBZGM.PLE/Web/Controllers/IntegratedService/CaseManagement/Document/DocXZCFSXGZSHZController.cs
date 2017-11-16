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
    /// 行政处罚事先告知书回执
    /// </summary>
    public class DocXZCFSXGZSHZController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID, string AIID,
              string ADID, long Rad)
        {
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;
            XZCFJDS xzcfjds = new XZCFJDS();

            ViewBag.WICode = caseworkflow.CaseForm.WICode;
            XZCFSXGZSHZ xzcfsxgzshz = new XZCFSXGZSHZ();

            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    xzcfsxgzshz.DSR = form101.PersonForm.XM;

                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    xzcfsxgzshz.DSR = form101.OrgForm.FDDBRXM;
                }
            }
            xzcfsxgzshz.ZSD = form101.ZSD;
            xzcfsxgzshz.XZCFSXGZSBH = DocBuildBLL.GetXZCFSXGZSBHByWIID(WIID);


            return PartialView(THIS_VIEW_PATH + "XZCFSXGZSHZ.cshtml", xzcfsxgzshz);
        }

        public ActionResult Edit(string WIID, string DDID, string AIID,
              string ADID, string DIID, long Rad)
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
            XZCFSXGZSHZ xzcfsxgzshz = (XZCFSXGZSHZ)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditXZCFSXGZSHZ.cshtml", xzcfsxgzshz);
        }


        //行政处罚事先告知书回执
        [HttpPost]
        public ActionResult CommitDocumetXZCFSXGZSHZ(XZCFSXGZSHZ _xzcfsxgzshz)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string WICode = this.Request.Form["WICode"];

            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            if (docTypeID == 1)
            {

                savePDFFilePath = DocBuildBLL.DocBuildXZCFSXGZSHZ(
                    SessionManager.User.RegionName, WICode, _xzcfsxgzshz);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = System.Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_xzcfsxgzshz),
                    ASSEMBLYNAME = _xzcfsxgzshz.GetType().Assembly.FullName,
                    TYPENAME = _xzcfsxgzshz.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "行政处罚事先告知书回执"
                };
            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, WICode, "行政处罚事先告知书回执",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = System.Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "行政处罚事先告知书回执"
                };
            }

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
        //修改行政处罚事先告知书回执
        [HttpPost]
        public ActionResult CommitEditDocumetXZCFSXGZSHZ(XZCFSXGZSHZ _xzcfsxgzshz)
        {
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string ddid = this.Request.Form["DDID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildXZCFSXGZSHZ(SessionManager.User.RegionName,
                ajbh, _xzcfsxgzshz);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_xzcfsxgzshz),
                DOCNAME = "行政处罚事先告知书回执"
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
