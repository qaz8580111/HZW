using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 协办队员调查取证确认
    /// </summary>
    public class ViewModel6
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
        /// 是否同意
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 确认意见
        /// </summary>
        public string QRYJ { get; set; }
    }
}