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
    /// <summary>
    /// 罚没物品处理记录
    /// </summary>
    public class DocFMWPCLJLController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID, string AIID,
              string ADID, long Rad)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;
            FMWPCLJL fmwpclijl = new FMWPCLJL();
            fmwpclijl.CLWPDYXZCFJDSJWH = DocBuildBLL.GetXZCFJDSBHByWIID(WIID);
            return PartialView(THIS_VIEW_PATH + "FMWPCLJL.cshtml", fmwpclijl);
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

            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            FMWPCLJL fmwpcljl = (FMWPCLJL)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditFMWPCLJL.cshtml", fmwpcljl);
        }

        //罚没物品处理记录
        [HttpPost]
        public ActionResult CommitDocumentFMWPCLJL(FMWPCLJL _fmwpcljl)
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
                FMWPCLJL fmwpcljl = new FMWPCLJL();
                // 处理机关名称或印章
                fmwpcljl.CLJGMCHYZ = _fmwpcljl.CLJGMCHYZ;
                // 处理时间
                fmwpcljl.CLSJ = _fmwpcljl.CLSJ;
                // 处理地点
                fmwpcljl.CLDD = _fmwpcljl.CLDD;
                // 处理物品执行人
                fmwpcljl.CLWPZXR = _fmwpcljl.CLWPZXR;
                // 记录人
                fmwpcljl.JLR = _fmwpcljl.JLR;
                // 见证人或监销人
                fmwpcljl.JZRHJXR = _fmwpcljl.JZRHJXR;
                // 处理物品原持有人
                fmwpcljl.CLWPYCYR = _fmwpcljl.CLWPYCYR;
                // 处理物品名称、数量和规格
                fmwpcljl.CLWPMCSLJGG = _fmwpcljl.CLWPMCSLJGG;
                // 处理物品的原行政处罚决定书及文号
                fmwpcljl.CLWPDYXZCFJDSJWH = _fmwpcljl.CLWPDYXZCFJDSJWH;
                // 处理理由及依据
                fmwpcljl.CLLYJYJ = _fmwpcljl.CLLYJYJ;
                // 处理方式及处理结果
                fmwpcljl.CLFSJCLJG = _fmwpcljl.CLFSJCLJG;
                // 执行人员签名
                //fmwpcljl.ZXRYQM = _fmwpcljl.ZXRYQM;
                //// 执行人员签名日期
                //fmwpcljl.ZXRYQMRQ = _fmwpcljl.ZXRYQMRQ;
                //// 见证人或监销人员签名
                //fmwpcljl.JZRHJXRQM = _fmwpcljl.JZRHJXRQM;
                //// 见证人或监销人员签名日期
                //fmwpcljl.JZRHJXRQMRQ = _fmwpcljl.JZRHJXRQMRQ;
                //// 批准机关负责人签名
                //fmwpcljl.PZJGFZRQM = _fmwpcljl.PZJGFZRQM;
                //// 批准机关负责人签名日期
                //fmwpcljl.PZJGFZRQMRQ = _fmwpcljl.PZJGFZRQMRQ;


                savePDFFilePath = DocBuildBLL.DocBuildFMWPCLJL(
                   SessionManager.User.RegionName, wiCode, fmwpcljl);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(fmwpcljl),
                    ASSEMBLYNAME = fmwpcljl.GetType().Assembly.FullName,
                    TYPENAME = fmwpcljl.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "罚没物品处理记录"
                };
            }
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "罚没物品处理记录",
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
                    DOCNAME = "罚没物品处理记录"
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

        //修改罚没物品处理记录
        [HttpPost]
        public ActionResult CommitEditDocumentFMWPCLJL(FMWPCLJL _fmwpcljl)
        {
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string ddid = this.Request.Form["DDID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];
            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildFMWPCLJL(SessionManager.User.RegionName,
                ajbh, _fmwpcljl);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_fmwpcljl),
                DOCNAME = "罚没物品处理记录"
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
