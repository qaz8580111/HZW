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
using Web;
using Web.Workflows;

namespace Taizhou.PLE.CMS.Web.Controllers.WorkflowCenter.DocDCXWTZS
{
    /// <summary>
    /// 调查询问通知书(市容)
    /// </summary>
    public class DocDCXWTZSSRController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
                form101 = caseWorkflow.CaseForm.FinalForm.Form101;
            }

            DCXWTZS dcxwtzs = new DCXWTZS();
            dcxwtzs.BH = DocBuildBLL.GetDCXWTZSCode();

            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    dcxwtzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    dcxwtzs.DSR = form101.OrgForm.MC;
                }
            }

            dcxwtzs.FASJ = form101.FASJ;
            dcxwtzs.FADD = form101.FADD;
            dcxwtzs.WFXW = form101.AY;
            dcxwtzs.LXR = SessionManager.User.UserName;
            dcxwtzs.DZ = "台州市椒江区星云路218号";
            dcxwtzs.LXDH = "";
            return PartialView(THIS_VIEW_PATH + "DCXWTZSSR.cshtml", dcxwtzs);
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
            DCXWTZS dcxwtzs = (DCXWTZS)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditDCXWTZSSR.cshtml", dcxwtzs);
        }

        //调查询问通知书
        [HttpPost]
        public ActionResult CommitDocumentDCXWTZSSR(DCXWTZS _dcxwtzs)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DOCDDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string wiCode = this.Request.Form["WICode"];

            CaseWorkflow caseWorkflow = new CaseWorkflow(wiid);

            CaseForm caseForm = (CaseForm)caseWorkflow
                .Workflow.Properties["CaseForm"];

            Form101 form1 = caseForm.FinalForm.Form101;
            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            if (docTypeID == 1)
            {
                savePDFFilePath = DocBuildBLL.DocBuildDCXWTZS(
                       SessionManager.User.RegionName, wiCode, _dcxwtzs);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_dcxwtzs),
                    ASSEMBLYNAME = _dcxwtzs.GetType().Assembly.FullName,
                    TYPENAME = _dcxwtzs.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "调查询问通知书(市容)",
                    DOCBH = _dcxwtzs.BH
                };
            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "调查询问通知书",
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
                    DOCNAME = "调查询问通知书(市容)"
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

        //修改调查询问通知书(市容)
        [HttpPost]
        public ActionResult CommitEditDocumentDCXWTZS(DCXWTZS _dcxwtzs)
        {
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFielPath = DocBuildBLL.DocBuildDCXWTZS(SessionManager.User.RegionName,
                ajbh, _dcxwtzs);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFielPath,
                VALUE = Serializer.Serialize(_dcxwtzs),
                DOCNAME = "调查询问通知书(市容)"
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
