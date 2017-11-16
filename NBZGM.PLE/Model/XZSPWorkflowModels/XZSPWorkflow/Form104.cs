using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow
{
    /// <summary>
    /// 执法中队审核核查
    /// </summary>
    public class Form104 : BaseForm
    {
        /// <summary>
        /// 审核意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 相关材料
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}
