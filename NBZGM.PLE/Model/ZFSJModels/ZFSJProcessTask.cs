using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ZFSJModels
{
    public class ZFSJProcessTask
    {
        /// <summary>
        /// 流程实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 流程状态标识
        /// </summary>
        public decimal? WorkFlowStatusID { get; set; }

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
        /// 事件来源
        /// </summary>
        public string EventSource { get; set; }

        /// <summary>
        /// 活动定义名称
        /// </summary>
        public string ADName { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        public string ToUserID { get; set; }

        /// <summary>
        /// 活动的流程内容
        /// </summary>
        public string WDATA { get; set; }

        /// <summary>
        /// 时间超期时间
        /// </summary>
        public DateTime? SJTimeLimit { get; set; }
    }
}
