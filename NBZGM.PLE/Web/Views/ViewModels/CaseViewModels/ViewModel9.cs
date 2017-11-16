using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 承办单位审批意见
    /// </summary>
    public class ViewModel9
    {
        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 工作流活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 陈述申辩时间
        /// </summary>
        public DateTime CSSBSJ { get; set; }

        /// <summary>
        /// 陈述申辩结果标识
        /// </summary>
        public decimal CSSBJGID { get; set; }

        /// <summary>
        /// 陈述申辩结果名称
        /// </summary>
        public string CSSBJGName { get; set; }

        /// <summary>
        /// 承办单位是否同意
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 办案单位审批意见
        /// </summary>
        public string BADWYJ { get; set; }

        /// <summary>
        /// 承办意见
        /// </summary>
        public string CBYJ { get; set; }

        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }
    }
}