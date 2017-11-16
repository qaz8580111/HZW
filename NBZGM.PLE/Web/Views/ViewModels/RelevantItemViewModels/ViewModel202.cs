using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.RelevantItemViewModels
{
    public class ViewModel202
    {
        /// <summary>
        /// 所属案件工作流实例标识(父流程实例标识)
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
        /// 承办机构审核意见
        /// </summary>
        public string CBJGSHYJ { get; set; }

        /// <summary>
        /// 分管领导标识
        /// </summary>
        public decimal FGLDID { get; set; }

        /// <summary>
        /// 分管领导姓名
        /// </summary>
        public string FGLDName { get; set; }
    }
}