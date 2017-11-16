using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 主板队员一调查取证
    /// </summary>
    public class ViewModel15
    {
        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 工作流活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ZXSJ { get; set; }

        /// <summary>
        /// 执行结果标识
        /// </summary>
        public decimal ZXJGID { get; set; }

        /// <summary>
        /// 执行结果名称
        /// </summary>
        public string ZXJGName { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FKNR { get; set; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public string FKSJ { get; set; }

        /// <summary>
        /// 当事人意见
        /// </summary>
        public string DSRYJ { get; set; }
    }
}