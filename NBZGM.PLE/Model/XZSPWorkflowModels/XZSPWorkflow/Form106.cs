using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow
{
    /// <summary>
    /// 承办机构审核申请
    /// </summary>
    public class Form106 : BaseForm
    {
        /// <summary>
        /// 审核意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 要处理的副大队长标识
        /// </summary>
        public string FDZZID { get; set; }
    }
}
