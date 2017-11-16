using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model.RelevantItemWorkflowModels;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;
using Web.ViewModels.RelevantItemViewModels;
using Web.Workflows;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.BLL.UnitBLLs;

namespace Web.Controllers.IntegratedService.CaseManagement.RelevantItem
{
    public class Workflow201Controller : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/RelevantItemWorkflow/Workflow201/";

        [HttpGet]
        public ActionResult Index(CaseForm CaseForm, RelevantItemForm RelevantItemForm)
        {
            string parentWIID = CaseForm.WIID;
            ViewBag.CaseForm = CaseForm;

            //获取承办单位
            List<UNIT> cbdwList = UnitBLL.GetCBDW();
            ViewBag.CBDWSelectList = new SelectList(cbdwList, "UNITID", "UNITNAME");

            Form101 form101 = CaseForm.FinalForm.Form101;
            ViewModel201 viewModel201 = new ViewModel201();
            viewModel201.WSBH = DocBuildBLL.GetQTSXNBSPBCode();
            viewModel201.AY = form101.AY;
            viewModel201.LARQ = Convert.ToDateTime(form101.FASJ);
            viewModel201.ParentWIID = parentWIID;
            viewModel201.ParentAIID = CaseForm.FinalForm.CurrentForm.ID.ToString();
            //当事人类型
            if (form101.DSRLX == "dw")
            {
                viewModel201.MC = form101.OrgForm.MC;
                viewModel201.FDDBR = form101.OrgForm.FDDBRXM;
                viewModel201.ZW = form101.OrgForm.ZW;
            }
            else if (form101.DSRLX == "gr")
            {
                viewModel201.XM = form101.PersonForm.XM;
                viewModel201.MZ = form101.PersonForm.MZ;
                viewModel201.XB = form101.PersonForm.XB;
                viewModel201.SFZH = form101.PersonForm.SFZH;
                viewModel201.GZDW = form101.PersonForm.GZDW;
            }
            viewModel201.ZZ = form101.ZSD;
            viewModel201.DH = form101.LXDH;
            viewModel201.JYAQ = form101.AQZY;

            return PartialView(THIS_VIEW_PATH + "Index.cshtml", viewModel201);
        }

