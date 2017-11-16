using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 办案单位审批立案建议
    /// </summary>
    public class Form102 : BaseForm
    {
        /// <summary>
        /// 领导审批意见
        /// </summary>
        public string LDSPYJ { get; set; }

        /// <summary>
        /// 分管领导标识
        /// </summary>
        public decimal FGLDID { get; set; }

        /// <summary>
        /// 分管领导姓名
        /// </summary>
        public string FGLDName { get; set; }

        /// <summary>
        /// 法制处审核人 
        /// </summary>
        public User FZCSHR { get; set; }

        /// <summary>
        /// 主办队员
        /// </summary>
        public User ZBDY { get; set; }

        /// <summary>
        /// 协办队员
        /// </summary>
        public User XBDY { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        public bool? Approved { get; set; }

        /// <summary>
        /// 领导审批日期
        /// </summary>
        public DateTime? LDSPRQ { get; set; }

        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }
    }
}