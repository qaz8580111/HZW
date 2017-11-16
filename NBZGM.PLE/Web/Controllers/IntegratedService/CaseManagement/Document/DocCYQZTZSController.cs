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

namespace Taizhou.PLE.CMS.Web.Controllers.WorkflowCenter.DocCYQZTZS
{
    /// <summary>
    /// 抽样取证通知书
    /// </summary>
    public class DocCYQZTZSController : Controller
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
            CYQZTZS cyqztzs = new CYQZTZS();

            if (form101.DSRLX == "dw")
            {
                if (form101.OrgForm != null)
                {
                    cyqztzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }
            else
            {
                if (form101.PersonForm != null)
                {
                    if (form101.PersonForm != null)
                    {
                        cyqztzs.DSR = form101.PersonForm.XM;
                    }
                }
            }

            cyqztzs.CYQZBH = DocBuildBLL.CYQZTZSCode();
            cyqztzs.WFXW = form101.AY;

            if (form101.IllegalForm != null)
            {
                cyqztzs.WFDGD = form101.IllegalForm.WeiZe;
            }

            return PartialView(THIS_VIEW_PATH + "CYQZTZS.cshtml", cyqztzs);
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
            CYQZTZS cyqztzs = (CYQZTZS)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditCYQZTZS.cshtml", cyqztzs);
        }

        //添加抽样取证通知书
        [HttpPost]
        public ActionResult CommitDocumentCYQZTZS(CYQZTZS _cyqztzs)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];

            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string wiCode = this.Request.Form["WICode"];
            string CYQZBDQD = this.Request.Form["CYQZBDQDJson"];

            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";

            //抽样取证编号(后台自动生成)
            string CYQZBH = DocBuildBLL.CYQZTZSCode();

            //表单录入
            if (docTypeID == 1)
            {
                //反序列化
                List<CYQZWPQD> CYQZWPQDList = JsonHelper
                    .JsonDeserialize<List<CYQZWPQD>>(CYQZBDQD);

                _cyqztzs.CYQZWPQDList = CYQZWPQDList;

                savePDFFilePath = DocBuildBLL.DocBuildCYQZTZS(
                   SessionManager.User.RegionName, wiCode, _cyqztzs);

                docInstance = new DOCINSTANCE()
                {
                    DOCBH = _cyqztzs.CYQZBH,
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_cyqztzs),
                    ASSEMBLYNAME = _cyqztzs.GetType().Assembly.FullName,
                    TYPENAME = _cyqztzs.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "抽样取证通知书",
                };
            }
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "抽样取证通知书",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCBH = CYQZBH,
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "抽样取证通知书"
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

        //修改抽样取证通知书
        [HttpPost]
        public ActionResult CommitEditDocumentCYQZTZS(CYQZTZS _cyqztzs)
        {
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string docId = this.Request.Form["DIID"];
            string ajbh = this.Request.Form["WICode"];
            string CYQZBDQD = this.Request.Form["CYQZBDQDJson"];

            //反序列化抽样取证物品清单
            List<CYQZWPQD> CYQZWPQDList = JsonHelper.JsonDeserialize<List<CYQZWPQD>>(CYQZBDQD);
            _cyqztzs.CYQZWPQDList = CYQZWPQDList;

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildCYQZTZS(SessionManager.User.RegionName,
                ajbh, _cyqztzs);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_cyqztzs),
                DOCNAME = "抽样取证通知书"
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