        [HttpPost]
        public ActionResult Commit(ViewModel201 vm)
        {
            RelevantItemWorkflow relevantItemWorkflow =
                new RelevantItemWorkflow(vm.ParentWIID);
            relevantItemWorkflow.Workflow.WIName = vm.SQSX;

            CaseForm caseForm = relevantItemWorkflow.ParentWorkflow.CaseForm;
            //一般案件流程活动标识
            string parentAIID = caseForm.FinalForm.CurrentForm.ID;
            //一般案件流程活动定义标识
            decimal parentADID = caseForm.FinalForm.CurrentForm.ADID;

            RelevantItemForm relevantItemForm = relevantItemWorkflow.RelevantItemForm;
            relevantItemForm.WIName = vm.SQSX;

            string aiid = relevantItemForm.RelevantItemForm1.ID;
            Activity activity = relevantItemWorkflow.Workflow.Activities[aiid];

            relevantItemForm.RelevantItemForm1.SQSX = vm.SQSX;
            relevantItemForm.RelevantItemForm1.WSBH = vm.WSBH;
            relevantItemForm.RelevantItemForm1.AY = vm.AY;
            relevantItemForm.RelevantItemForm1.LARQ = (DateTime)vm.LARQ;
            relevantItemForm.RelevantItemForm1.XM = vm.XM;
            relevantItemForm.RelevantItemForm1.XB = vm.XB;
            relevantItemForm.RelevantItemForm1.ZY = vm.ZY;
            relevantItemForm.RelevantItemForm1.MZ = vm.MZ;
            relevantItemForm.RelevantItemForm1.SFZH = vm.SFZH;
            relevantItemForm.RelevantItemForm1.MC = vm.MC;
            relevantItemForm.RelevantItemForm1.FDDBR = vm.FDDBR;
            relevantItemForm.RelevantItemForm1.ZW = vm.ZW;
            relevantItemForm.RelevantItemForm1.GZDW = vm.GZDW;
            relevantItemForm.RelevantItemForm1.DH = vm.DH;
            relevantItemForm.RelevantItemForm1.ZZ = vm.ZZ;
            relevantItemForm.RelevantItemForm1.YZBM = vm.YZBM;
            relevantItemForm.RelevantItemForm1.JYAQ = vm.JYAQ;
            relevantItemForm.RelevantItemForm1.CBRYJ = vm.CBRYJ;
            relevantItemForm.RelevantItemForm1.CBDWID = vm.CBDWID;
            relevantItemForm.RelevantItemForm1.CBDWName = vm.CBDWName;
            relevantItemForm.RelevantItemForm1.CBLDID = vm.CBLDID;
            relevantItemForm.RelevantItemForm1.CBLDName = vm.CBLDName;
            relevantItemForm.RelevantItemForm1.ProcessUser = SessionManager.User;
            relevantItemForm.RelevantItemForm1.ProcessTime = DateTime.Now;

            QTSXNBSPB qtsxnbspb = new QTSXNBSPB();
            qtsxnbspb.SQSX = relevantItemForm.RelevantItemForm1.SQSX;
            qtsxnbspb.WSBH = relevantItemForm.RelevantItemForm1.WSBH;
            qtsxnbspb.AY = relevantItemForm.RelevantItemForm1.AY;
            qtsxnbspb.LARQ = relevantItemForm.RelevantItemForm1.LARQ;
            qtsxnbspb.XM = relevantItemForm.RelevantItemForm1.XM;
            qtsxnbspb.XB = relevantItemForm.RelevantItemForm1.XB;
            qtsxnbspb.ZY = relevantItemForm.RelevantItemForm1.ZY;
            qtsxnbspb.MZ = relevantItemForm.RelevantItemForm1.MZ;
            qtsxnbspb.SFZHM = relevantItemForm.RelevantItemForm1.SFZH;
            qtsxnbspb.MC = relevantItemForm.RelevantItemForm1.MC;
            qtsxnbspb.FDDBR = relevantItemForm.RelevantItemForm1.FDDBR;
            qtsxnbspb.ZW = relevantItemForm.RelevantItemForm1.ZW;
            qtsxnbspb.GZDW = relevantItemForm.RelevantItemForm1.GZDW;
            qtsxnbspb.DH = relevantItemForm.RelevantItemForm1.DH;
            qtsxnbspb.ZZ = relevantItemForm.RelevantItemForm1.ZZ;
            qtsxnbspb.YZBM = relevantItemForm.RelevantItemForm1.YZBM;
            qtsxnbspb.JYAQ = relevantItemForm.RelevantItemForm1.YZBM;
            qtsxnbspb.CBRYJ = relevantItemForm.RelevantItemForm1.CBRYJ;
            qtsxnbspb.CBRQZ = relevantItemForm.RelevantItemForm1.ProcessUser.UserName;
            qtsxnbspb.CBRQZRQ = relevantItemForm.RelevantItemForm1.ProcessTime.Value.ToString("yyyy年MM月dd日");

            string qtsxnbspbPath = DocBuildBLL.DocBuildQTSXNBSPB(
               SessionManager.User.RegionName, caseForm.WICode, qtsxnbspb);
            DOCINSTANCE QTSXNBSPBDocIntance = new DOCINSTANCE()
            {
                DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                DDID = DocDefinition.QTSXNBSPB,
                DOCTYPEID = (decimal)DocTypeEnum.PDF,
                AIID = parentAIID,
                DPID = DocBLL.GetDPIDByADID(parentADID),
                VALUE = Serializer.Serialize(qtsxnbspb),
                ASSEMBLYNAME = qtsxnbspb.GetType().Assembly.FullName,
                TYPENAME = qtsxnbspb.GetType().FullName,
                WIID = relevantItemForm.ParentWIID,
                DOCPATH = qtsxnbspbPath,
                CREATEDTIME = DateTime.Now,
                DOCNAME = "其他事项内部审批表",
                DOCBH = qtsxnbspb.WSBH
            };
            DocBLL.AddRelevantDocInstance(QTSXNBSPBDocIntance, true);

            if (vm.DDID != null)
            {
                //相关事项审批关联文书路径
                string saveRelevantPDFFilePath = "";
                decimal? DDID = null;
                string assemblyName = null;
                string typeName = null;
                string value = null;
                string docName = null;
                string docBH = null;

                //查封（扣押）决定书
                if (vm.DDID == DocDefinition.CFKYJDS)
                {
                    //反序列化
                    string CFKYPWQDJson = Request.Form["CFKYPWQDJson"];
                    List<WPQDLB> CYQZWPQDList = JsonHelper
                        .JsonDeserialize<List<WPQDLB>>(CFKYPWQDJson);

                    CFKYJDS cfkyjds = vm.CFKYJDS;
                    cfkyjds.WPQDLBS = CYQZWPQDList;
                    saveRelevantPDFFilePath = DocBuildBLL.DocBuildCFKYJDS(SessionManager.User.RegionName,
                        caseForm.WICode, cfkyjds);
                    docBH = DocBuildBLL.GetCFKYJDSCode();
                    DDID = DocDefinition.CFKYJDS;
                    value = Serializer.Serialize(cfkyjds);
                    assemblyName = cfkyjds.GetType().Assembly.FullName;
                    typeName = cfkyjds.GetType().FullName;
                    docName = "查封（扣押）决定书";
                }
                //查封（扣押）通知书
                else if (vm.DDID == DocDefinition.CFKYTZS)
                {
                    //序列化
                    string CFKYTZSJson = Request.Form["CFKYTZSJson"];
                    List<CFKYWPQD> CFKYWPQDList = JsonHelper
                        .JsonDeserialize<List<CFKYWPQD>>(CFKYTZSJson);

                    CFKYTZS cfkytzs = vm.CFKYTZS;
                    cfkytzs.CFKYWPQDList = CFKYWPQDList;
                    docBH = DocBuildBLL.GetCFKYTZSCode();
                    saveRelevantPDFFilePath = DocBuildBLL.DocBuildCFKYTZS(SessionManager.User.RegionName,
                        caseForm.WICode, cfkytzs);
                    DDID = DocDefinition.CFKYTZS;
                    value = Serializer.Serialize(cfkytzs);
                    assemblyName = cfkytzs.GetType().Assembly.FullName;
                    typeName = cfkytzs.GetType().FullName;
                    docName = "查封（扣押）通知书";
                }
                //解除查封（扣押）通知书
                else if (vm.DDID == DocDefinition.JCCFKYTZS)
                {
                    //反序列化
                    string JCCFKYTZSJson = Request.Form["JCCFKYTZSJson"];
                    List<JCCFKYTZS.CFKYWPQD> JCCFKYWPList = JsonHelper
                        .JsonDeserialize<List<JCCFKYTZS.CFKYWPQD>>(JCCFKYTZSJson);
                    JCCFKYTZS jccfkytzs = vm.JCCFKYTZS;
                    jccfkytzs.JCCFKYWPList = JCCFKYWPList;
                    docBH = DocBuildBLL.GetJCCFKYTZSCode();
                    saveRelevantPDFFilePath = DocBuildBLL.DocBuildJCCFKYTZS(SessionManager.User.RegionName,
                       caseForm.WICode, jccfkytzs);
                    DDID = DocDefinition.JCCFKYTZS;
                    value = Serializer.Serialize(jccfkytzs);
                    assemblyName = jccfkytzs.GetType().Assembly.FullName;
                    typeName = jccfkytzs.GetType().FullName;
                    docName = "解除查封（扣押）通知书";
                }
                //先行登记保存证据通知书     
                else if (vm.DDID == DocDefinition.XXDJBCZJTZS)
                {
                    //反序列化
                    string XXDJBCZJJson = Request.Form["XXDJBCZJJson"];
                    List<XXDJBCZJQD> XXDJBCZJQDList = JsonHelper
                        .JsonDeserialize<List<XXDJBCZJQD>>(XXDJBCZJJson);
                    docBH = DocBuildBLL.XXDJBCZJTZSCode();
                    XXDJBCZJTZS xxdjbczjtzs = vm.XXDJBCZJTZS;
                    xxdjbczjtzs.XXDJBCZJQDList = XXDJBCZJQDList;
                    saveRelevantPDFFilePath = DocBuildBLL.DocBuildXXDJBCZJTZS(SessionManager.User.RegionName,
                        caseForm.WICode, xxdjbczjtzs);
                    DDID = DocDefinition.XXDJBCZJTZS;
                    value = Serializer.Serialize(xxdjbczjtzs);
                    assemblyName = xxdjbczjtzs.GetType().Assembly.FullName;
                    typeName = xxdjbczjtzs.GetType().FullName;
                    docName = "先行登记保存证据通知书";
                }
                //先行登记保存证据物品处理通知书
                else if (vm.DDID == DocDefinition.XXDJBCZJWPCLTZS)
                {
                    //反序列化
                    string XXDJBCZJWPCLJson = Request.Form["XXDJBCZJWPCLJson"];
                    List<XXDJBCZJWPCLQD> JCCFKYWPList = JsonHelper
                        .JsonDeserialize<List<XXDJBCZJWPCLQD>>(XXDJBCZJWPCLJson);
                    docBH = DocBuildBLL.XXDJBCZJWPCLTZSCode();
                    XXDJBCZJWPCLTZS xxdjbczjwpcltzs = vm.XXDJBCZJWPCLTZS;
                    xxdjbczjwpcltzs.XXDJBCZJWPCLQDList = JCCFKYWPList;
                    saveRelevantPDFFilePath = DocBuildBLL.DocBuildXXDJBCZJWPCLTZS(SessionManager.User.RegionName,
                        caseForm.WICode, xxdjbczjwpcltzs);
                    DDID = DocDefinition.XXDJBCZJWPCLTZS;
                    value = Serializer.Serialize(xxdjbczjwpcltzs);
                    assemblyName = xxdjbczjwpcltzs.GetType().Assembly.FullName;
                    typeName = xxdjbczjwpcltzs.GetType().FullName;
                    docName = "先行登记保存证据物品处理通知书";
                }

                DOCINSTANCE relevantItemDocIntance = new DOCINSTANCE
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = DDID,
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    DOCBH = docBH,
                    AIID = parentAIID,
                    DPID = DocBLL.GetDPIDByADID(parentADID),
                    VALUE = value,
                    ASSEMBLYNAME = assemblyName,
                    TYPENAME = typeName,
                    WIID = relevantItemForm.ParentWIID,
                    DOCPATH = saveRelevantPDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = docName
                };
                DocBLL.AddDocInstance(relevantItemDocIntance, false);
                relevantItemForm.SPSXForm = new SPSXForm
                {
                    SPSXDIID = relevantItemDocIntance.DOCINSTANCEID,
                    SPSXDIName = relevantItemDocIntance.DOCNAME,
                    SPSXDISrc = relevantItemDocIntance.DOCPATH
                };
            }
            activity.Submit();

            return RedirectToAction("PendingCaseList", "GeneralCase");
        }

        /// <summary>
        /// 获取相关事项审批文书编号 已存在返回false，不存在返回true
        /// </summary>
        public bool GetXGSXSPWSBH(string wiid, decimal ddid, string docbh)
        {
            PLEEntities db = new PLEEntities();
            var result = db.DOCINSTANCES.Where(t => t.WIID == wiid && t.DDID == ddid && t.DOCBH == docbh);
            //大于0时表示数据库已经有该文书编号
            if (result.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
