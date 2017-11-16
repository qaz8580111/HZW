using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CustomModels
{
    /// <summary>
    /// 已处理任务
    /// </summary>
    public class ProcessedTask
    {
        /// <summary>
        /// 流程定义标识
        /// </summary>
        public decimal WDID { get; set; }

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string WDName { get; set; }

        /// <summary>
        /// 流程实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 流程实例名称
        /// </summary>
        public string WIName { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        public string WICode { get; set; }

        /// <summary>
        /// 流程活动定义标识
        /// </summary>
        public decimal ADID { get; set; }

        /// <summary>
        /// 流程活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 递交时间
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }

        /// <summary>
        /// 当前环节名称
        /// </summary>
        public string ADName { get; set; }

        /// <summary>
        /// 当前处理人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 流程状态标示
        /// </summary>
        public decimal WorkflowStausID { get; set; }
    }
}
