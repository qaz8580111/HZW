using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Document
{
    /// <summary>
    /// 当事人身份证明
    /// </summary>
    public class DocDSRSFZMController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
            }
            //CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            //ViewBag.WICode = caseWorkflow.CaseForm.WICode;
            return PartialView(THIS_VIEW_PATH + "DSRSFZM.cshtml");
        }

        public ActionResult Edit(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            return PartialView(THIS_VIEW_PATH + "EditDSRSFZM.cshtml");
        }

        //当事人身份证明
        [HttpPost]
        public ActionResult CommitDocumentDSRSFZM()
        {
            string wsmc = this.Request.Form["WenshuName"];

            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string wiCode = this.Request.Form["WICode"];

            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            //表单录入
            savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "当事人身份证明",
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
                DOCNAME = wsmc
            };
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

    }
}
