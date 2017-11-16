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
    public class Workflow126Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow126/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            return PartialView(THIS_VIEW_PATH + "Index.cshtml");
        }

        public ActionResult Commit(ViewModel26 viewModel26)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel26.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel26.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            Form101 form1 = caseForm.FinalForm.Form101;
            Form122 form22 = caseForm.FinalForm.Form122;
            Form123 form23 = caseForm.FinalForm.Form123;
            Form124 form24 = caseForm.FinalForm.Form124;
            Form125 form25 = caseForm.FinalForm.Form125;
            Form126 form26 = caseForm.FinalForm.Form126;

            form26.FGFJZYJ = viewModel26.FGFJZYJ;
            form26.ProcessUser = SessionManager.User;
            form26.ProcessTime = DateTime.Now;

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

            xzcfajjabg.XZCFJDSWH = form22.XZCFJDSWH;
            xzcfajjabg.XZCFNR = form22.XZCFNR;
            xzcfajjabg.CFZXFSJFMCWDCZ = form22.CFZXFSJFMCWDCZ;
            xzcfajjabg.ZBDYYJ = form22.ZBDYYJ;
            xzcfajjabg.XBDYYJ = form23.XBDYYJ;
            xzcfajjabg.BADWYJ = form24.BADWYJ;
            xzcfajjabg.FZCYJ = form25.FZCYJ;
            xzcfajjabg.FGFJZYJ = viewModel26.FGFJZYJ;

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
                WIID = viewModel26.WIID,
                DOCPATH = savePDFFilePath,
                DOCNAME = "行政处罚案件结案报告",
                CREATEDTIME = DateTime.Now
            };

            DocBLL.AddDocInstance(docIntance, true);

            //短信内容
            string SMStoUserNAme = this.Request.Form["FGLDNAME"];
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
