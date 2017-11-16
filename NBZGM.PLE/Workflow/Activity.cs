using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.WorkflowLib
{
    public class Activity
    {
        #region 内部成员变量

        private ActivityDefinition definition;

        private Workflow workflow;

        private Activity previousActivity;

        #endregion

        #region 成员属性

        /// <summary>
        /// 活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 所属活动定义标识
        /// </summary>
        public decimal ADID { get; set; }

        /// <summary>
        /// 所属活动定义
        /// </summary>
        public ActivityDefinition Definition
        {
            get
            {
                if (this.definition == null && this.ADID != 0)
                    this.definition = ActivityDefinition.Get(this.ADID);

                return this.definition;
            }
            set
            {
                this.definition = value;
            }
        }

        /// <summary>
        /// 所属工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 所属工作流实例
        /// </summary>
        public Workflow Workflow
        {
            get
            {
                if (this.workflow == null && this.WIID != null)
                    this.workflow = Workflow.Get(this.WIID);

                return this.workflow;
            }
            set
            {
                this.workflow = value;
            }
        }

        /// <summary>
        /// 前一活动标识
        /// </summary>
        internal string PreviousAIID { get; set; }

        /// <summary>
        /// 前一活动
        /// </summary>
        public Activity PreviousActivity
        {
            get
            {
                if (this.previousActivity == null && this.PreviousAIID != null)
                {
                    this.previousActivity = this.Workflow.Activities[this.PreviousAIID];
                }

                return this.previousActivity;
            }
            set
            {
                this.previousActivity = value;
            }
        }

        public List<Activity> ChildActivities
        {
            get
            {
                List<Activity> activities = new List<Activity>();

                foreach (Activity activity in this.Workflow.Activities.Values)
                {
                    if (activity.PreviousAIID == this.AIID)
                    {
                        activities.Add(activity);
                    }
                }

                return activities;
            }
        }

        /// <summary>
        /// 指定处理人
        /// </summary>
        public decimal? AssignUserID { get; set; }

        /// <summary>
        /// 处理用户标识
        /// </summary>
        public decimal? ProcessUserID { get; set; }

        /// <summary>
        ///处理时间 
        /// </summary>
        public DateTime? ProcessTime { get; set; }

        /// <summary>
        /// 递交时间
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// 超期时间
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public ActivityStatusEnum ActivityStatus { get; set; }

        /// <summary>
        /// 属性字典
        /// </summary>
        public PropertyDictionary<string, object> Properties { get; private set; }

        #endregion

        #region 构造方法

        public Activity()
        {
            this.Properties = new PropertyDictionary<string, object>();
        }

        #endregion

        #region 成员方法

        public static Activity Get(string AIID)
        {
            PLEEntities db = new PLEEntities();

            var result = db.ACTIVITYINSTANCES.SingleOrDefault(t => t.AIID == AIID);

            if (result == null)
                return null;

            Activity activity = new Activity()
            {
                AIID = result.AIID,
                ADID = result.ADID.Value,
                WIID = result.WIID,
                PreviousAIID = result.PREVIOUSAIID,
                ActivityStatus = (ActivityStatusEnum)result.ACTIVITYSTATUSID,
                ProcessTime = result.PROCESSTIME,
                ProcessUserID = result.PROCESSUSERID.Value,
                AssignUserID = result.ASSIGNUSERID,
                DeliveryTime = result.DELIVERYTIME.Value,
                ExpirationTime = result.EXPIRATIONTIME
            };

            foreach (var propertyEntity in result.ACTIVITYPEROPERTIES)
            {
                activity.Properties[propertyEntity.KEY] =
                    Serializer.Deserialize(propertyEntity.ASSEMBLYNAME,
                    propertyEntity.TYPENAME, propertyEntity.VALUE.ToString());
            }

            return activity;
        }

        public void CommitChanges()
        {
            PLEEntities db = new PLEEntities();

            var aiResult = db.ACTIVITYINSTANCES
                .SingleOrDefault(t => t.AIID == this.AIID);

            if (aiResult == null)
            {
                return;
            }

            aiResult.ADID = this.Definition.ADID;
            aiResult.WIID = this.Workflow.WIID;
            aiResult.PREVIOUSAIID = this.PreviousActivity == null
                ? null : this.PreviousActivity.AIID;
            aiResult.ACTIVITYSTATUSID = (decimal)this.ActivityStatus;
            aiResult.PROCESSTIME = this.ProcessTime;
            aiResult.PROCESSUSERID = this.ProcessUserID;
            aiResult.ASSIGNUSERID = this.AssignUserID;
            aiResult.DELIVERYTIME = this.DeliveryTime;
            aiResult.EXPIRATIONTIME = this.ExpirationTime;

            var apResults = from t in db.ACTIVITYPEROPERTIES
                            where t.AIID == this.AIID
                            select t;

            foreach (var apResult in apResults)
            {
                db.ACTIVITYPEROPERTIES.Remove(apResult);
            }

            foreach (string key in this.Properties.Keys)
            {
                object value = this.Properties[key];
                Type type = value.GetType();

                ACTIVITYPEROPERTy property = new ACTIVITYPEROPERTy();

                property.APID = Guid.NewGuid().ToString("N");
                property.AIID = this.AIID;
                property.KEY = key;
                property.VALUE = Serializer.Serialize(value);
                property.ASSEMBLYNAME = type.Assembly.FullName;
                property.TYPENAME = type.FullName;

                db.ACTIVITYPEROPERTIES.Add(property);
            }

            db.SaveChanges();
        }

        #endregion

        #region 事件

        public event EventHandler Actived;
        public event EventHandler Submitted;

        public void Active()
        {
            if (this.Actived != null)
                this.Actived(this, null);
        }

        public void Submit()
        {
            if (this.Submitted != null)
                this.Submitted(this, null);
        }

        #endregion
    }
}
