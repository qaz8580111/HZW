using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 当事人意见反馈
    /// </summary>
    public class Form115 : BaseForm
    {
        /// <summary>
        /// 当事人反馈意见标识
        /// 0：放弃陈述申辩或听证
        /// 1：要求陈述申辩
        /// 2：符合听证条件，当事人提出听证申请
        /// </summary>
        public decimal DSRYJID { get; set; }

        /// <summary>
        /// 当事人反馈意见名称
        /// </summary>
        public string DSRYJName { get; set; }

        /// <summary>
        /// 当事人意见
        /// </summary>
        public string DSRYJ { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FKNR { get; set; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public string FKSJ { get; set; }
    }
}
