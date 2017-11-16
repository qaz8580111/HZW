using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Workflows;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.BLL.CaseBLLs;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    /// <summary>
    /// 行政处罚事先告知书
    /// </summary>
    public class DocXZCFSXGZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID, string AIID,
              string ADID, long Rad)
        {
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form107 form107 = caseworkflow.CaseForm.FinalForm.Form107;
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;

            ViewBag.WICode = caseworkflow.CaseForm.WICode;
            XZCFSXGZS xzcfsxgzs = new XZCFSXGZS();
            xzcfsxgzs.AY = form101.AY;
            xzcfsxgzs.BH = DocBuildBLL.XZCFSXGZSCode();
            xzcfsxgzs.WFSS = form107.RDDWFSS;
            xzcfsxgzs.XZCFLY = form107.WFDFLFGHGZ;
            xzcfsxgzs.CFYJ = form107.CFYJ;
            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    xzcfsxgzs.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    xzcfsxgzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }

            string cfje = "     ";

            //判断处罚金额
            if (form107.CFJE != 0)
            {
                cfje = Utility.NumtoCny(form107.CFJE);
            }
            xzcfsxgzs.GZRQ = DateTime.Now;
            xzcfsxgzs.ZFJGDZ = "台州市经济开发区星云路218号";
            string xzcfnr = "　　你（单位）" + xzcfsxgzs.AY + "一案，经调查和研究，本机关认为存在以下违法事实：" + xzcfsxgzs.WFSS + "";
            xzcfnr += "上述违法事实违反了" + xzcfsxgzs.XZCFLY + "的规定，" + xzcfsxgzs.CFYJ + "的规定，拟给予如下行政处罚\r";
            xzcfnr += caseworkflow.CaseForm.FinalForm.Form107.DCZJHCBRYJ;
            //xzcfnr += "　　1、\r";
            //xzcfnr += string.Format("　　2、处罚人民币{0}整\r", cfje);
            xzcfnr += "　　根据《中华人民共和国行政处罚法》第三十一条、第三十二条和         之";
            xzcfnr += "规定你（单位）可在收到本告知书之日起3日内向本机关提出书面陈述、申辩";
            xzcfnr += "和        ，逾期未提出的，视为放弃上述权利，本机关将依法作出行政处罚决定。";
            xzcfsxgzs.XZCFNR = xzcfnr;

            return PartialView(THIS_VIEW_PATH + "XZCFSXGZS.cshtml", xzcfsxgzs);
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
            XZCFSXGZS xzcfsxgzs = (XZCFSXGZS)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditXZCFSXGZS.cshtml", xzcfsxgzs);
        }


        //行政处罚事先告知书
        [HttpPost]
        public ActionResult CommitDocumetXZCFSXGZS(XZCFSXGZS _xzcfxsgzs)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DOCDDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string WICode = this.Request.Form["WICode"];

            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            if (docTypeID == 1)
            {
                savePDFFilePath = DocBuildBLL.DocBuildXZCFSXGZS(
                    SessionManager.User.RegionName, WICode, _xzcfxsgzs);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = System.Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_xzcfxsgzs),
                    ASSEMBLYNAME = _xzcfxsgzs.GetType().Assembly.FullName,
                    TYPENAME = _xzcfxsgzs.GetType().FullName,
                    WIID = wiid,
                    DOCBH = _xzcfxsgzs.BH,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "行政处罚事先告知书"
                };
            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, WICode, "行政处罚事先告知书",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = System.Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    WIID = wiid,
                    DOCBH = _xzcfxsgzs.BH,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "行政处罚事先告知书"
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

        //修改行政处罚事先告知书
        [HttpPost]
        public ActionResult CommitEditDocumetXZCFSXGZS(XZCFSXGZS _xzcfxsgzs)
        {
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docID = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildXZCFSXGZS(SessionManager.User.RegionName,
                ajbh, _xzcfxsgzs);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docID,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_xzcfxsgzs),
                DOCNAME = "行政处罚事先告知书"
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
