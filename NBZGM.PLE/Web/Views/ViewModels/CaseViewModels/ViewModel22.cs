using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    public class ViewModel22
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
        /// 行政处罚决定书文号
        /// </summary>
        public string XZCFJDSWH { get; set; }

        /// <summary>
        /// 行政处罚内容
        /// </summary>
        public string XZCFNR { get; set; }

        /// <summary>
        /// 处罚执行方式及罚没财务的处置
        /// </summary>
        public string CFZXFSJFMCWDCZ { get; set; }

        /// <summary>
        /// 主办队员意见
        /// </summary>
        public string ZBDYYJ { get; set; }      
    }
}