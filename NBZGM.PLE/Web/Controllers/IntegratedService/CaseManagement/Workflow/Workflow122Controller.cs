using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;
using Web;
using Web.ViewModels.CaseViewModels;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow
{
    public class Workflow122Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow122/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;
            ViewModel22 viewModel22 = new ViewModel22();
            viewModel22.XZCFJDSWH = DocBuildBLL.GetXZCFJDSBHByWIID(CaseForm.WIID);
            return PartialView(THIS_VIEW_PATH + "Index.cshtml",viewModel22);
        }

        public ActionResult Commit(ViewModel22 viewModel22)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel22.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel22.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            Form101 form1 = caseForm.FinalForm.Form101;
            Form122 form22 = caseForm.FinalForm.Form122;

            form22.XZCFJDSWH = viewModel22.XZCFJDSWH;
            form22.XZCFNR = viewModel22.XZCFNR;
            form22.CFZXFSJFMCWDCZ = viewModel22.CFZXFSJFMCWDCZ;
            form22.ZBDYYJ = viewModel22.ZBDYYJ;
            form22.ProcessUser = SessionManager.User;
            form22.ProcessTime = DateTime.Now;

            activity.Submit();

            XZCFAJJABG xzcfajjabg = new XZCFAJJABG();
            xzcfajjabg.AY = form1.AY;
            xzcfajjabg.FADD = form1.FADD;
            xzcfajjabg.FASJ = form1.FASJ;
            xzcfajjabg.AJLYName = form1.AJLYName;
            xzcfajjabg.MC = form1.OrgForm.MC;
            xzcfajjabg.ZZJGDMZBH = form1.OrgForm.ZZJGDMZBH;
            xzcfajjabg.FDDBRXM = form1.OrgForm.FDDBRXM;
            xzcfajjabg.ZW = form1.OrgForm.ZW;
            xzcfajjabg.XM = form1.PersonForm.XM;
            xzcfajjabg.XB = form1.PersonForm.XB;
            xzcfajjabg.CSNY = form1.PersonForm.CSNY;
            xzcfajjabg.MZ = form1.PersonForm.MZ;
            xzcfajjabg.SFZH = form1.PersonForm.SFZH;
            xzcfajjabg.GZDW = form1.PersonForm.GZDW;

            xzcfajjabg.ZSD = form1.ZSD;
            xzcfajjabg.LXDH = form1.LXDH;

            xzcfajjabg.XZCFJDSWH = viewModel22.XZCFJDSWH;
            xzcfajjabg.XZCFNR = viewModel22.XZCFNR;
            xzcfajjabg.CFZXFSJFMCWDCZ = viewModel22.CFZXFSJFMCWDCZ;
            xzcfajjabg.ZBDYYJ = viewModel22.ZBDYYJ;

            string savePDFFilePath = DocBuildBLL.BuildXZCFAJJABG(
                SessionManager.User.RegionName, caseForm.WICode, xzcfajjabg);

            DOCINSTANCE docIntance = new DOCINSTANCE()
            {
                DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                DDID = DocDefinition.LASPB,
                DOCTYPEID = (decimal)DocTypeEnum.PDF,
                AIID = activity.AIID,
                DPID = DocBLL.GetDPIDByADID(activity.ADID),
                VALUE = Serializer.Serialize(xzcfajjabg),
                ASSEMBLYNAME = xzcfajjabg.GetType().Assembly.FullName,
                TYPENAME = xzcfajjabg.GetType().FullName,
                WIID = viewModel22.WIID,
                DOCPATH = savePDFFilePath,
                DOCNAME = "行政处罚案件结案报告",
                CREATEDTIME = DateTime.Now
            };

            DocBLL.AddDocInstance(docIntance, true);

            //短信内容
            string SMStoUserNAme = this.Request.Form["XBDYNAME"];
            string megContent = SMStoUserNAme + ",您在案件管理子系统中有一条新任务等待处理";
            //电话号码
            string phoneNumber = this.Request.Form["FSDX"];
            //发送短信
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                SMSUtility.SendMessage(phoneNumber, megContent + "[" + SessionManager.User.UserName + "]", DateTime.Now.Ticks);
            }

            return RedirectToAction("PendingCaseList", "GeneralCase");
        }

    }
}
