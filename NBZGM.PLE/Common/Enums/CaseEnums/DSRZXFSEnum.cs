using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.CaseEnums
{
    /// <summary>
    /// 当事人执行方式
    /// </summary>
    public enum DSRZXFSEnum : int
    {
        /// <summary>
        /// 当事人自己履行
        /// </summary>
        DSRZJLX = 1,

        /// <summary>
        /// 当事人提起行政复议或者行政诉讼
        /// </summary>
        DSRTQXZFYHXZSS = 2,

        /// <summary>
        /// 行政强制执行
        /// </summary>
        XZQZZX = 3,

        /// <summary>
        /// 申请法院强制执行
        /// </summary>
        SQFYQZZX = 4
    }
}
