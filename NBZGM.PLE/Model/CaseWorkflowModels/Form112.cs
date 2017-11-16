using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 分管副局长审核处理意见
    /// </summary>
    public class Form112 : BaseForm
    {
        /// <summary>
        /// 是否同意
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 分管副局长意见
        /// </summary>
        public string FGFJZYJ { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime CLSJ { get; set; }
    }
}
