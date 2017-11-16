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

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    //调查询问通知书(规划)
    public class DocDCXWTZSGHController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID
            , string AIID, string ADID, long rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
                form101 = caseWorkflow.CaseForm.FinalForm.Form101;
            }
            DCXWTZSGH dcxwtzsgh = new DCXWTZSGH();
            dcxwtzsgh.BH = DocBuildBLL.GetDCXWTZSCode();
            //判断当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    dcxwtzsgh.DSR = form101.PersonForm.XM;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    dcxwtzsgh.DSR = form101.OrgForm.MC;
                }
            }

            dcxwtzsgh.FASJ = form101.FASJ;
            dcxwtzsgh.FADD = form101.FADD;
            dcxwtzsgh.WFXW = form101.AY;
            dcxwtzsgh.LXR = SessionManager.User.UserName;
            dcxwtzsgh.DZ = "台州市椒江区星云路218号";
            dcxwtzsgh.DH = "";
            return PartialView(THIS_VIEW_PATH + "DCXWTZSGH.cshtml", dcxwtzsgh);
        }

        public ActionResult Edit(string WIID, string DDID, string AIID,
            string ADID, string DIID, long rad)
        {
            ViewBag.DDID = DDID;
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.DIID = DIID;

            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;

            //根据文书标示获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            DCXWTZSGH dcxwtzsgh = (DCXWTZSGH)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            return PartialView(THIS_VIEW_PATH + "EditDCXWTZSGH.cshtml", dcxwtzsgh);
        }

        [HttpPost]
        public ActionResult CommitDocumentDCXWTZSGH(DCXWTZSGH _dcxwtzsgh)
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
                DCXWTZSGH dcxwtzsgh = new DCXWTZSGH();
                //案件编号
                dcxwtzsgh.BH = _dcxwtzsgh.BH;
                //当事人               
                dcxwtzsgh.DSR = _dcxwtzsgh.DSR;
                //发案时间
                dcxwtzsgh.FASJ = _dcxwtzsgh.FASJ;
                //发案地点
                dcxwtzsgh.FADD = _dcxwtzsgh.FADD;
                //违法行为
                dcxwtzsgh.WFXW = _dcxwtzsgh.WFXW;
                //调查询问时间
                dcxwtzsgh.DCXWSJ = _dcxwtzsgh.DCXWSJ;
                //调查询问地点
                dcxwtzsgh.DCXWDD = _dcxwtzsgh.DCXWDD;
                //当事人身份证明
                dcxwtzsgh.DSRSFZM = _dcxwtzsgh.DSRSFZM;
                //委托他人办理的...
                dcxwtzsgh.WTTRBLD = _dcxwtzsgh.WTTRBLD;
                //建设项目批准文件
                dcxwtzsgh.JSXMPZWJ = _dcxwtzsgh.JSXMPZWJ;
                //建设用地规划许可证
                dcxwtzsgh.JSYDGHXKZ = _dcxwtzsgh.JSYDGHXKZ;
                //建设工程规划许可证
                dcxwtzsgh.JSGCGHXKZ = _dcxwtzsgh.JSGCGHXKZ;
                //建设用地许可证或建设用地批准书
                dcxwtzsgh.JSYDXKZORJSYDPZS = _dcxwtzsgh.JSYDXKZORJSYDPZS;
                //土地使用证复印件
                dcxwtzsgh.TDSYZFYJ = _dcxwtzsgh.TDSYZFYJ;
                //竣工复核图原件
                dcxwtzsgh.JGFHTYJ = _dcxwtzsgh.JGFHTYJ;
                //房产测绘成果原件
                dcxwtzsgh.FCCHCGYJ = _dcxwtzsgh.FCCHCGYJ;
                //相关部门意见原件
                dcxwtzsgh.XGBMYJYJ = _dcxwtzsgh.XGBMYJYJ;
                //有关协议和合同
                dcxwtzsgh.YGXYHHT = _dcxwtzsgh.YGXYHHT;
                //其他
                dcxwtzsgh.QT = _dcxwtzsgh.QT;
                //联系人
                dcxwtzsgh.LXR = _dcxwtzsgh.LXR;
                //地址
                dcxwtzsgh.DZ = _dcxwtzsgh.DZ;
                //电话
                dcxwtzsgh.DH = _dcxwtzsgh.DH;
                //发出日期
                dcxwtzsgh.FCRQ = _dcxwtzsgh.FCRQ;
                //当事人收件签章
                dcxwtzsgh.DSRSJQZ = _dcxwtzsgh.DSRSJQZ;
                //当事人电话
                dcxwtzsgh.DSRDH = _dcxwtzsgh.DSRDH;
                //收件签章日期
                dcxwtzsgh.DSRSJRQ = _dcxwtzsgh.DSRSJRQ;

                savePDFFilePath = DocBuildBLL.DocBuildDCXWTZSGH(
                       SessionManager.User.RegionName, wiCode, dcxwtzsgh);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(dcxwtzsgh),
                    ASSEMBLYNAME = dcxwtzsgh.GetType().Assembly.FullName,
                    TYPENAME = dcxwtzsgh.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "调查询问通知书(规划)",
                    DOCBH = _dcxwtzsgh.BH
                };
            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "调查询问通知书(规划)",
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
                    DOCNAME = "调查询问通知书(规划)"
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

        //修改调查询问通知书(规划)
        [HttpPost]
        public ActionResult CommitEditDocumentDCXWTZSGH(DCXWTZSGH _dcxwtzsgh)
        {
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docID = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFielPath = DocBuildBLL.DocBuildDCXWTZSGH(SessionManager.User.RegionName,
                ajbh, _dcxwtzsgh);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docID,
                DOCPATH = savePDFFielPath,
                VALUE = Serializer.Serialize(_dcxwtzsgh),
                DOCNAME = "调查询问通知书(规划)"
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
