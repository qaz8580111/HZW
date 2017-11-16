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


namespace Web.Controllers.IntegratedService.CaseManagement.Document
{
    /// <summary>
    /// 案件集体讨论记录
    /// </summary>
    public class DocAJJTTLJLController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            //根据流程标识得到立案审批表中提交的数据
            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;
            Form101 form101 = caseWorkflow.CaseForm.FinalForm.Form101;

            //初始化加载委托行为（案由）
            AJJTTLJL ajjttljl = new AJJTTLJL();
            ajjttljl.AH = form101.WSBH;
            ajjttljl.AJMC = form101.AY;
            return PartialView(THIS_VIEW_PATH + "AJJTTLJL.cshtml", ajjttljl);
        }

        public ActionResult Edit(string WIID, string DDID,
            string AIID, string ADID, string DIID, long rad)
        {
            ViewBag.WIID = WIID;
            ViewBag.DDID = DDID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.DDID = DDID;

            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;

            //根据文书标识获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            AJJTTLJL ajjitljl = (AJJTTLJL)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditAJJTTLJL.cshtml", ajjitljl);
        }

        [HttpPost]
        public ActionResult CommitDocumentAJJTTLJL(AJJTTLJL AJJTTLJL)
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
                //文书生成路径
                savePDFFilePath = DocBuildBLL.DocBuildAJJTTLJL(
                    SessionManager.User.RegionName, wiCode, AJJTTLJL);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(AJJTTLJL),
                    ASSEMBLYNAME = AJJTTLJL.GetType().Assembly.FullName,
                    TYPENAME = AJJTTLJL.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "案件集体讨论记录"
                };
            }
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "案件集体讨论记录",
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
                    DOCNAME = "案件集体讨论记录"
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
        //修改案件集体讨论记录
        public ActionResult CommitEditDocumentAJJTTLJL(AJJTTLJL _ajjttljl)
        {
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string ddid = this.Request.Form["DDID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];
            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildAJJTTLJL(SessionManager.User.RegionName,
                ajbh, _ajjttljl);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_ajjttljl),
                DOCNAME = "案件集体讨论记录"
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
