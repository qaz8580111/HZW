using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.CaseEnums
{
    /// <summary>
    /// 文书所属阶段枚举
    /// </summary>
    public enum DocPhaseEnum : int
    {
        /// <summary>
        /// 立案阶段
        /// </summary>
        LAJD = 1,

        /// <summary>
        /// 作出处罚决定阶段
        /// </summary>
        ZCCFJDJD = 2,

        /// <summary>
        /// 告知阶段
        /// </summary>
        GZJD = 3,

        /// <summary>
        /// 陈述申辩阶段
        /// </summary>
        CSSBJD = 4,

        /// <summary>
        /// 听证阶段
        /// </summary>
        TJJD = 5,

        /// <summary>
        /// 制作送达阶段
        /// </summary>
        ZZSDJD = 6,

        /// <summary>
        /// 结案
        /// </summary>
        JAJD = 7
    }
}
