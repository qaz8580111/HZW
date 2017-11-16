using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow
{
    /// <summary>
    /// 承办人意见
    /// </summary>
    public class Form105 : BaseForm
    {
        /// <summary>
        /// 承办人审查意见
        /// </summary>
        public string SCYJ { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 要处理的综合科科长标识
        /// </summary>
        public string CBJGID { get; set; }

        /// <summary>
        /// 相关材料
        /// </summary>
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// 大队处理意见
        /// </summary>
        public string ZFDYYJ { get; set; }

    }
}
