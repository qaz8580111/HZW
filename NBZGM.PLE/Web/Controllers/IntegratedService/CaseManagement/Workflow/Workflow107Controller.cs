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
    /// <summary>
    /// 主办队员提出处理意见
    /// </summary>
    public class Workflow107Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow107/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            //初始化认定违法事实
            string rddwfss = "";
            string fasj = Convert.ToDateTime(CaseForm.FinalForm.Form101.FASJ).ToString("yyyy年MM月dd日");
            if (CaseForm.FinalForm.Form101.DSRLX == "dw")
            {
                //当事人为单位时
                rddwfss = string.Format("　　当事人{0}于{1}", CaseForm.FinalForm.Form101.OrgForm.MC, fasj);
            }
            else
            {
                //当事人为个人
                rddwfss = string.Format("　　当事人{0}于{1}", CaseForm.FinalForm.Form101.PersonForm.XM, fasj);
            }

            //初始化证据
            List<ComplexDocInstance> allInstances =
                DocBLL.GetDocInstancesByWIID(CaseForm.WIID).ToList();
            string[] docNames = (from t in allInstances
                                 select t.DocInstance.DOCNAME
                                ).ToArray();

            ViewBag.zj = docNames;

            //违责
            string weize = string.Format("违反了{0}之规定", CaseForm.FinalForm.Form101.IllegalForm.WeiZe);

            //法则
            string faze = string.Format("依据{0}之规定", CaseForm.FinalForm.Form101.IllegalForm.FaZe);

            //主办队员意见
            string dsr = "";
            if (CaseForm.FinalForm.Form101.DSRLX == "dw")
            {
                //当事人为单位
                dsr = CaseForm.FinalForm.Form101.OrgForm.MC;
            }
            else
            {
                //当事人为个人
                dsr = CaseForm.FinalForm.Form101.PersonForm.XM;
            }
            string zbdyyj = "　　经调查，建议对当事人{0}作如下行政处罚\r";
            zbdyyj += "　　1、责令当事人\r";
            zbdyyj += "　　2、处罚人民币     元整（￥    元）罚款。\r";
            zbdyyj += "　　妥否，请领导审批!";
            zbdyyj = string.Format(zbdyyj, dsr);
            ViewModel7 viewModel7 = new ViewModel7
            {
                AIID = AIID,
                WIID = CaseForm.WIID,
                RDDWFSS = rddwfss,
                WFDFLFGHGZ = weize,
                CFYJ = faze,
                DCZJDCBRYJ = zbdyyj
            };

            if (CaseForm.FinalForm.Form107 != null)
            {
                if (CaseForm.FinalForm.Form107.CFJE != 0)
                    viewModel7.CFJE = CaseForm.FinalForm.Form107.CFJE;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form107.CFYJ))
                    viewModel7.CFYJ = CaseForm.FinalForm.Form107.CFYJ;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form107.RDDWFSS))
                    viewModel7.RDDWFSS = CaseForm.FinalForm.Form107.RDDWFSS;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form107.CLFS))
                    viewModel7.CLFS = CaseForm.FinalForm.Form107.CLFS;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form107.WFDFLFGHGZ))
                    viewModel7.WFDFLFGHGZ = CaseForm.FinalForm.Form107.WFDFLFGHGZ;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form107.ZJ))
                    viewModel7.ZJ = CaseForm.FinalForm.Form107.ZJ;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form107.DCZJHCBRYJ))
                    viewModel7.DCZJDCBRYJ = CaseForm.FinalForm.Form107.DCZJHCBRYJ;
                if (!string.IsNullOrWhiteSpace(CaseForm.FinalForm.Form107.THYJ))
                    viewModel7.THYJ = CaseForm.FinalForm.Form107.THYJ;
            }

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewModel7);
        }

        public ActionResult Commit(ViewModel7 viewModel7)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel7.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel7.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            caseForm.FinalForm.Form107.ID = activity.AIID;
            caseForm.FinalForm.Form107.RDDWFSS = viewModel7.RDDWFSS;
            caseForm.FinalForm.Form107.ZJ = viewModel7.ZJ;
            caseForm.FinalForm.Form107.WFDFLFGHGZ = viewModel7.WFDFLFGHGZ;
            caseForm.FinalForm.Form107.CFYJ = viewModel7.CFYJ;
            caseForm.FinalForm.Form107.CLFS = viewModel7.CLFS;
            caseForm.FinalForm.Form107.CFJE = viewModel7.CFJE;
            caseForm.FinalForm.Form107.DCZJHCBRYJ = viewModel7.DCZJDCBRYJ;
            caseForm.FinalForm.Form107.ProcessUser = SessionManager.User;
            caseForm.FinalForm.Form107.ProcessTime = DateTime.Now;

            Form101 form1 = caseForm.FinalForm.Form101;
            Form107 form7 = caseForm.FinalForm.Form107;
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
            //调查终结后承办人意见
            ajclspb.DCJBHCBRYJ = form7.DCZJHCBRYJ;
            //调查终结后承办人签章
            ajclspb.DCJBHCBRZ = form7.ProcessUser.UserName;
            //调查终结后承办人签章日期
            ajclspb.DCJBHCBRQ = form7.ProcessTime.Value;

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
                WIID = viewModel7.WIID,
                DOCPATH = savePDFFilePath,
                DOCNAME = "案件处理审批表",
                CREATEDTIME = DateTime.Now
            };

            DocBLL.AddDocInstance(docInstance, true);
            activity.Submit();

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
