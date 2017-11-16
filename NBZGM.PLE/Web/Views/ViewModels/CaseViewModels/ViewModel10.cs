using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 法制处审批
    /// </summary>
    public class ViewModel10
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
        /// 听证时间
        /// </summary>
        public DateTime TZSJ { get; set; }

        /// <summary>
        /// 听证结果标识
        /// </summary>
        public decimal TZJGID { get; set; }

        /// <summary>
        /// 听证结果名称
        /// </summary>
        public string TZJGName { get; set; }

        /// <summary>
        /// 法制处审批意见
        /// </summary>
        public string FZCYJ { get; set; }

        /// <summary>
        /// 法制处是否同意
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }
    }
}