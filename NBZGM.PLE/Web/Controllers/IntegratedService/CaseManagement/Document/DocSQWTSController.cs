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
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    /// <summary>
    /// 授权委托书
    /// </summary>
    public class DocSQWTSController : Controller
    {

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                //根据流程标识得到立案审批表中提交的数据
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
                form101 = caseWorkflow.CaseForm.FinalForm.Form101;
            }

            //初始化加载委托行为（案由）
            SQWTS sqwts = new SQWTS();
            sqwts.WTXW = form101.AY;

            return PartialView(THIS_VIEW_PATH + "SQWTS.cshtml", sqwts);
        }


        public ActionResult Edit(string WIID, string DDID,
            string AIID, string ADID, string DIID, long rad)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);

            ViewBag.WICode = caseWorkflow.CaseForm.WICode;
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.DIID = DIID;

            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            ViewBag.DDID = docInstance.DDID;

            SQWTS sqwts = (SQWTS)Serializer.Deserialize(docInstance.ASSEMBLYNAME, docInstance.TYPENAME,
                docInstance.VALUE.ToString());
            return PartialView(THIS_VIEW_PATH + "EditSQWTS.cshtml", sqwts);
        }

        //授权委托书
        [HttpPost]
        public ActionResult CommitDocumentSQWTS(SQWTS _sqwts)
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

            //表单录入
            if (docTypeID == 1)
            {
                SQWTS sqwts = new SQWTS();
                sqwts.WTR = _sqwts.WTR;
                sqwts.SWTR = _sqwts.SWTR;
                sqwts.GZDWHZZ = _sqwts.GZDWHZZ;
                sqwts.SFZHM = _sqwts.SFZHM;
                sqwts.LXDH = _sqwts.LXDH;
                sqwts.QWDD = _sqwts.QWDD;
                sqwts.WTXW = _sqwts.WTXW;
                //sqwts.WTSJ = DateTime.Now;
                sqwts.WTSJ = _sqwts.WTSJ;

                //文书生成路径
                savePDFFilePath = DocBuildBLL.DocBuildSQWTS(
                    SessionManager.User.RegionName, wiCode, sqwts);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(sqwts),
                    ASSEMBLYNAME = sqwts.GetType().Assembly.FullName,
                    TYPENAME = sqwts.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "授权委托书"
                };
            }
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "授权委托书",
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
                    DOCNAME = "授权委托书"
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

        /// <summary>
        /// 修改授权委托书
        /// </summary>
        /// <param name="_sqwts">授权委托书实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommitEditDocumentSQWTS(SQWTS _sqwts)
        {
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docID = this.Request.Form["DIID"];

            string savePDFFilePath = "";

            //生成文书保存路径
            savePDFFilePath = DocBuildBLL.DocBuildSQWTS(
                SessionManager.User.RegionName, ajbh, _sqwts);

            DOCINSTANCE docInstance = new DOCINSTANCE()
           {
               DOCINSTANCEID = docID,
               VALUE = Serializer.Serialize(_sqwts),
               DOCPATH = savePDFFilePath,
               DOCNAME = "授权委托书"
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
