using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 主办队员提出处理意见
    /// </summary>
    public class Form107 : BaseForm
    {
        /// <summary>
        /// 认定的违法事实
        /// </summary>
        public string RDDWFSS { get; set; }

        /// <summary>
        /// 证据
        /// </summary>
        public string ZJ { get; set; }

        /// <summary>
        /// 违反的法律、法规和规章
        /// </summary>
        public string WFDFLFGHGZ { get; set; }

        /// <summary>
        /// 处罚依据
        /// </summary>
        public string CFYJ { get; set; }

        /// <summary>
        /// 处理方式（行政处罚、撤案、不予处罚、移送）
        /// </summary>
        public string CLFS { get; set; }

        /// <summary>
        /// 处罚金额
        /// </summary>
        public decimal CFJE { get; set; }

        /// <summary>
        /// 调查终结后承办人意见
        /// </summary>
        public string DCZJHCBRYJ { get; set; }

        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }
    }
}
