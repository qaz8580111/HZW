using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CustomModels
{
    public class CustomRelevantItem
    {
        /// <summary>
        /// 一般案件流程标识
        /// </summary>
        public string PARENTWIID { get; set; }

        /// <summary>
        /// 相关事项审批流程标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public decimal? WORKFLOWSTATUSID { get; set; }

        /// <summary>
        /// 审批事项
        /// </summary>
        public string WINAME { get; set; }

        /// <summary>
        /// 当前环节名称
        /// </summary>
        public string ADNAME { get; set; }

        /// <summary>
        /// 当前处理人
        /// </summary>
        public string USERNAME { get; set; }
    }
}
