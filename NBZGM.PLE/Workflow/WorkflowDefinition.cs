using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.WorkflowLib
{
    public class WorkflowDefinition
    {
        #region 属性

        /// <summary>
        /// 工作流定义标识
        /// </summary>
        public decimal WDID { get; set; }

        /// <summary>
        /// 工作流定义名称
        /// </summary>
        public string WDName { get; set; }

        /// <summary>
        /// 工作流定义描述
        /// </summary>
        public string WDDescription { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 根据工作流定义标识获取工作流定义对象
        /// </summary>
        /// <param name="WDID">工作流定义标识</param>
        /// <returns>工作库定义对象</returns>
        public static WorkflowDefinition Get(decimal WDID)
        {
            PLEEntities db = new PLEEntities();
            var result = db.WORKFLOWDEFINITIONS.SingleOrDefault(t => t.WDID == WDID);

            if (result == null)
            {
                return null;
            }

            WorkflowDefinition definition = new WorkflowDefinition();

            definition.WDID = result.WDID;
            definition.WDName = result.WDNAME;
            definition.WDDescription = result.WDDESC;

            return definition;
        }

        #endregion
    }
}
