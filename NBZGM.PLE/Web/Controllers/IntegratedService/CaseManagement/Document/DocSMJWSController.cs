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
    public class DocSMJWSController : Controller
    {
        //
        // GET: /DocSMJWS/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            return PartialView(THIS_VIEW_PATH + "SMJWS.cshtml");
        }
        //提交扫描件文书
        [HttpPost]
        public ActionResult CommitDocumentSMJWS(SMJWS smjws)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            string WICode = this.Request.Form["WICode"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];

            UserInfo User = SessionManager.User;
            string savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                SessionManager.User.RegionName, WICode, smjws.Name,
                this.Request.Files);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                DDID = decimal.Parse(ddid),
                DOCTYPEID = (decimal)DocTypeEnum.Image,
                AIID = aiid,
                DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                WIID = wiid,
                DOCPATH = savePDFFilePath,
                CREATEDTIME = DateTime.Now,
                DOCNAME = smjws.Name
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
