using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model.RelevantItemWorkflowModels;
using Taizhou.PLE.WorkflowLib;

namespace Web.Workflows
{
    public partial class RelevantItemWorkflow : WorkflowBase
    {
        public const int WDID = 2; // 执法案件相关事项内部审批流程

        #region 定义活动定义常量

        /// <summary>
        /// 201队员提出申请
        /// </summary>
        public const int ADID201 = 201;

        /// <summary>
        /// 202承办单位审批申请
        /// </summary>
        public const int ADID202 = 202;

        /// <summary>
        /// 203行政机构负责人审批申请
        /// </summary>
        public const int ADID203 = 203;

        #endregion

        /// <summary>
        /// 所属一般案件流程(父流程)
        /// </summary>
        public CaseWorkflow ParentWorkflow { get; set; }

        /// <summary>
        /// 相关事项审批数据
        /// </summary>
        public RelevantItemForm RelevantItemForm
        {
            get
            {
                if (!this.Workflow.Properties.ContainsKey("RelevantItemForm"))
                    return null;

                return (RelevantItemForm)this.Workflow.Properties["RelevantItemForm"];
            }
            set
            {
                this.Workflow.Properties["RelevantItemForm"] = value;
            }
        }

        /// <summary>
        /// 新建相关事项审批流程
        /// </summary>
        public RelevantItemWorkflow(string parentWIID)
        {
            this.ParentWorkflow = new CaseWorkflow(parentWIID);

            this.Workflow = new Workflow();
            this.Workflow.WIID = Guid.NewGuid().ToString("N");
            this.Workflow.WDID = WDID;
            this.Workflow.ParentWIID = parentWIID;
            this.Workflow.Definition = WorkflowDefinition.Get(WDID);
            this.Workflow.WICode = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.Workflow.WIName = "未命名相关事项审批";
            this.Workflow.CreatedTime = DateTime.Now;
            this.Workflow.WorkflowStatus = WorkflowStatusEnum.Initiation;
            this.Workflow.UnitID = SessionManager.User.UnitID;

            this.Workflow.CaseSourceID = null;
            this.Workflow.IllegalItemID = null;
            Workflow.Add(this.Workflow);

            //创建流程第一步活动:队员提出审批申请
            Activity firstActivity = this.CreateActivity(null, ADID201, SessionManager.User.UserID);

            RelevantItemForm1 form1 = new RelevantItemForm1()
            {
                ID = firstActivity.AIID,
                ADID = firstActivity.Definition.ADID,
                ADName = firstActivity.Definition.ADName,
            };

            this.RelevantItemForm = new RelevantItemForm()
            {
                ParentWIID = parentWIID,
                WIID = this.Workflow.WIID,
                WICode = this.Workflow.WICode,
                WIName = this.Workflow.WIName,
                RelevantItemForm1 = form1,
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 根据流程实例标识初始化流程
        /// </summary>
        /// <param name="wiid">流程实例标识</param>
        public RelevantItemWorkflow(string parentWIID, string wiid)
        {
            this.ParentWorkflow = new CaseWorkflow(parentWIID);

            this.Workflow = Workflow.Get(wiid);

            if (this.Workflow != null)
            {
                foreach (Activity activity in this.Workflow.Activities.Values)
                {
                    this.BindEvent(activity.Definition.ADID, activity);
                }
            }
        }

        protected override void BindEvent(decimal adid, Activity activity)
        {
            if (adid == ADID201)
            {
                activity.Submitted += new EventHandler(AD201_Submitted);
            }
            else if (adid == ADID202)
            {
                activity.Submitted += new EventHandler(AD202_Submitted);
            }
            else if (adid == ADID203)
            {
                activity.Submitted += new EventHandler(AD203_Submitted);
            }
        }
    }
}