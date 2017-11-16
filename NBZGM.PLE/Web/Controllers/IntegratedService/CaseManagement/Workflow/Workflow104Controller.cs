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
using Web.ViewModels.CaseViewModels;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    public class Workflow104Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow104/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;

            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel4 viewModel4 = new ViewModel4
            {
                AIID = AIID,
                WIID = CaseForm.WIID
            };

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel4);
        }

        public ActionResult Commit(ViewModel4 viewModel4)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel4.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel4.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            Form101 form1 = caseForm.FinalForm.Form101;
            Form102 form2 = caseForm.FinalForm.Form102;
            Form103 form3 = caseForm.FinalForm.Form103;
            Form104 form4 = caseForm.FinalForm.Form104;

            form4.FGFJZYJ = viewModel4.FGFJZYJ;
            form4.Approved = viewModel4.Approved;
            form4.ProcessUser = SessionManager.User;
            form4.ProcessTime = DateTime.Now;

            LASPB laspb = new LASPB();
            laspb.WSBH = form1.WSBH;
            laspb.AY = form1.AY;
            laspb.FADD = form1.FADD;
            laspb.FASJ = form1.FASJ;
            laspb.AJLY = form1.AJLYName;
            laspb.MC = form1.OrgForm.MC;
            laspb.ZZJGDMZBH = form1.OrgForm.ZZJGDMZBH;
            laspb.FDDBRXM = form1.OrgForm.FDDBRXM;
            laspb.ZW = form1.OrgForm.ZW;
            laspb.XM = form1.PersonForm.XM;
            laspb.XB = form1.PersonForm.XB;
            laspb.CSNY = form1.PersonForm.CSNY;
            laspb.MZ = form1.PersonForm.MZ;
            laspb.SFZH = form1.PersonForm.SFZH;
            laspb.GZDW = form1.PersonForm.GZDW;
            laspb.ZSD = form1.ZSD;
            laspb.LXDH = form1.LXDH;

            laspb.AQZY = form1.AQZY;
            laspb.LALY = form1.LALY;
            laspb.NBYJ = form1.NBYJ;
            laspb.NBYJQM = form1.ProcessUser.UserName;
            laspb.NBYJQMRQ = form1.ProcessTime.Value.ToString("yyyy年MM月dd日");

            laspb.Approve = form2.Approved.Value;
            laspb.CBRY = string.Join("、", form2.ZBDY.UserName, form2.XBDY.UserName);
            laspb.LDSPYJQM = form2.ProcessUser.UserName;
            laspb.LDSPYJQMRQ = form2.ProcessTime.Value.ToString("yyyy年MM月dd日");

            laspb.FZCSFTY = form3.Approved;
            laspb.FZCSPYJ = form3.FZJGYJ;
            laspb.FZCSPYJQMRQ = form3.ProcessTime.Value.ToString("yyyy年MM月dd日");
            laspb.FZCQM = form3.ProcessUser.UserName;

            laspb.FGFJZSFTY = viewModel4.Approved;
            laspb.FGFJZSPYJ = viewModel4.FGFJZYJ;
            laspb.FGFJZSPRQ = DateTime.Now.ToString("yyyy年MM月dd日");
            laspb.FGLD = form4.ProcessUser.UserName;
            string savePDFFilePath = DocBuildBLL.BuildLASPB(
                SessionManager.User.RegionName, caseForm.WICode, laspb);

            DOCINSTANCE docIntance = new DOCINSTANCE()
            {
                DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                DDID = DocDefinition.LASPB,
                DOCTYPEID = (decimal)DocTypeEnum.PDF,
                AIID = activity.AIID,
                DPID = DocBLL.GetDPIDByADID(activity.ADID),
                VALUE = Serializer.Serialize(laspb),
                ASSEMBLYNAME = laspb.GetType().Assembly.FullName,
                TYPENAME = laspb.GetType().FullName,
                WIID = viewModel4.WIID,
                DOCPATH = savePDFFilePath,
                DOCNAME = "立案审批表",
                CREATEDTIME = DateTime.Now
            };

            DocBLL.AddDocInstance(docIntance, true);
            activity.Submit();

            //是否回退
            string isBack = this.Request.Form["Approved"];
            string SMSUserName = this.Request.Form["BackProcessUserName"];
            string megContent = "";
            //回退
            if (isBack == "False")
            {
                //短信内容
                megContent = SMSUserName + ",您在案件管理子系统中有一条新任务等待处理";
            }
            //不是回退，短信提醒人为下一流程处理人
            else
            {
                string SMStoUserName = this.Request.Form["ZBDYNAME"];
                megContent = SMStoUserName + ",您在案件管理子系统中有一条新任务等待处理";
            }
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
