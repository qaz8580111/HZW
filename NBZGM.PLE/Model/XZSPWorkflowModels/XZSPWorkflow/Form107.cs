using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow
{
    /// <summary>
    /// 执法大队审核申请
    /// </summary>
    public class Form107:BaseForm
    {
        /// <summary>
        /// 审核意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 文书材料
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}
