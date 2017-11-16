using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 法制处审批立案建议
    /// </summary>
    public class Form103 : BaseForm
    {
        /// <summary>
        /// 法制机构意见
        /// </summary>
        public string FZJGYJ { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        public bool? Approved { get; set; }
        
        /// <summary>
        /// 法制机构审批时间
        /// </summary>
        public string FZJGRQ { get; set; }

        /// <summary>
        /// 法制机构审批意见
        /// </summary>
        public bool? FZCSFTY { get; set; }

        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }
    }
}