using System;
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

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    /// <summary>
    /// 案件集体讨论笔录
    /// </summary>
    public class DocAJJTTLBLController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CommitDocumetAJJTTLBL(AJJTTLBL _ajjttlbl)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ajbh1 = this.Request.Form["CODE"];
            //对案件编号进行处理
            string[] arr = ajbh1.Split();
            string ajbh = string.Join("", arr);
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];

            CaseWorkflow caseWorkFlow = new CaseWorkflow(wiid);

            CaseForm caseForm = (CaseForm)caseWorkFlow
                .Workflow.Properties["CaseForm"];

            Form101 form1 = caseForm.FinalForm.Form101;
            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            //表单录入
            if (docTypeID == 1)
            {
                AJJTTLBL ajjttlbl = new AJJTTLBL();
                //案件名称
                ajjttlbl.AJMC = _ajjttlbl.AJMC;
                //案号
                ajjttlbl.AH = ajbh;
                //开始时间
                ajjttlbl.StartTLSJ = _ajjttlbl.StartTLSJ;
                //结束时间
                ajjttlbl.EndTLSJ = _ajjttlbl.EndTLSJ;
                //地点
                ajjttlbl.DD = _ajjttlbl.DD;
                //集体讨论原因
                ajjttlbl.JTTLYY = _ajjttlbl.JTTLYY;
                //主持人
                ajjttlbl.ZCR = _ajjttlbl.ZCR;
                //记录人
                ajjttlbl.JLR = _ajjttlbl.JLR;
                //参加人员
                ajjttlbl.CJRY = _ajjttlbl.CJRY;
                //列席人员
                ajjttlbl.LXRY = _ajjttlbl.LXRY;

                savePDFFilePath = DocBuildBLL.DocBuildAJJTTLBL(
                       SessionManager.User.RegionName, ajbh, ajjttlbl);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(ajjttlbl),
                    ASSEMBLYNAME = ajjttlbl.GetType().Assembly.FullName,
                    TYPENAME = ajjttlbl.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "案件集体讨论记录"
                };
            }
            //上传扫描件
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, ajbh, "案件集体讨论记录",
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

        //编辑案件集体讨论笔录
        [HttpPost]
        public ActionResult CommitEditDocumetAJJTTLBL(AJJTTLBL _ajjttlbl)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ajbh1 = this.Request.Form["CODE"];
            //对案件编号进行处理
            string[] arr = ajbh1.Split();
            string ajbh = string.Join("", arr);
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string docID = this.Request.Form["DIID"];

            CaseWorkflow caseWorkFlow = new CaseWorkflow(wiid);

            CaseForm caseForm = (CaseForm)caseWorkFlow
                .Workflow.Properties["CaseForm"];

            Form101 form1 = caseForm.FinalForm.Form101;
            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            //表单录入
            if (docTypeID == 1)
            {
                AJJTTLBL ajjttlbl = new AJJTTLBL();
                //案件名称
                ajjttlbl.AJMC = _ajjttlbl.AJMC;
                //案号
                ajjttlbl.AH = ajbh;
                //开始时间
                ajjttlbl.StartTLSJ = _ajjttlbl.StartTLSJ;
                //结束时间
                ajjttlbl.EndTLSJ = _ajjttlbl.EndTLSJ;
                //地点
                ajjttlbl.DD = _ajjttlbl.DD;
                //集体讨论原因
                ajjttlbl.JTTLYY = _ajjttlbl.JTTLYY;
                //主持人
                ajjttlbl.ZCR = _ajjttlbl.ZCR;
                //记录人
                ajjttlbl.JLR = _ajjttlbl.JLR;
                //参加人员
                ajjttlbl.CJRY = _ajjttlbl.CJRY;
                //列席人员
                ajjttlbl.LXRY = _ajjttlbl.LXRY;

                savePDFFilePath = DocBuildBLL.DocBuildAJJTTLBL(
                       SessionManager.User.RegionName, ajbh, ajjttlbl);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(ajjttlbl),
                    ASSEMBLYNAME = ajjttlbl.GetType().Assembly.FullName,
                    TYPENAME = ajjttlbl.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "案件集体讨论记录"
                };
            }
            //上传扫描件
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, ajbh, "案件集体讨论记录",
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

            //修改文书
            if (!string.IsNullOrWhiteSpace(docID))
            {
                //DocBLL.EditDocInstance(docInstance, docID);
            }

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
