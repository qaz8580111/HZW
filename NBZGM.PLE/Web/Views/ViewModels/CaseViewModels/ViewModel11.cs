using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.CaseViewModels
{
    /// <summary>
    /// 案件集体讨论笔录
    /// </summary>
    public class ViewModel11
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
        /// 讨论结果
        /// </summary>
        public string TLJG { get; set; }

    }
}