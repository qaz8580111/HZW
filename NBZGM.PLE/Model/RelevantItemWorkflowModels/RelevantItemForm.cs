using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.RelevantItemWorkflowModels
{
    public class RelevantItemForm
    {
        /// <summary>
        /// 一般案件流程标识
        /// </summary>
        public string ParentWIID { get; set; }

        /// <summary>
        /// 相关事项审批流程标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        public string WICode { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string WIName { get; set; }

        /// <summary>
        /// 审批事项实体(若为空表示审批事项是其他相关事项审批)
        /// </summary>
        public SPSXForm SPSXForm { get; set; }

        public RelevantItemForm1 RelevantItemForm1 { get; set; }

        public RelevantItemForm2 RelevantItemForm2 { get; set; }

        public RelevantItemForm3 RelevantItemForm3 { get; set; }


    }
}
