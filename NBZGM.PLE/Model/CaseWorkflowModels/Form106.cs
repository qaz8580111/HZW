using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 协办队员确认调查取证
    /// </summary>
    public class Form106 : BaseForm
    {
        /// <summary>
        /// 是否同意
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 确认意见
        /// </summary>
        public string QRYJ { get; set; }
    }
}
