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

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    public class DocWSSDHZController : Controller
    {
        /// <summary>
        /// 文书送达回证
        /// </summary>
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID, string AIID,
               string ADID, long Rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
                form101 = caseworkflow.CaseForm.FinalForm.Form101;
                ViewBag.WICode = caseworkflow.CaseForm.WICode;
            }
            WSSDHZ wssdhz = new WSSDHZ();

            wssdhz.AY = form101.AY;
            wssdhz.SDFS = "直接送达";


            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    wssdhz.SSDR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    wssdhz.SSDR = form101.OrgForm.MC;
                }
            }
            return PartialView(THIS_VIEW_PATH + "WSSDHZ.cshtml", wssdhz);
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
            WSSDHZ wssdhz = (WSSDHZ)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);


            return PartialView(THIS_VIEW_PATH + "EditWSSDHZ.cshtml", wssdhz);
        }

        //送达公告
        [HttpPost]
        public ActionResult CommitDocumentWSSDHZ(WSSDHZ _wssdhz)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];

            //对案件编号进行处理

            decimal docTypeID = 0.0M;
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string WICode = this.Request.Form["WICode"];

            decimal.TryParse(strDocTypeID, out docTypeID);
            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";
            UserInfo User = SessionManager.User;

            //表单录入
            if (docTypeID == 1)
            {
                savePDFFilePath = DocBuildBLL.DocBuildWSSDHZ(
              SessionManager.User.RegionName, WICode, _wssdhz);
                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_wssdhz),
                    ASSEMBLYNAME = _wssdhz.GetType().Assembly.FullName,
                    TYPENAME = _wssdhz.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "文书送达回证"
                };
            }
            //以扫描件的方式录入
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
              SessionManager.User.RegionName, WICode, "文书送达回证",
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
                    DOCNAME = "文书送达回证"
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
        public ActionResult CommitEditDocumentWSSDHZ(WSSDHZ _wssdhz)
        {
            string wiid = this.Request.Form["WIID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];
            string aiid = this.Request.Form["AIID"];
            string ddid = this.Request.Form["DDID"];

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildWSSDHZ(SessionManager.User.RegionName,
                ajbh, _wssdhz);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_wssdhz),
                DOCNAME = "文书送达回证"
            };

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
