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
using Web;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Document
{

    //文书送达回证
    public class DocSDGGController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID, string AIID,
               string ADID, long Rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
                form101 = caseWorkflow.CaseForm.FinalForm.Form101;
            }
            SDGG sdgg = new SDGG();

            sdgg.AY = form101.AY;
            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    sdgg.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    sdgg.DSR = form101.OrgForm.FDDBRXM;
                }
            }

            return PartialView(THIS_VIEW_PATH + "SDGG.cshtml", sdgg);
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
            SDGG sdgg = (SDGG)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditSDGG.cshtml", sdgg);
        }

        [HttpPost]
        public ActionResult CommitDocumentSDGG(SDGG _sdgg)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string wiCode = this.Request.Form["WICode"];

            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";
            UserInfo User = SessionManager.User;

            //表单录入
            if (docTypeID == 1)
            {
                savePDFFilePath = DocBuildBLL.DocBuildSDGG(
              SessionManager.User.RegionName, wiCode, _sdgg);
                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_sdgg),
                    ASSEMBLYNAME = _sdgg.GetType().Assembly.FullName,
                    TYPENAME = _sdgg.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "送达公告"
                };
            }
            //以扫描件的方式录入
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
              SessionManager.User.RegionName, wiCode, "送达公告",
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
                    DOCNAME = "送达公告"
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

        [HttpPost]
        public ActionResult CommitEditDocumentSDGG(SDGG _sdgg)
        {
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string ddid = this.Request.Form["DDID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];
            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildSDGG(SessionManager.User.RegionName,
                ajbh, _sdgg);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_sdgg),
                DOCNAME = "送达公告"
            };

            //修改文书
            DocBLL.EditDocInstance(docInstance);

            return RedirectToAction("WorkflowProcess", "Workflow", new
            {
                WIID = wiid,
                AIID = aiid,
                DDID = ddid,
                DIID = docInstance.DOCINSTANCEID
            });

        }
    }
}
