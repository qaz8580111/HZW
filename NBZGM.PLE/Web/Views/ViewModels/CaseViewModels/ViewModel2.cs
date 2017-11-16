using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 承办机构负责人审批立案建议
    /// </summary>
    public class ViewModel2
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
        /// 主办队员
        /// </summary>
        public string ZBDY { get; set; }

        /// <summary>
        /// 协办队员
        /// </summary>
        public string XBDY { get; set; }

        /// <summary>
        /// 法制处审核人
        /// </summary>
        public string FZCSHR { get; set; }

        /// <summary>
        /// 法制处审核人编号
        /// </summary>
        public decimal FZCSHRBH { get; set; }

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