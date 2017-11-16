using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;


namespace Web.Workflows
{
    public partial class CaseWorkflow : WorkflowBase
    {
        public const int WDID = 1; // 执法案件管理系统流程

        #region 定义活动定义常量

        /// <summary>
        /// 101 执法队员提出立案申请
        /// </summary>
        public const int ADID101 = 101;

        /// <summary>
        /// 102 办案单位审核立案申请
        /// </summary>
        public const int ADID102 = 102;

        /// <summary>
        /// 103 法制处审核立案申请
        /// </summary>
        public const int ADID103 = 103;

        /// <summary>
        /// 104 分管副局长审核立案申请
        /// </summary>
        public const int ADID104 = 104;

        /// <summary>
        /// 主办队员调查取证
        /// </summary>
        public const int ADID105 = 105;

        /// <summary>
        /// 协办队员确认调查取证
        /// </summary>
        public const int ADID106 = 106;

        /// <summary>
        /// 主办队员提出处理意见
        /// </summary>
        public const int ADID107 = 107;

        /// <summary>
        /// 协办队员确认处理意见
        /// </summary>
        public const int ADID108 = 108;

        /// <summary>
        /// 办案单位审核处理意见
        /// </summary>
        public const int ADID109 = 109;

        /// <summary>
        /// 法制处审核处理意见
        /// </summary>
        public const int ADID110 = 110;

        /// <summary>
        /// 集体讨论
        /// </summary>
        public const int ADID111 = 111;

        /// <summary>
        /// 分管副局长审核处理意见
        /// </summary>
        public const int ADID112 = 112;

        /// <summary>
        /// 送达通知(不予行政处罚的情况)
        /// </summary>
        public const int ADID113 = 113;

        /// <summary>
        /// 行政处罚事先告知
        /// </summary>
        public const int ADID114 = 114;

        /// <summary>
        /// 当事人意见反馈
        /// </summary>
        public const int ADID115 = 115;

        /// <summary>
        /// 主办队员作出处罚决定
        /// </summary>
        public const int ADID116 = 116;

        /// <summary>
        /// 协办队员确认处罚决定
        /// </summary>
        public const int ADID117 = 117;

        /// <summary>
        /// 办案单位确认处理意见
        /// </summary>
        public const int ADID118 = 118;

        /// <summary>
        /// 法制处确认处理意见
        /// </summary>
        public const int ADID119 = 119;

        /// <summary>
        /// 分管副局长确认处理意见
        /// </summary>
        public const int ADID120 = 120;

        /// <summary>
        /// 主办队员送达处罚决定书
        /// </summary>
        public const int ADID121 = 121;

        /// <summary>
        /// 主办队员提出结案申请
        /// </summary>
        public const int ADID122 = 122;

        /// <summary>
        /// 协办队员确认结案申请
        /// </summary>
        public const int ADID123 = 123;

        /// <summary>
        /// 办案单位确认结案申请
        /// </summary>
        public const int ADID124 = 124;

        /// <summary>
        /// 法制处确认结案申请
        /// </summary>
        public const int ADID125 = 125;

        /// <summary>
        /// 分管副局长确认结案意见
        /// </summary>
        public const int ADID126 = 126;

        /// <summary>
        /// 当事人提起行政复议或者行政诉讼
        /// </summary>
        public const int ADID127 = 127;

        /// <summary>
        /// 行政强制执行
        /// </summary>
        public const int ADID128 = 128;

        /// <summary>
        /// 申请法院强制执行
        /// </summary>
        public const int ADID129 = 129;

        /// <summary>
        /// 执行结果上报（执法队员1）
        /// </summary>
        public const int ADID130 = 130;

        /// <summary>
        /// 执行结果上报（执法队员1）
        /// </summary>
        public const int ADID131 = 131;

        /// <summary>
        /// 执行结果上报（执法队员1）
        /// </summary>
        public const int ADID132 = 132;

        /// <summary>
        /// 文书环节
        /// </summary>
        public const int ADID133 = 133;
        #endregion


