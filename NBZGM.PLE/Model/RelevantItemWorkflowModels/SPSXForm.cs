using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.RelevantItemWorkflowModels
{
    /// <summary>
    /// 审批事项实体
    /// </summary>
    public class SPSXForm
    {
        /// <summary>
        /// 审批事项文书标识
        /// </summary>
        public string SPSXDIID { get; set; }

        /// <summary>
        /// 审批事项文书名称
        /// </summary>
        public string SPSXDIName { get; set; }

        /// <summary>
        /// 审批事项文书路径
        /// </summary>
        public string SPSXDISrc { get; set; }
    }
}
