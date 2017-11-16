using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CustomModels
{
    /// <summary>
    /// 所有任务
    /// </summary>
    public class AllTask
    {
        /// <summary>
        /// 流程定义标识
        /// </summary>
        public decimal WDID { get; set; }

        /// <summary>
        /// 流程实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 流程名称
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
        /// 流程活动定义名称
        /// </summary>
        public string ADName { get; set; }

        /// <summary>
        /// 流程活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 递交时间
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// 超期时间
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public decimal? WorkflowStatusid { get; set; }
    }
}
