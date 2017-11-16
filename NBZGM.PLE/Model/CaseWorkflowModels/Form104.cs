using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 分管副局长审批立案建议
    /// </summary>
    public class Form104 : BaseForm
    {
        /// <summary>
        /// 分管副局长意见
        /// </summary>
        public string FGFJZYJ { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        public bool Approved { get; set; }
    }
}