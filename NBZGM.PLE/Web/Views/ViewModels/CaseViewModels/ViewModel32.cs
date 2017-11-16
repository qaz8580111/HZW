using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Model.CaseWorkflowModels;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 符合听证条件，当事人提出听证申请
    /// </summary>
    public class ViewModel32
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