        public CaseForm CaseForm
        {
            get
            {
                if (!this.Workflow.Properties.ContainsKey("CaseForm"))
                    return null;

                return (CaseForm)this.Workflow.Properties["CaseForm"];
            }
            set
            {
                this.Workflow.Properties["CaseForm"] = value;
            }
        }

        /// <summary>
        /// 新建案件流程
        /// </summary>
        public CaseWorkflow()
        {
            this.Workflow = new Workflow();
            this.Workflow.WIID = Guid.NewGuid().ToString("N");
            this.Workflow.WDID = WDID;
            this.Workflow.Definition = WorkflowDefinition.Get(WDID);
            this.Workflow.WICode = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.Workflow.WIName = "新发起的案件";
            this.Workflow.CreatedTime = DateTime.Now;
            this.Workflow.WorkflowStatus = WorkflowStatusEnum.Initiation;
            this.Workflow.UnitID = SessionManager.User.UnitID;
            this.Workflow.UserID = SessionManager.User.UserID;
            this.Workflow.CaseSourceID = null;
            this.Workflow.IllegalItemID = null;
            Workflow.Add(this.Workflow);

            //创建流程第一步活动:承办机构负责人审批立案建议
            Activity firstActivity = this.CreateActivity(null, ADID101, SessionManager.User.UserID);

            Form101 form101 = new Form101()
            {
                ID = firstActivity.AIID,
                ADID = firstActivity.Definition.ADID,
                ADName = firstActivity.Definition.ADName,
            };

            TotalForm finalForm = new TotalForm
            {
                CurrentForm = form101,
                Form101 = form101
            };

            this.CaseForm = new CaseForm()
            {
                WIID = this.Workflow.WIID,
                WDID = this.Workflow.WDID,
                WICode = this.Workflow.WICode,
                WIName = this.Workflow.WIName,
                CreatedTime = this.Workflow.CreatedTime,
                UnitID = this.Workflow.UnitID,
                UnitName = SessionManager.User.UnitName,
                ProcessForms = new List<TotalForm>(),
                FinalForm = finalForm
            };
            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 根据流程实例标识初始化案件流程
        /// </summary>
        /// <param name="wiid">流程实例标识</param>
        public CaseWorkflow(string wiid)
        {
            this.Workflow = Workflow.Get(wiid);

            if (this.Workflow == null)
                return;

            foreach (Activity activity in this.Workflow.Activities.Values)
            {
                this.BindEvent(activity.Definition.ADID, activity);
            }
        }

        /// <summary>
        /// 控制页面跳转至当前要处理的流程环节页面
        /// </summary>
        /// <param name="activity">当前要处理的流程环节</param>
        private void Redirect(Activity activity)
        {
            string page = null;

            switch ((int)activity.Definition.ADID)
            {
                case ADID101:
                    page = "/Workflow101/Index";
                    break;
                case ADID102:
                    page = "/Workflow102/Index";
                    break;
                case ADID103:
                    page = "/Workflow103/Index";
                    break;
                case ADID104:
                    page = "/Workflow104/Index";
                    break;
                default:
                    throw new Exception("流程活动未定义.");
            }

            string redirectUrl = string.Format("{0}?WIID={1}&AIID={2}",
                page, this.Workflow.WIID, activity.AIID.ToString());

            HttpContext.Current.Response.Redirect(redirectUrl, true);
        }

        /// <summary>
        /// 绑定流程活动每个环节的相关事件
        /// </summary>
        /// <param name="adid">流程活动定义标识</param>
        /// <param name="activity">流程活动</param>
        protected override void BindEvent(decimal adid, Activity activity)
        {
            if (adid == ADID101)
            {
                activity.Submitted += new EventHandler(AD1_Submitted);
            }
            else if (adid == ADID102)
            {
                activity.Submitted += new EventHandler(AD2_Submitted);
            }
            else if (adid == ADID103)
            {
                activity.Submitted += new EventHandler(AD3_Submitted);
            }
            else if (adid == ADID104)
            {
                activity.Submitted += new EventHandler(AD4_Submitted);
            }
            else if (adid == ADID105)
            {
                activity.Submitted += new EventHandler(AD5_Submitted);
            }
            else if (adid == ADID106)
            {
                activity.Submitted += new EventHandler(AD6_Submitted);
            }
            else if (adid == ADID107)
            {
                activity.Submitted += new EventHandler(AD7_Submitted);
            }
            else if (adid == ADID108)
            {
                activity.Submitted += new EventHandler(AD8_Submitted);
            }
            else if (adid == ADID109)
            {
                activity.Submitted += new EventHandler(AD9_Submitted);
            }
            else if (adid == ADID110)
            {
                activity.Submitted += new EventHandler(AD10_Submitted);
            }
            else if (adid == ADID111)
            {
                activity.Submitted += new EventHandler(AD11_Submitted);
            }

            else if (adid == ADID112)
            {
                activity.Submitted += new EventHandler(AD12_Submitted);
            }
            else if (adid == ADID113)
            {
                activity.Submitted += new EventHandler(AD13_Submitted);
            }
            else if (adid == ADID114)
            {
                activity.Submitted += new EventHandler(AD14_Submitted);
            }
            else if (adid == ADID115)
            {
                activity.Submitted += new EventHandler(AD15_Submitted);
            }
            else if (adid == ADID116)
            {
                activity.Submitted += new EventHandler(AD16_Submitted);
            }
            else if (adid == ADID117)
            {
                activity.Submitted += new EventHandler(AD17_Submitted);
            }
            else if (adid == ADID118)
            {
                activity.Submitted += new EventHandler(AD18_Submitted);
            }
            else if (adid == ADID119)
            {
                activity.Submitted += new EventHandler(AD19_Submitted);
            }
            else if (adid == ADID120)
            {
                activity.Submitted += new EventHandler(AD20_Submitted);
            }
            else if (adid == ADID121)
            {
                activity.Submitted += new EventHandler(AD21_Submitted);
            }
            else if (adid == ADID122)
            {
                activity.Submitted += new EventHandler(AD22_Submitted);
            }
            else if (adid == ADID123)
            {
                activity.Submitted += new EventHandler(AD23_Submitted);
            }

            else if (adid == ADID124)
            {
                activity.Submitted += new EventHandler(AD24_Submitted);
            }
            else if (adid == ADID125)
            {
                activity.Submitted += new EventHandler(AD25_Submitted);
            }
            else if (adid == ADID126)
            {
                activity.Submitted += new EventHandler(AD26_Submitted);
            }
            else if (adid == ADID127)
            {
                activity.Submitted += new EventHandler(AD27_Submitted);
            }
            else if (adid == ADID128)
            {
                activity.Submitted += new EventHandler(AD28_Submitted);
            }
            else if (adid == ADID129)
            {
                activity.Submitted += new EventHandler(AD29_Submitted);
            }
            else if (adid == ADID130)
            {
                activity.Submitted += new EventHandler(AD30_Submitted);
            }
            else if (adid == ADID131)
            {
                activity.Submitted += new EventHandler(AD31_Submitted);
            }
            else if (adid == ADID132)
            {
                activity.Submitted += new EventHandler(AD32_Submitted);
            }
            else if (adid == ADID133)
            {
                activity.Submitted += new EventHandler(AD33_Submitted);
            }
        }

        /// <summary>
        /// 获取案件编号
        /// </summary>
        /// <returns></returns>
        public static string GetWorkflowCode()
        {
            PLEEntities db = new PLEEntities();

            DateTime now = DateTime.Now;

            DateTime time1 = new DateTime(now.Year, 1, 1);
            DateTime time2 = new DateTime(now.Year + 1, 1, 1);
            int count = db.WORKFLOWINSTANCES.Where(t =>
                t.CREATEDTIME.Value >= time1 && t.CREATEDTIME < time2).Count();

            return string.Format("台城执立〔 {0} 〕第 {1:D5} 号", time1.ToString("yyyy"), ++count);
        }
    }
}