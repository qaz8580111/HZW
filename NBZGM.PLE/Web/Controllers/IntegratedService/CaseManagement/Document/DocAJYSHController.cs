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
using Taizhou.PLE.Model.CustomModels;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    /// <summary>
    /// //案件移送函
    /// </summary>
    public class DocAJYSHController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CommitDocumentAJYSH()
        {
            string ajbh1 = this.Request.Form["CODE"];
            //对案件编号进行处理
            string[] arr = ajbh1.Split();
            string ajbh = string.Join("", arr);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";
            UserInfo User = SessionManager.User;

            savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                SessionManager.User.RegionName, ajbh, "案件移送函",
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
                DOCNAME = "案件移送函"
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
