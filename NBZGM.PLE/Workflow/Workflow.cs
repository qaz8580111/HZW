using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.WorkflowLib
{
    public class Workflow
    {

        #region 内部成员变量

        private WorkflowDefinition definition;

        private PropertyDictionary<string, Activity> activities;

        #endregion

        #region 成员属性

        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 父流程实例标识
        /// </summary>
        public string ParentWIID { get; set; }

        /// <summary>
        /// 工作流定义标识
        /// </summary>
        public decimal WDID { get; set; }

        /// <summary>
        /// 所属工作流定义
        /// </summary>
        public WorkflowDefinition Definition
        {
            get
            {
                if (this.definition == null && this.WDID != 0)
                {
                    this.definition = WorkflowDefinition.Get(this.WDID);
                }

                return this.definition;
            }
            set
            {
                this.definition = value;
            }
        }

        /// <summary>
        /// 工作流实例编号
        /// </summary>
        public string WICode { get; set; }

        /// <summary>
        /// 工作流实例名称
        /// </summary>
        public string WIName { get; set; }

        /// <summary>
        /// 流程所属单位标识
        /// </summary>
        public decimal? UnitID { get; set; }

        /// <summary>
        /// 流程所属用户标识
        /// </summary>
        public decimal? UserID { get; set; }

        /// <summary>
        /// 案件来源标识
        /// </summary>
        public decimal? CaseSourceID { get; set; }

        /// <summary>
        /// 违法行为事项标识
        /// </summary>
        public decimal? IllegalItemID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public WorkflowStatusEnum WorkflowStatus { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public PropertyDictionary<string, object> Properties { get; private set; }

        /// <summary>
        /// 活动
        /// </summary>
        public PropertyDictionary<string, Activity> Activities
        {
            get
            {
                if (this.activities != null)
                {
                    return this.activities;
                }

                PLEEntities db = new PLEEntities();

                var result = db.WORKFLOWINSTANCES
                    .SingleOrDefault(t => t.WIID == this.WIID);

                if (result == null)
                {
                    return null;
                }

                this.activities = new PropertyDictionary<string, Activity>();

                foreach (ACTIVITYINSTANCE entity in result.ACTIVITYINSTANCES)
                {
                    Activity activity = new Activity()
                    {
                        AIID = entity.AIID,
                        ADID = entity.ADID.Value,
                        WIID = this.WIID,
                        Workflow = this,
                        AssignUserID = entity.ASSIGNUSERID,
                        PreviousAIID = entity.PREVIOUSAIID,
                        ActivityStatus = (ActivityStatusEnum)entity.ACTIVITYSTATUSID,
                        ProcessTime = entity.PROCESSTIME,
                        DeliveryTime = entity.DELIVERYTIME.Value,
                        ExpirationTime = entity.EXPIRATIONTIME
                    };

                    foreach (var propertyEntity in entity.ACTIVITYPEROPERTIES)
                    {
                        activity.Properties[propertyEntity.KEY] =
                            Serializer.Deserialize(propertyEntity.ASSEMBLYNAME,
                            propertyEntity.TYPENAME, propertyEntity.VALUE.ToString());
                    }

                    this.activities.Add(activity.AIID, activity);
                }

                this.Activities.Adding +=
                    new PropertyEventHandler<string, Activity>(this.Activities_Adding);

                this.Activities.Removing +=
                    new PropertyEventHandler<string, Activity>(this.Activities_Removing);

                this.Activities.Updating +=
                    new PropertyEventHandler<string, Activity>(this.Activities_Updating);

                this.Activities.Clearing +=
                    new PropertyEventHandler<string, Activity>(this.Activities_Clearing);

                return this.activities;
            }
        }

        #endregion

        #region 构造方法

        public Workflow()
        {
            this.Properties = new PropertyDictionary<string, object>();
        }

        #endregion

        #region 方法

        private void Activities_Adding(object sender, PropertyEventArgs<string, Activity> e)
        {
            Activity activity = e.Value;

            activity.Workflow = this;

            PLEEntities db = new PLEEntities();

            var result = db.ACTIVITYINSTANCES.SingleOrDefault(t => t.AIID == activity.AIID);

            if (result != null)
                return;

            ACTIVITYINSTANCE ai = new ACTIVITYINSTANCE();

            ai.AIID = activity.AIID;
            ai.ADID = activity.Definition.ADID;
            ai.WIID = activity.Workflow.WIID;
            ai.PREVIOUSAIID = activity.PreviousAIID;
            ai.ASSIGNUSERID = activity.AssignUserID;
            ai.ACTIVITYSTATUSID = (int)activity.ActivityStatus;
            ai.PROCESSUSERID = activity.ProcessUserID;
            ai.PROCESSTIME = activity.ProcessTime;
            ai.DELIVERYTIME = activity.DeliveryTime;
            ai.EXPIRATIONTIME = activity.ExpirationTime;

            db.ACTIVITYINSTANCES.Add(ai);

            foreach (string key in activity.Properties.Keys)
            {
                object value = activity.Properties[key];
                Type type = value.GetType();

                ACTIVITYPEROPERTy ap = new ACTIVITYPEROPERTy();
                ap.APID = Guid.NewGuid().ToString("N");
                ap.AIID = activity.AIID;
                ap.KEY = key;
                ap.VALUE = Serializer.Serialize(value);
                ap.ASSEMBLYNAME = type.Assembly.FullName;
                ap.TYPENAME = type.FullName;

                db.ACTIVITYPEROPERTIES.Add(ap);
            }

            db.SaveChanges();
        }

        private void Activities_Removing(object sender, PropertyEventArgs<string, Activity> e)
        {

        }

        private void Activities_Updating(object sender, PropertyEventArgs<string, Activity> e)
        {

        }

        private void Activities_Clearing(object sender, PropertyEventArgs<string, Activity> e)
        {

        }

        public static Workflow Get(string WIID)
        {
            PLEEntities db = new PLEEntities();

            var result = db.WORKFLOWINSTANCES
                .SingleOrDefault(t => t.WIID == WIID);

            if (result == null)
                return null;

            Workflow workflow = new Workflow()
            {
                WIID = result.WIID,
                ParentWIID = result.PARENTWIID,
                WDID = result.WDID.Value,
                WICode = result.WICODE,
                WIName = result.WINAME,
                CreatedTime = result.CREATEDTIME.Value,
                UnitID = result.UNITID,
                WorkflowStatus = (WorkflowStatusEnum)result.WORKFLOWSTATUSID
            };

            foreach (var property in result.WORKFLOWPEROPERTIES)
            {
                workflow.Properties[property.KEY] =
                    Serializer.Deserialize(property.ASSEMBLYNAME, property.TYPENAME,
                    property.VALUE.ToString());
            }

            return workflow;
        }

        public static void Add(Workflow workflow)
        {
            if (workflow == null)
                return;

            PLEEntities db = new PLEEntities();

            var result = db.WORKFLOWINSTANCES
                .SingleOrDefault(t => t.WIID == workflow.WIID);

            if (result != null)
                return;

            WORKFLOWINSTANCE instance = new WORKFLOWINSTANCE
            {
                WIID = workflow.WIID,
                WDID = workflow.WDID,
                WICODE = workflow.WICode,
                WINAME = workflow.WIName,
                WORKFLOWSTATUSID = (decimal)workflow.WorkflowStatus,
                CREATEDTIME = workflow.CreatedTime,
                UNITID = workflow.UnitID,
                USERID = workflow.UserID,
                PARENTWIID = workflow.ParentWIID
            };

            db.WORKFLOWINSTANCES.Add(instance);

            foreach (string key in workflow.Properties.Keys)
            {
                object value = workflow.Properties[key];
                Type type = value.GetType();

                WORKFLOWPEROPERTy property = new WORKFLOWPEROPERTy();
                property.WPID = Guid.NewGuid().ToString("N");
                property.WIID = instance.WIID;
                property.KEY = key;
                property.VALUE = Serializer.Serialize(value);
                property.ASSEMBLYNAME = type.Assembly.FullName;
                property.TYPENAME = type.FullName;

                db.WORKFLOWPEROPERTIES.Add(property);
            }

            db.SaveChanges();
        }

        public static void Remove(string WIID)
        {
            PLEEntities db = new PLEEntities();

            WORKFLOWINSTANCE resultWI = db.WORKFLOWINSTANCES
                .SingleOrDefault(t => t.WIID == WIID);

            if (resultWI == null)
                return;

            var resultAIs = db.ACTIVITYINSTANCES.Where(t => t.WIID == WIID);

            foreach (var ai in resultAIs)
            {
                var resultAPs = db.ACTIVITYPEROPERTIES
                    .Where(t => t.AIID == ai.AIID);

                foreach (var ap in resultAPs)
                {
                    db.ACTIVITYPEROPERTIES.Remove(ap);
                }

                db.ACTIVITYINSTANCES.Remove(ai);
            }

            var resultWPs = db.WORKFLOWPEROPERTIES.Where(t => t.WIID == WIID);

            foreach (var wp in resultWPs)
            {
                db.WORKFLOWPEROPERTIES.Remove(wp);
            }

            db.WORKFLOWINSTANCES.Remove(resultWI);

            db.SaveChanges();
        }

        public void CommitChanges()
        {
            PLEEntities db = new PLEEntities();

            var wiResult = db.WORKFLOWINSTANCES
                .SingleOrDefault(t => t.WIID == this.WIID);

            wiResult.WIID = this.WIID;
            wiResult.WDID = this.WDID;
            wiResult.WICODE = this.WICode;
            wiResult.WINAME = this.WIName;
            wiResult.CREATEDTIME = this.CreatedTime;
            wiResult.WORKFLOWSTATUSID = (decimal)this.WorkflowStatus;
            wiResult.UNITID = this.UnitID;
            wiResult.PARENTWIID = this.ParentWIID;

            var wpResults = from t in db.WORKFLOWPEROPERTIES
                            where t.WIID == this.WIID
                            select t;

            foreach (var wp in wpResults)
            {
                db.WORKFLOWPEROPERTIES.Remove(wp);
            }

            foreach (string key in this.Properties.Keys)
            {
                object value = this.Properties[key];
                Type type = value.GetType();

                WORKFLOWPEROPERTy property = new WORKFLOWPEROPERTy();
                property.WPID = Guid.NewGuid().ToString("N");
                property.WIID = this.WIID;
                property.KEY = key;
                property.VALUE = Serializer.Serialize(value);
                property.ASSEMBLYNAME = type.Assembly.FullName;
                property.TYPENAME = type.FullName;

                db.WORKFLOWPEROPERTIES.Add(property);
            }

            db.SaveChanges();
        }

        #endregion
    }
}
