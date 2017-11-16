using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 法制处处理意见
    /// </summary>
    public class Form110 : BaseForm
    {
        /// <summary>
        /// 法制处意见
        /// </summary>
        public string FZCYJ { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }
    }
}
