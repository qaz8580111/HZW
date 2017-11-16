using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model.XZSPWorkflowModels.GGSP;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.XZSPBLLs;
using System.Web.Script.Serialization;

namespace Taizhou.PLE.Web.Process.XZSPProcess
{
    public class GGSPProcess
    {
        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="from101">登记表单</param>
        public static void Registration(Form101 from101)
        {
            decimal ggsp = WorkflowDefinitionEnum.GGSP.GetHashCode();

            XZSPACTDEF ActivityDefinition = ActivityDefinitionBLL.Query()
                .Where(t => t.WDID == ggsp
                  && t.SEQNO == 1).First();

            string WIID = CreatedWorkflowInstance(from101.ProcessTime.Value);
            string AIID = CreatedActivityInstance(WIID,
                ActivityDefinition.ADID, "", "", "", "", 0,
                from101.ProcessTime.Value);

            from101.ID = AIID;
            from101.ADID = ActivityDefinition.ADID;
            from101.ADName = ActivityDefinition.ADNAME;

            UpdateWorkflowInstanceCurrentAIID(WIID, AIID);

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = AIID;
            baseFrom.ADID = from101.ADID;
            baseFrom.ADName = from101.ADName;
            baseFrom.ProcessUserID = from101.ProcessUserID;
            baseFrom.ProcessUserName = from101.ProcessUserName;
            baseFrom.ProcessTime = from101.ProcessTime;
            totalFrom.Form101 = from101;
            totalFrom.CurrentForm = baseFrom;

            List<TotalForm> totalFromList = new List<TotalForm>();
            totalFromList.Add(totalFrom);

            GGSPForm ggspFrom = new GGSPForm()
            {
                WIID = WIID,
                WIName = "",
                WICode = "",
                //UnitID="",
                UnitName = "",
                WDID = ActivityDefinition.WDID.Value,
                ProcessForms = totalFromList,
                FinalForm = totalFrom,
                CreatedTime = from101.ProcessTime.Value
            };

            Approved(WIID, AIID, ggspFrom, "", "", "");

        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="wiid">流程实例标识</param>
        /// <param name="aiid">活动实例标识</param>
        /// <param name="totalFrom">全部表单</param>
        /// <param name="toDeptID">要流转到的部门标识</param>
        /// <param name="toPositionID">要流转到的职位标识</param>
        /// <param name="toUserID">要流转到的用户标识</param>
        public static void Approved(string wiid, string aiid,
            GGSPForm ggspFrom, string toDeptID,
            string toPositionID, string toUserID)
        {
            XZSPACTIST activityInstance = CompleteActivityInstance(aiid, ggspFrom);
            XZSPWFIST workflowInstance = UpdateWorkflowInstance(wiid, ggspFrom);

            decimal nextADID = 0;

            if (activityInstance.XZSPACTDEF.NEXTADID != null)
            {
                nextADID = activityInstance.XZSPACTDEF.NEXTADID.Value;
            }

            if (nextADID == 0)
            {
                //已经是最后一步
                decimal status = StatusEnum.Complete.GetHashCode();
                WorkflowInstanceBLL
                    .UpdateStatus(wiid, status);
            }
            else
            {
                XZSPACTDEF activityDefinition = ActivityDefinitionBLL
                    .Single(nextADID);
                string deptID = "";
                string positionID = activityDefinition.DEFAULTPOSITIONID;
                string userID = "";

                if (string.IsNullOrEmpty(toDeptID))
                {
                    deptID = toDeptID;
                }

                if (string.IsNullOrEmpty(toPositionID))
                {
                    positionID = toPositionID;
                }

                if (string.IsNullOrEmpty(toUserID))
                {
                    userID = toUserID;
                }

                string CurrentAIID = CreatedActivityInstance(wiid,
                    nextADID, activityInstance.AIID, deptID, positionID,
                    userID, 0, DateTime.Now);

                UpdateWorkflowInstanceCurrentAIID(wiid, CurrentAIID);
            }

        }

        public static string CreatedWorkflowInstance(DateTime dt)
        {
            XZSPWFIST workflowInstance = new XZSPWFIST()
            {
                WDID = WorkflowDefinitionEnum.GGSP.GetHashCode(),
                STATUSID = StatusEnum.Locked.GetHashCode(),
                CREATEDTIME = dt
            };

            return WorkflowInstanceBLL.Add(workflowInstance);
        }

        public static string CreatedActivityInstance(string wiid, decimal adid,
            string previonsAIID, string toDeptID, string toPositionID,
            string toUserID, decimal timeLimit, DateTime dt)
        {
            XZSPACTIST activityInstance = new XZSPACTIST()
            {
                ADID = adid,
                WIID = wiid,
                STATUSID = StatusEnum.Active.GetHashCode(),
                PREVIONSAIID = previonsAIID,
                TODEPTID = toDeptID,
                TOPOSITIONID = toPositionID,
                TOUSERID = toUserID,
                TIMELIMIT = timeLimit,
                CREATEDTIME = dt,
            };

            return ActivityInstanceBLL.Add(activityInstance);
        }

        public static XZSPACTIST CompleteActivityInstance(string aiid,
            GGSPForm ggspFrom)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string data = serializer.Serialize(ggspFrom);
            decimal status = StatusEnum.Complete.GetHashCode();

            return ActivityInstanceBLL.Update(aiid, status, data);
        }

        public static XZSPWFIST UpdateWorkflowInstance(string wiid,
            GGSPForm ggspFrom)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string data = serializer.Serialize(ggspFrom);
            decimal status = StatusEnum.Locked.GetHashCode();

            return WorkflowInstanceBLL.Update(wiid, status, data);
        }

        public static void UpdateWorkflowInstanceCurrentAIID(string wiid,
            string currentAIID)
        {
            decimal status = StatusEnum.Active.GetHashCode();

            WorkflowInstanceBLL.UpdateAIID(wiid, currentAIID);
        }

        public static GGSPForm GetYSFWFormByWIID(string WIID)
        {
            XZSPWFIST WorkflowInstance = WorkflowInstanceBLL.Single(WIID);
            string str = WorkflowInstance.WDATA;

            JavaScriptSerializer ser = new JavaScriptSerializer();
            GGSPForm ggspFrom = ser.Deserialize<GGSPForm>(str);

            return ggspFrom;
        }
    }
}
