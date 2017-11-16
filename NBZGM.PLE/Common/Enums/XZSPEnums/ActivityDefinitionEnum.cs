using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.XZSPEnums
{
    /// <summary>
    /// 活动定义枚举
    /// </summary>
    public enum ActivityDetinitionEnum:int
    {
        /// <summary>
        /// 受理
        /// </summary>
        SL=1,

        /// <summary>
        /// 预审
        /// </summary>
        YS=2,

        /// <summary>
        /// 勘探反馈
        /// </summary>
        KTFK=3,

        /// <summary>
        /// 领导审核
        /// </summary>
        LDSH=4,

        /// <summary>
        /// 局领导审核
        /// </summary>
        JLDSH = 5,

        /// <summary>
        /// 结案归档
        /// </summary>
        JAGD=6
    }
}
