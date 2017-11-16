using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 行政处罚告知书
    /// </summary>
    public class ViewModel13
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
        /// 备注说明
        /// </summary>
        public string BZSM { get; set; }
    }
}