using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.CaseEnums
{
    /// <summary>
    /// 流程活动状态
    /// </summary>
    public enum ActivityStatusEnum : int
    {
        /// <summary>
        /// 空的
        /// </summary>
        None = 0,

        /// <summary>
        /// 活动的
        /// </summary>
        Active = 1,

        /// <summary>
        /// 不活动的
        /// </summary>
        Inactive = 2
    }
}
