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
using Taizhou.PLE.WorkflowLib;
using Web.ViewModels.CaseViewModels;
using Web.Workflows;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow
{
    public class Workflow109Controller : Controller
    {
        /// <summary>
        ///办案单位确认
        /// </summary>

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow109/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            ViewModel9 viewModel9 = new ViewModel9
            {
                AIID = AIID,
                WIID = CaseForm.WIID
            };
            if (CaseForm.FinalForm.Form109 != null)
            {
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form109.THYJ))
                viewModel9.THYJ = CaseForm.FinalForm.Form109.THYJ;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form109.BADWYJ))
                viewModel9.BADWYJ = CaseForm.FinalForm.Form109.BADWYJ;
            }
            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel9);
        }

        public ActionResult Commit(ViewModel9 viewModel9)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel9.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel9.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form109.ID = activity.AIID;
            caseForm.FinalForm.Form109.Approved = viewModel9.Approved;
            caseForm.FinalForm.Form109.ProcessTime = DateTime.Now;
            caseForm.FinalForm.Form109.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form109.BADWYJ = viewModel9.BADWYJ;

            Form101 form1 = caseForm.FinalForm.Form101;
            Form107 form7 = caseForm.FinalForm.Form107;
            Form108 form8 = caseForm.FinalForm.Form108;
            Form109 form9 = caseForm.FinalForm.Form109;

            AJCLSPB ajclspb = new AJCLSPB();
            ajclspb.AY = form1.AY;
            ajclspb.AJLY = form1.AJLYName;
            ajclspb.MC = form1.OrgForm.MC;
            ajclspb.ZZJGDMZBH = form1.OrgForm.ZZJGDMZBH;
            ajclspb.FDDBRXM = form1.OrgForm.FDDBRXM;
            ajclspb.ZW = form1.OrgForm.ZW;
            ajclspb.XM = form1.PersonForm.XM;
            ajclspb.XB = form1.PersonForm.XB;
            ajclspb.CSNY = form1.PersonForm.CSNY;
            ajclspb.MZ = form1.PersonForm.MZ;
            ajclspb.SFZH = form1.PersonForm.SFZH;
            ajclspb.GZDW = form1.PersonForm.GZDW;

            ajclspb.ZSD = form1.ZSD;
            ajclspb.LXDH = form1.LXDH;

            ajclspb.RDDWFSS = form7.RDDWFSS;
            ajclspb.ZJ = form7.ZJ;
            ajclspb.WFDFLFGHGZ = form7.WFDFLFGHGZ;
            ajclspb.CFYJ = form7.CFYJ;
            // 承办人（主办）意见
            ajclspb.DCJBHCBRYJ = form7.DCZJHCBRYJ;
            ajclspb.DCJBHCBRZ = form7.ProcessUser.UserName;
            ajclspb.DCJBHCBRQ = form7.ProcessTime.Value;

            //协办队员
            ajclspb.XBDYCLJG = form8.Approved;
            ajclspb.XBDYCLSJ = form8.ProcessTime;
            ajclspb.XBDYYJ = form8.XBDYYJ;
            ajclspb.XBDYQZ = form8.ProcessUser.UserName;

            //办案单位
            ajclspb.BADWRQ = DateTime.Now;
            ajclspb.BADWQZ = SessionManager.User.UserName;
            ajclspb.BADWYJ = viewModel9.CBYJ;
            ajclspb.BADWJG = viewModel9.Approved;

            string savePDFFilePath = DocBuildBLL.BuildAJCLSPB(
                SessionManager.User.RegionName, caseForm.WICode, ajclspb);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                DDID = DocDefinition.AJCLSPB,
                DOCTYPEID = (decimal)DocTypeEnum.PDF,
                AIID = activity.AIID,
                DPID = DocBLL.GetDPIDByADID(activity.ADID),
                VALUE = Serializer.Serialize(ajclspb),
                ASSEMBLYNAME = ajclspb.GetType().Assembly.FullName,
                TYPENAME = ajclspb.GetType().FullName,
                WIID = viewModel9.WIID,
                DOCPATH = savePDFFilePath,
                DOCNAME = "案件处理审批表",
                CREATEDTIME = DateTime.Now
            };

            DocBLL.AddDocInstance(docInstance, true);
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
                string SMStoUserName = this.Request.Form["FZCSHRNAME"];
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
