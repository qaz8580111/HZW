using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ZFSJModels
{
    public class ZFSJPendingTask
    {

        /// <summary>
        /// 流程实例标识
        /// </summary>
        public string WIID { get; set; }

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
        /// 事件编号
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// 事件来源
        /// </summary>
        public string EventSource { get; set; }

        /// <summary>
        /// 事件标题
        /// </summary>
        public string EventTitle { get; set; }
        /// <summary>
        /// 活动定义名称
        /// </summary>
        public string ADName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 超期时间
        /// </summary>
        public decimal? TimeLimit { get; set; }

        /// <summary>
        /// 时间超期时间
        /// </summary>
        public DateTime? SJTimeLimit { get; set; }
        /// <summary>
        /// 活动的流程内容
        /// </summary>
        public string WDATA { get; set; }
    }
}
