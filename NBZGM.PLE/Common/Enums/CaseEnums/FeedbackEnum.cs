using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.CaseEnums
{
    /// <summary>
    /// 当事人反馈处罚意见枚举
    /// </summary>
    public enum FeedbackEnum : int
    {
        /// <summary>
        /// 放弃陈述申辩或听证
        /// </summary>
        FQ = 0,

        /// <summary>
        /// 要求陈述申辩
        /// </summary>
        SB = 1,

        /// <summary>
        /// 符合听证条件，当事人提出听证申请
        /// </summary>
        TZ = 2
    }
}
