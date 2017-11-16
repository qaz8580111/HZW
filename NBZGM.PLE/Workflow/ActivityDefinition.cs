using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.WorkflowLib
{
    public class ActivityDefinition
    {
        #region 内部成员变量

        private WorkflowDefinition workflowDefinition;

        #endregion

        #region 成员属性

        /// <summary>
        /// 标识
        /// </summary>
        public decimal ADID { get; set; }

        /// <summary>
        /// 工作流定义
        /// </summary>
        public WorkflowDefinition WorkflowDefinition
        {
            get
            {
                if (this.workflowDefinition != null)
                {
                    return this.workflowDefinition;
                }
                
                PLEEntities db = new PLEEntities();

                var result = db.ACITIVITYDEFINITIONS.SingleOrDefault(t => t.ADID == this.ADID);

                if (result == null)
                    return null;

                return result.WDID == null ?
                    null : WorkflowDefinition.Get(result.WDID.Value);
            }
            set
            {
                this.workflowDefinition = value;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string ADName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string ADDescription { get; set; }

        /// <summary>
        /// 超期时间(小时数)
        /// </summary>
        public decimal? TimeLimits { get; set; }

        #endregion

        #region 方法

        public static ActivityDefinition Get(decimal ADID)
        {
            PLEEntities db = new PLEEntities();

            var result = db.ACITIVITYDEFINITIONS.SingleOrDefault(t => t.ADID == ADID);

            if (result == null)
            {
                return null;
            }

            ActivityDefinition definition = new ActivityDefinition();
            definition.ADID = result.ADID;
            definition.ADName = result.ADNAME;
            definition.ADDescription = result.ADDESC;
            definition.TimeLimits = result.TIMELIMITS;

            return definition;
        }

        #endregion
    }
}
