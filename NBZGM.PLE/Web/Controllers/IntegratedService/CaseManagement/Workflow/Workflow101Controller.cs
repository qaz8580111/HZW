using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.Model.CaseDocModels;
using Web.ViewModels;
using Web.Workflows;
using Taizhou.PLE.WorkflowLib;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Web.ViewModels.CaseViewModels;
using Taizhou.PLE.BLL.CaseBLLs;
using Web;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.WorkFlowBLLs;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.BLL.UserBLLs;

namespace Taizhou.PLE.Web.Controllers
{
    /// <summary>
    /// 执法队员提出立案申请
    /// </summary>
    public class Workflow101Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/Workflow101/";

        /// <summary>
        /// 初始化承办人员提出立案建议表单
        /// </summary>
        /// <param name="AIID">环节标识</param>
        /// <param name="CaseForm">案件信息</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string WIID, string AIID, CaseForm CaseForm)
        {
            Form101 form101 = new Form101();
            if (CaseForm != null)
            {
                form101 = CaseForm.FinalForm.Form101;
            }
            //判断该案件是否退回
            ViewBag.IsRollback = "0";
            if (CaseForm != null)
            {
                if (CaseForm.FinalForm.Form102 != null)
                {
                    ViewBag.IsRollback = "1";
                }
            }
            //案卷来源
            List<SelectListItem> AJLYSelectList = new List<SelectListItem>()
            {
              (new SelectListItem() {Text = "巡查发现", Value = "1"}),
              (new SelectListItem() {Text = "交办移送", Value = "2"}),
              (new SelectListItem() {Text = "数字城管", Value = "3"}),
              (new SelectListItem() {Text= "规划核实发现",Value="5"}),
              (new SelectListItem() {Text="其他",Value="6"})
            };
            ViewBag.AJLYSelectList = AJLYSelectList;

            //性别
            List<SelectListItem> XBSelectList = new List<SelectListItem>()
            {
              (new SelectListItem() {Text = "男", Value = "男"}),
              (new SelectListItem() {Text = "女", Value = "女"})
            };
            ViewBag.XBSelectList = XBSelectList;

            #region 绑定承办单位和承办领导下拉框数据

            List<UNIT> cbdwList = UnitBLL.GetCBDW();
            string selectedCBDWID = "";
            List<SelectListItem> CBLDSelectList = new List<SelectListItem>(); ;

            if (form101.CBLDID != null)
            {
                selectedCBDWID = form101.CBDWID.ToString();
                CBLDSelectList = WorkflowBLL.GetCBLDsByUnitID(form101.CBDWID.Value)
                    .ToList()
                    .Select(t => new SelectListItem
                {
                    Value = t.USERID.ToString(),
                    Text = string.Format("{0}({1})", t.USERNAME, t.USERPOSITION.USERPOSITIONNAME),
                    Selected = t.USERID == form101.CBLDID ? true : false
                }).ToList();
            }

            ViewBag.CBDWSelectList = new SelectList(cbdwList, "UNITID", "UNITNAME", selectedCBDWID);
            ViewBag.CBLDSelectList = CBLDSelectList;

            #endregion

            #region 绑定违法行为下拉框数据

            //违法行为大类列表
            List<SelectListItem> dlSelectList = IllegalItemBLL
                .GetIllegalClassesByParentID(null).ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.ILLEGALCODE + "  " + c.ILLEGALCLASSNAME,
                    Value = c.ILLEGALCLASSID.ToString()
                }).ToList();
            //违法行为小类列表
            List<SelectListItem> xlSelectList = new List<SelectListItem>();
            //违法行为子类列表
            List<SelectListItem> zlSelectList = new List<SelectListItem>();
            //违法行为列表
            List<SelectListItem> wfxwSelectList = new List<SelectListItem>();

            //拟办队员
            List<USER> userlist = UserBLL.GetAllUsers().Where(t => t.UNITID == SessionManager.User.UnitID).ToList();
            List<SelectListItem> nbdySelectList1 = userlist.Select(t => new SelectListItem()
                {
                    Text = t.USERNAME,
                    Value = t.USERID.ToString(),
                    Selected = t.USERID == SessionManager.User.UserID ? true : false
                }).ToList();
            List<SelectListItem> nbdySelectList2 = userlist.Select(t => new SelectListItem()
            {
                Text = t.USERNAME,
                Value = t.USERID.ToString(),
            }).ToList();
            nbdySelectList1.Insert(0, new SelectListItem() { Text = "请选择", Value = "-1" });
            nbdySelectList2.Insert(0, new SelectListItem() { Text = "请选择", Value = "-1" });
            form101.NBDYID1 = (int)SessionManager.User.UserID;
            form101.NBDYNAME1 = SessionManager.User.UserName;
            ViewBag.nbdySelectList1 = nbdySelectList1;
            ViewBag.nbdySelectList2 = nbdySelectList2;

            if (form101.IllegalForm != null && form101.IllegalForm.ID != null)
            {
                //大类标识
                decimal? dlID = null;
                //违法行为小类集合、违法行为子类集合、违法行为集合
                List<IllegalClassSelectItem> xlList, zlList, wfxwList;

                IllegalItemBLL.GetILLEGALITEMByID(form101.IllegalForm.ID,
                    out zlList, out xlList, out wfxwList, out dlID);
                var result = dlSelectList.SingleOrDefault(t => t.Value == dlID.Value.ToString());
                result.Selected = true;

                xlSelectList = xlList.Select(c => new SelectListItem
               {
                   Text = c.Text,
                   Value = c.Value,
                   Selected = c.Selected
               }).ToList();

                zlSelectList = zlList.Select(c => new SelectListItem
            {
                Text = c.Text,
                Value = c.Value,
                Selected = c.Selected
            }).ToList();

                wfxwSelectList = wfxwList
             .Select(c => new SelectListItem
             {
                 Text = c.Text,
                 Value = c.Value,
                 Selected = c.Selected
             }).ToList();
                wfxwSelectList.Insert(0, new SelectListItem
            {
                Text = "请选择违法行为",
                Value = ""
            });
            }

            ViewBag.dlSelectList = dlSelectList;
            ViewBag.xlSelectList = xlSelectList;
            ViewBag.zlSelectList = zlSelectList;
            ViewBag.wfxwSelectList = wfxwSelectList;

            #endregion

            ViewModel1 viewMode1 = new ViewModel1();
            if (CaseForm != null)
            {
                //初始化表单数据
                viewMode1 = new ViewModel1
               {
                   WIID = CaseForm.WIID.ToString(),
                   WSBH = string.IsNullOrWhiteSpace(form101.WSBH) ? DocBuildBLL.GetLASPBCode() : form101.WSBH,
                   AIID = form101.ID,
                   AJLYID = form101.AJLYID,
                   AJLYName = form101.AJLYName,
                   AQZY = form101.AQZY,
                   AY = form101.AY,
                   SFWZDAN = form101.SFWZDAN,
                   CBLDID = form101.CBLDID,
                   CBLDName = form101.CBLDName,
                   CBDWID = form101.CBDWID,
                   CBDWName = form101.CBDWName,
                   DSRLX = form101.DSRLX,
                   OrgForm = form101.OrgForm,
                   PersonForm = form101.PersonForm,
                   ZSD = form101.ZSD,
                   LXDH = form101.LXDH,
                   FADD = form101.FADD,
                   FASJ = form101.FASJ,
                   LALY = form101.LALY,
                   NBYJ = form101.NBYJ,
                   SFLA = form101.SFLA,
                   IllegalForm = form101.IllegalForm,
                   THYJ = form101.THYJ,
                   NBDYID1 = form101.NBDYID1,
                   NBDYID2 = form101.NBDYID2,
                   NBDYNAME1 = form101.NBDYNAME1,
                   NBDYNAME2 = form101.NBDYNAME2
               };
            }
            else
            {
                viewMode1.WSBH = DocBuildBLL.GetLASPBCode();
            }
            return PartialView(THIS_VIEW_PATH + "Index.cshtml", viewMode1);
        }

        /// <summary>
        /// 提交承办人员提出立案建议表单
        /// </summary>
        /// <param name="viewMode1">承办人员提出立案建议表单</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Commit(ViewModel1 viewMode1)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(viewMode1.WIID);
            Activity activity = caseWorkflow.Workflow.Activities[viewMode1.AIID];
            CaseForm caseForm = caseWorkflow.CaseForm;
            caseWorkflow.Workflow.WIName = viewMode1.AY;
            caseForm.WIName = viewMode1.AY;
            Form101 form101 = caseForm.FinalForm.Form101;


            //当事人基本情况
            form101.DSRLX = viewMode1.DSRLX;
            form101.OrgForm = viewMode1.OrgForm == null ? new OrgForm() : viewMode1.OrgForm;
            form101.PersonForm = viewMode1.PersonForm == null ? new PersonForm() : viewMode1.PersonForm;
            form101.ZSD = viewMode1.ZSD;
            form101.LXDH = viewMode1.LXDH;
            form101.WSBH = viewMode1.WSBH;
            form101.IllegalForm = viewMode1.IllegalForm;
            form101.AY = viewMode1.AY;
            form101.FADD = viewMode1.FADD;
            form101.FASJ = viewMode1.FASJ;
            form101.AJLYID = viewMode1.AJLYID;
            form101.AJLYName = viewMode1.AJLYName;
            form101.SFLA = viewMode1.SFLA;
            form101.SFWZDAN = viewMode1.SFWZDAN;
            form101.AQZY = viewMode1.AQZY;
            form101.LALY = viewMode1.LALY;
            form101.NBYJ = viewMode1.NBYJ;
            form101.CBDWID = viewMode1.CBDWID;
            form101.CBDWName = viewMode1.CBDWName;
            form101.CBLDID = viewMode1.CBLDID;
            form101.CBLDName = viewMode1.CBLDName;
            form101.NBDYID1 = viewMode1.NBDYID1;
            form101.NBDYID2 = viewMode1.NBDYID2;
            form101.NBDYNAME1 = viewMode1.NBDYNAME1;
            form101.NBDYNAME2 = viewMode1.NBDYNAME2;

            form101.ProcessUser = SessionManager.User;
            form101.ProcessTime = DateTime.Now;

            LASPB laspb = new LASPB();
            laspb.WSBH = form101.WSBH;
            laspb.AY = form101.AY;
            laspb.FADD = form101.FADD;
            laspb.FASJ = form101.FASJ;
            laspb.AJLY = form101.AJLYName;
            laspb.MC = form101.OrgForm.MC;

            if (form101.DSRLX == "dw")
            {
                laspb.ZZJGDMZBH = form101.OrgForm.ZZJGDMZBH;
                laspb.FDDBRXM = form101.OrgForm.FDDBRXM;
                laspb.ZW = form101.OrgForm.ZW;
            }
            else if (form101.DSRLX == "gr")
            {
                laspb.XM = form101.PersonForm.XM;
                laspb.XB = form101.PersonForm.XB;
                laspb.CSNY = form101.PersonForm.CSNY;
                laspb.MZ = form101.PersonForm.MZ;
                laspb.SFZH = form101.PersonForm.SFZH;
                laspb.GZDW = form101.PersonForm.GZDW;
            }

            laspb.ZSD = form101.ZSD;
            laspb.LXDH = form101.LXDH;
            laspb.AQZY = form101.AQZY;
            laspb.LALY = form101.LALY;
            laspb.NBYJ = form101.NBYJ;
            laspb.NBYJQM = form101.ProcessUser.UserName;
            laspb.NBYJQMRQ = form101.ProcessTime.Value.ToString("yyyy年MM月dd日");
            laspb.NBDYNAME1 = form101.NBDYNAME1;
            laspb.NBDYNAME2 = form101.NBDYNAME2;

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
                WIID = caseWorkflow.CaseForm.WIID,
                DOCPATH = savePDFFilePath,
                CREATEDTIME = DateTime.Now,
                DOCBH = laspb.WSBH,
                DOCNAME = "立案审批表"
            };

            DocBLL.AddDocInstance(docIntance, true);
            activity.Submit();
            //短信内容
            string megContent = viewMode1.CBLDName + ",您在案件管理子系统中有一条新任务等待处理";
            //电话号码
            string phoneNumber = this.Request.Form["FSDX"];
            //发送短信
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                SMSUtility.SendMessage(phoneNumber, megContent + "[" + SessionManager.User.UserName + "]", DateTime.Now.Ticks);
            }
            return RedirectToAction("PendingCaseList", "GeneralCase");
        }

        //暂存
        [HttpPost]
        public ActionResult SaveForm(ViewModel1 viewMode1)
        {
            CaseWorkflow caseWorkflow;
            if (!string.IsNullOrWhiteSpace(viewMode1.WIID))
            {
                caseWorkflow = new CaseWorkflow(viewMode1.WIID);
            }
            else
            {
                caseWorkflow = new CaseWorkflow();
            }
            CaseForm caseForm = caseWorkflow.CaseForm;
            caseWorkflow.Workflow.WIName = viewMode1.AY;
            caseForm.WIName = viewMode1.AY;
            Form101 form101 = caseForm.FinalForm.Form101;

            form101.WSBH = viewMode1.WSBH;
            //当事人基本情况
            form101.DSRLX = viewMode1.DSRLX;
            form101.OrgForm = viewMode1.OrgForm == null ? new OrgForm() : viewMode1.OrgForm;
            form101.PersonForm = viewMode1.PersonForm == null ? new PersonForm() : viewMode1.PersonForm;
            form101.ZSD = viewMode1.ZSD;
            form101.LXDH = viewMode1.LXDH;

            form101.IllegalForm = viewMode1.IllegalForm;
            form101.AY = viewMode1.AY;
            form101.FADD = viewMode1.FADD;
            form101.FASJ = viewMode1.FASJ;
            form101.AJLYID = viewMode1.AJLYID;
            form101.AJLYName = viewMode1.AJLYName;
            form101.SFLA = viewMode1.SFLA;
            form101.SFWZDAN = viewMode1.SFWZDAN;
            form101.AQZY = viewMode1.AQZY;
            form101.LALY = viewMode1.LALY;
            form101.NBYJ = viewMode1.NBYJ;
            form101.CBDWID = viewMode1.CBDWID;
            form101.CBDWName = viewMode1.CBDWName;
            form101.CBLDID = viewMode1.CBLDID;
            form101.CBLDName = viewMode1.CBLDName;
            form101.NBDYID1 = viewMode1.NBDYID1;
            form101.NBDYID2 = viewMode1.NBDYID2;
            form101.NBDYNAME1 = viewMode1.NBDYNAME1;
            form101.NBDYNAME2 = viewMode1.NBDYNAME2;

            caseWorkflow.Workflow.CommitChanges();

            return this.Redirect(string.Format("/Workflow/WorkflowProcess?WIID={0}&AIID={1}"
                , caseWorkflow.CaseForm.WIID, caseWorkflow.CaseForm.FinalForm.Form101.ID));
        }

        public JsonResult GetIllegalClasses()
        {
            string strIllegaClassID = this.Request.QueryString["IllegaClassID"];
            decimal illegaClassID = 0.0M;

            if (!decimal.TryParse(strIllegaClassID, out illegaClassID))
            {
                return null;
            }

            IQueryable<ILLEGALCLASS> illegalClasses = IllegalItemBLL
                .GetIllegalClassesByParentID(illegaClassID);

            List<SelectModel> illegalClasseList = new List<SelectModel>();

            foreach (var temp in illegalClasses)
            {
                illegalClasseList.Add(new SelectModel
                {
                    ID = temp.ILLEGALCLASSID,
                    Name = temp.ILLEGALCODE + " " + temp.ILLEGALCLASSNAME
                });
            }

            return Json(illegalClasseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIllegalItems()
        {
            string strIllegaClassID = this.Request.QueryString["IllegaClassID"];
            decimal illegaClassID = 0.0M;

            if (!decimal.TryParse(strIllegaClassID, out illegaClassID))
            {
                return null;
            }

            IQueryable<ILLEGALITEM> illegalItems = IllegalItemBLL
                .GetIllegalItemByClassID(illegaClassID);

            List<SelectModel> illegalList = new List<SelectModel>();
            foreach (ILLEGALITEM temp in illegalItems)
            {
                illegalList.Add(new SelectModel
                {
                    ID = temp.ILLEGALITEMID,
                    Name = temp.ILLEGALCODE + " " + temp.ILLEGALITEMNAME
                });
            }

            return Json(illegalList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回违法行为信息
        /// </summary>
        /// <returns>违法行为信息</returns>
        public JsonResult GetIllegalInfomation()
        {
            //违法行为标识
            string strIllegalItemID = this.Request.QueryString["IllegalItemID"];
            decimal illegalItemID = 0.0M;

            if (!decimal.TryParse(strIllegalItemID, out illegalItemID))
            {
                return null;
            }

            ILLEGALITEM illegalItem = IllegalItemBLL
                .GetIllegalItemByIllegalItemID(illegalItemID);

            var temp = new
            {
                ILLEGALITEMNAME = illegalItem.ILLEGALITEMNAME,
                WEIZE = illegalItem.WEIZE,
                FZZE = illegalItem.FZZE,
                ILLEGALCODE = illegalItem.ILLEGALCODE,
                //处罚内容
                PENALTYCONTENT = illegalItem.PENALTYCONTENT
            };

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCBLDs(string UnitID)
        {
            decimal unitID = 0.0M;

            if (!decimal.TryParse(UnitID, out unitID))
            {
                return null;
            }

            IQueryable<USER> CBLDList = WorkflowBLL.GetCBLDsByUnitID(unitID);

            return Json(CBLDList.ToList().Select(t => new
            {
                ID = t.USERID,
                Name = string.Format("{0}({1})", t.USERNAME, t.USERPOSITION.USERPOSITIONNAME)
            }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证文书编号是否唯一(退回时用到)
        /// </summary>
        /// <param name="DDID">文书类型</param>
        /// <param name="WSBH">文书编号</param>
        /// <param name="WIID">流程标识</param>
        /// <returns>true唯一，false已存在编号</returns>
        [HttpPost]
        public bool ValidateRollbackWSBH(decimal DDID, string WSBH, string WIID)
        {
            PLEEntities db = new PLEEntities();
            bool res = false;
            var docinstances = db.DOCINSTANCES.Where(t => t.DDID == DDID && t.DOCBH == WSBH && t.WIID != WIID);
            if (docinstances.Count() < 1)
            {
                res = true;
            }
            return res;
        }
    }
}
