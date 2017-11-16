using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.Model.WebServiceModels;
using Taizhou.PLE.WorkflowLib;

namespace WebService.BLL
{
    public partial class PhoneCaseWorkflow : WorkflowBase
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
        /// 创建流程
        /// </summary>
        /// <param name="UserID">用户</param>
        public PhoneCaseWorkflow(decimal UserID)
        {

            USER user = Taizhou.PLE.BLL.UserBLLs.UserBLL.GetUserByUserID(UserID);

            this.Workflow = new Workflow();
            this.Workflow.WIID = Guid.NewGuid().ToString("N");
            this.Workflow.WDID = WDID;
            this.Workflow.Definition = WorkflowDefinition.Get(WDID);
            this.Workflow.WICode = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.Workflow.WIName = "新发起的案件";
            this.Workflow.CreatedTime = DateTime.Now;
            this.Workflow.WorkflowStatus = WorkflowStatusEnum.Initiation;
            this.Workflow.UnitID = null;
            this.Workflow.CaseSourceID = null;
            this.Workflow.IllegalItemID = null;
            Workflow.Add(this.Workflow);

            //创建流程第一步活动:承办机构负责人审批立案建议
            Activity firstActivity = this.CreateActivity(null, ADID101, user != null ? user.USERID : 0);

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
                UnitName = user != null ? user.UNIT.UNITNAME : "",
                ProcessForms = new List<TotalForm>(),
                FinalForm = finalForm
            };
            this.Workflow.CommitChanges();
        }


        /// <summary>
        /// 根据流程实例标识初始化案件流程
        /// </summary>
        /// <param name="wiid">流程实例标识</param>
        public PhoneCaseWorkflow(string wiid)
        {
            this.Workflow = Workflow.Get(wiid);

            if (this.Workflow == null)
                return;

            foreach (Activity activity in this.Workflow.Activities.Values)
            {
                this.BindEvent(activity.Definition.ADID, activity);
            }
        }

        private void AD1_Submitted(object sender, EventArgs e)
        {
            Form101 form1 = this.CaseForm.FinalForm.Form101;
            Form102 form2 = this.CaseForm.FinalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = form1;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = form1.ProcessUser.UserID;
            activity.ProcessTime = form1.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(this.CaseForm.FinalForm);
            Activity next = this.CreateActivity(activity, ADID102, form1.CBLDID);

            this.CaseForm.FinalForm.Form102 = new Form102()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName,
            };
            this.CaseForm.UnitID = form1.CBDWID;
            this.Workflow.UnitID = form1.CBDWID;
            this.Workflow.CommitChanges();
        }

        protected override void BindEvent(decimal adid, Activity activity)
        {
            activity.Submitted += new EventHandler(AD1_Submitted);
        }

        /// <summary>
        /// 暂存表单
        /// </summary>
        /// <param name="phoneViewModel1">表单对象</param>
        public static void SavePhoneWorkflow(PhoneViewModel1 phoneViewModel1)
        {
            PhoneCaseWorkflow caseWorkflow = new PhoneCaseWorkflow(phoneViewModel1.WIID);
            CaseForm caseForm = caseWorkflow.CaseForm;
            caseWorkflow.Workflow.WIName = phoneViewModel1.AY;
            caseForm.WIName = phoneViewModel1.AY;
            Form101 form101 = caseForm.FinalForm.Form101;

            form101.WSBH = phoneViewModel1.WSBH;
            //当事人基本情况
            form101.DSRLX = phoneViewModel1.DSRLX;

            if (phoneViewModel1.OrgForm != null)
            {
                form101.OrgForm.FDDBRXM = phoneViewModel1.OrgForm.FDDBRXM;
                form101.OrgForm.MC = phoneViewModel1.OrgForm.MC;
                form101.OrgForm.ZW = phoneViewModel1.OrgForm.ZW;
                form101.OrgForm.ZZJGDMZBH = phoneViewModel1.OrgForm.ZZJGDMZBH;
            }

            if (phoneViewModel1.PersonForm != null)
            {
                Taizhou.PLE.Model.CaseWorkflowModels.PersonForm PersonForm = new Taizhou.PLE.Model.CaseWorkflowModels.PersonForm();
                PersonForm.CSNY = phoneViewModel1.PersonForm.CSNY;
                PersonForm.GZDW = phoneViewModel1.PersonForm.GZDW;
                PersonForm.MZ = phoneViewModel1.PersonForm.MZ;
                PersonForm.SFZH = phoneViewModel1.PersonForm.SFZH;
                PersonForm.XB = phoneViewModel1.PersonForm.XB;
                form101.PersonForm = PersonForm;
            }


            form101.ZSD = phoneViewModel1.ZSD;
            form101.LXDH = phoneViewModel1.LXDH;

            form101.AJLYID = 1;
            form101.AJLYName = "巡查发现";

            if (phoneViewModel1.IllegalForm != null)
            {
                ILLEGALITEM illegalform = GetIllegalInfomation(phoneViewModel1.IllegalForm.ID.ToString());

                if (illegalform != null)
                {
                    form101.IllegalForm.ID = illegalform.ILLEGALITEMID;
                    form101.IllegalForm.Code = illegalform.ILLEGALCODE;
                    form101.IllegalForm.WeiZe = illegalform.WEIZE;
                    form101.IllegalForm.FaZe = illegalform.FZZE;
                    form101.IllegalForm.ChuFa = illegalform.PENALTYCONTENT;
                }
            }

            form101.AY = phoneViewModel1.AY;
            form101.FADD = phoneViewModel1.FADD;
            form101.FASJ = phoneViewModel1.FASJ;
            form101.SFLA = phoneViewModel1.SFLA;

            caseWorkflow.Workflow.CommitChanges();
        }

        /// <summary>
        /// 查询违法行为
        /// </summary>
        /// <returns>违法行为标识</returns>
        public static ILLEGALITEM GetIllegalInfomation(string strIllegalItemID)
        {
            decimal illegalItemID = 0.0M;
            if (!decimal.TryParse(strIllegalItemID, out illegalItemID))
            {
                return null;
            }

            ILLEGALITEM illegalItem = IllegalItemBLL
                .GetIllegalItemByIllegalItemID(illegalItemID);
            return illegalItem;
        }


    }
}