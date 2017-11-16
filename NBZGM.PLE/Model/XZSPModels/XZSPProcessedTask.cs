using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPModels
{
    public class XZSPProcessedTask
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
        /// 审批项目标识
        /// </summary>
        public decimal APID { get; set; }

        /// <summary>
        /// 活动定义标识
        /// </summary>
        public decimal ADID { get; set; }

        /// <summary>
        /// 当前活动定义标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 流程定义名称
        /// </summary>
        public string WDName { get; set; }

        /// <summary>
        /// 审批项目名称
        /// </summary>
        public string APName { get; set; }

        /// <summary>
        /// 活动定义名称
        /// </summary>
        public string ADName { get; set; }
    }
}
