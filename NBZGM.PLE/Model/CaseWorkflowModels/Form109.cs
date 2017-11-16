using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 办案单位审核处理意见
    /// </summary>
    public class Form109 : BaseForm
    {
        /// <summary>
        /// 办案单位意见
        /// </summary>
        public string BADWYJ { get; set; }

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
