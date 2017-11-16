using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.WorkFlowBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;
using Web;
using Web.ViewModels.CaseViewModels;
using Web.Workflows;

namespace Taizhou.PLE.Web.Controllers
{
    /// <summary>
    /// 办案单位审核立案建议
    /// </summary>
    public class Workflow102Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow102/";

        [HttpGet]
        public ActionResult Index(string AIID, CaseForm CaseForm)
        {
            Form101 form101 = CaseForm.FinalForm.Form101;
            Form102 form102 = CaseForm.FinalForm.Form102;
            ViewModel2 viewmodel2 = new ViewModel2();
            //绑定分管领导列表
            IQueryable<USER> FGLDs = WorkflowBLL.GetFGLDsByCBDWID(form101.CBDWID.Value);
            List<SelectListItem> FGLDSelectList = FGLDs.ToList().Select(c => new SelectListItem
            {
                Text = string.Format("{0}({1})", c.USERNAME, c.USERPOSITION.USERPOSITIONNAME),
                Value = c.USERID.ToString(),
            }).ToList();
            FGLDSelectList.Insert(0, new SelectListItem
            {
                Text = "请选分管领导",
                Value = ""
            });
            //绑定主办人员和
            decimal unitID = SessionManager.User.UnitID;
            IQueryable<USER> users = UserBLL.GetUserListByUID(form101.CBDWID);
            List<SelectListItem> ZBUserSelectList = users.ToList().Select(c => new SelectListItem
            {
                Text = c.USERNAME,
                Value = c.USERID.ToString()
            }).ToList();
            ZBUserSelectList.Insert(0, new SelectListItem
            {
                Text = "请选择队员",
                Value = ""
            });
            //协办人员列表
            List<SelectListItem> XBUserSelectList = users.ToList().Select(c => new SelectListItem
            {
                Text = c.USERNAME,
                Value = c.USERID.ToString()
            }).ToList();
            XBUserSelectList.Insert(0, new SelectListItem
            {
                Text = "请选择队员",
                Value = ""
            });

            //法制处审核人
            IQueryable<USER> fzcusers = UserBLL.GetUsersByUnitID(30);
            List<SelectListItem> FZCUserSelectList = fzcusers.ToList().Select(c => new SelectListItem
            {
                Text = c.USERNAME,
                Value = c.USERID.ToString()
            }).ToList();
            FZCUserSelectList.Insert(0, new SelectListItem
            {
                Text = "请选择法制处审核人",
                Value = ""
            });
            ViewBag.FZCUserSelectList = FZCUserSelectList;

            #region 初始化主办单位审批数据
            viewmodel2.LDSPYJ = form102.LDSPYJ;

            if (form102 != null && form102.FGLDID != 0)
            {
                viewmodel2.FGLDID = form102.FGLDID;
                viewmodel2.FGLDName = form102.FGLDName;
            }

            if (form102 != null && form102.FZCSHR != null)
            {
                viewmodel2.FZCSHRBH = form102.FZCSHR.UserID;
            }

            if (form102 != null && form102.ZBDY != null)
            {
                viewmodel2.ZBDY = form102.ZBDY.UserID.ToString();
            }

            if (form102 != null && form102.XBDY != null)
            {
                viewmodel2.XBDY = form102.XBDY.UserID.ToString();
            }
            if (form102 != null)
            {
                viewmodel2.THYJ = form102.THYJ;
            }
            #endregion

            ViewBag.FGLDSelectList = FGLDSelectList;
            ViewBag.ZBUserSelectList = ZBUserSelectList;
            ViewBag.XBUserSelectList = XBUserSelectList;
            ViewBag.CaseForm = CaseForm;
            ViewData["WIID"] = CaseForm.WIID;
            ViewData["AIID"] = AIID;

            return PartialView(THIS_VIEW_PATH + "index.cshtml", viewmodel2);
        }

        public ActionResult Commit(ViewModel2 viewModel2)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewModel2.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewModel2.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;

            Form101 form1 = caseForm.FinalForm.Form101;
            Form102 form2 = caseForm.FinalForm.Form102;

            if (!string.IsNullOrWhiteSpace(viewModel2.ZBDY))
            {
                decimal id = decimal.Parse(viewModel2.ZBDY);
                form2.ZBDY = UserBLL.GetUserByID(id);
            }

            if (!string.IsNullOrWhiteSpace(viewModel2.XBDY))
            {
                decimal id = decimal.Parse(viewModel2.XBDY);
                form2.XBDY = UserBLL.GetUserByID(id);
            }

            User FZCSHRUser = new User()
            {
                UserID = viewModel2.FZCSHRBH,
                UserName = viewModel2.FZCSHR
            };
            form2.FZCSHR = FZCSHRUser;
            form2.FGLDID = viewModel2.FGLDID;
            form2.FGLDName = viewModel2.FGLDName;
            form2.LDSPYJ = viewModel2.LDSPYJ;
            form2.Approved = viewModel2.Approved;
            form2.ProcessUser = SessionManager.User;
            form2.ProcessTime = DateTime.Now;

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
            laspb.FZCQM = form2.FZCSHR.UserName;
            laspb.CBRY = string.Join("、", form2.ZBDY.UserName, form2.XBDY.UserName);
            laspb.LDSPYJQM = form2.ProcessUser.UserName;
            laspb.LDSPYJQMRQ = form2.ProcessTime.Value.ToString("yyyy年MM月dd日");

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
                WIID = viewModel2.WIID,
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
                megContent = viewModel2.FZCSHR + ",您在案件管理子系统中有一条新任务等待处理";
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
