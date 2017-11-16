using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.WorkflowLib;

namespace Taizhou.PLE.Model.WebServiceModels
{
    public class PhoneWorkflow
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

        #endregion

        #region 构造方法

        public PhoneWorkflow()
        {
            this.Properties = new PropertyDictionary<string, object>();
        }
        #endregion

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
    }
}
