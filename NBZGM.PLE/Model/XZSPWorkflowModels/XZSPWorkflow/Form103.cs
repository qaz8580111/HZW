using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow
{
    /// <summary>
    /// 执法队员现场核查
    /// </summary>
    public class Form103:BaseForm
    {
        /// <summary>
        /// 核查意见
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 现场核查情况
        /// </summary>
        public string LocateCheckInfoForm103 { get; set; }

        /// <summary>
        /// 相关材料
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}
