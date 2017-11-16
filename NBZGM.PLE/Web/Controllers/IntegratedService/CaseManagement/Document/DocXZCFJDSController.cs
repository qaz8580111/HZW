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
    ///   //行政处罚决定书
    /// </summary>
    public class DocXZCFJDSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID, string AIID,
              string ADID, long Rad)
        {
            CaseWorkflow caseworkflow = new CaseWorkflow(WIID);
            Form101 form101 = caseworkflow.CaseForm.FinalForm.Form101;
            Form107 form107 = caseworkflow.CaseForm.FinalForm.Form107;
            XZCFJDS xzcfjds = new XZCFJDS();

            ViewBag.WICode = caseworkflow.CaseForm.WICode;
            //判断当事人类型
            xzcfjds.BH = "";
            xzcfjds.DSRLX = form101.DSRLX;
            xzcfjds.BH = DocBuildBLL.XZCFJDSCode();

            xzcfjds.ZSD = form101.ZSD;
            xzcfjds.LXDH = form101.LXDH;
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    xzcfjds.personForm = form101.PersonForm;
                    xzcfjds.GRZY = "个体";
                    xzcfjds.personForm.SFZH = form101.PersonForm.SFZH;
                }
            }

            if (form101.DSRLX == "dw")
            {
                if (form101.OrgForm != null)
                {
                    xzcfjds.orgForm = form101.OrgForm;
                }
            }

            string contentMB = "";
            if (form101.DSRLX == "dw")
            {
                contentMB = "　　经查明,你公司{0}。本机关认为你公司的上述行为违反了{1}，已构成违法。具体证据如下：{2}。现依据{3}, 决定对你公司作出如下行政处罚：\r";


            }
            else if (form101.DSRLX == "gr")
            {
                contentMB = "　　经查明,当事人{0}。本机关认为你公司的上述行为违反了{1}，已构成违法。具体证据如下：{2}。现依据{3}, 决定对你公司作出如下行政处罚：\r";

            }

            contentMB += caseworkflow.CaseForm.FinalForm.Form107.DCZJHCBRYJ;
            if (form107.CFJE != 0)
            {
                contentMB += "　　上述罚款，你公司应自收到本决定书之日起15日内，将罚款缴至台州市××银行××营业部（地址：×××路×××号），";
                contentMB += "（账户：台州市财政局，账号：5100××××）。逾期不缴纳罚款的，依据《中华人民共和国行政处罚法》第五十";
                contentMB += "一条第（一）项之规定每日按罚款数额的3%加处罚款。\r";
            }
            contentMB += "　　如对本行政处罚决定不服的，可在接到本决定书之日起六十日内，向台州市人民政府申请行政复议";
            contentMB += "也可在接到本决定书之日起三个月内直接向人民法院起诉。\r";
            contentMB += "　　逾期不申请行政复议，也不提起行政诉讼，又不履行行政处罚决定的，本机关将申请人民法院强制执行。";
            xzcfjds.CONTENT = string.Format(contentMB, form107.RDDWFSS, form107.WFDFLFGHGZ, form107.ZJ, form107.CFYJ);
            xzcfjds.XZCFSJ = DateTime.Now;


            return PartialView(THIS_VIEW_PATH + "XZCFJDS.cshtml", xzcfjds);
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
            XZCFJDS xzcfjds = (XZCFJDS)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditXZCFJDS.cshtml", xzcfjds);
        }

        [HttpPost]
        public ActionResult CommitDocumetDocXZCFJDS(XZCFJDS _xzcfjds)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string WICode = this.Request.Form["WICode"];

            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            if (docTypeID == 1)
            {
                savePDFFilePath = DocBuildBLL.DocBuildXZCFSJDS(
                    SessionManager.User.RegionName, WICode, _xzcfjds);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = System.Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_xzcfjds),
                    ASSEMBLYNAME = _xzcfjds.GetType().Assembly.FullName,
                    TYPENAME = _xzcfjds.GetType().FullName,
                    WIID = wiid,
                    DOCBH = _xzcfjds.BH,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "行政处罚决定书"
                };
            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, WICode, "行政处罚决定书",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = System.Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    WIID = wiid,
                    DOCBH = _xzcfjds.BH,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "行政处罚决定书"
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

        //修改行政处罚决定书
        [HttpPost]
        public ActionResult CommitEditDocumetDocXZCFJDS(XZCFJDS _xzcfjds)
        {
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFielPath = DocBuildBLL.DocBuildXZCFSJDS(SessionManager.User.RegionName,
                ajbh, _xzcfjds);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFielPath,
                VALUE = Serializer.Serialize(_xzcfjds),
                DOCNAME = "行政处罚决定书"
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
