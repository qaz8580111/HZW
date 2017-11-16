using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.RelevantItemViewModels
{
    public class ViewModel203
    {
        /// <summary>
        /// 所属案件工作流实例标识
        /// </summary>
        public string ParentWIID { get; set; }

        /// <summary>
        /// 所属案件活动实例标识
        /// </summary>
        public string ParentAIID { get; set; }

        /// <summary>
        /// 案件编号
        /// </summary>
        public string AJBH { get; set; }

        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 工作流活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 行政机关负责人审批意见
        /// </summary>
        public string XZJGFZRSPYJ { get; set; }
    }
}