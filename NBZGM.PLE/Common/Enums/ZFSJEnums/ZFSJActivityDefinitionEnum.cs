using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.ZFSJEnums
{
    public enum ZFSJActivityDefinitionEnum : int
    {
        /// <summary>
        /// 事件上报
        /// </summary>
        SJSB = 1,

        /// <summary>
        /// 事件派遣
        /// </summary>
        SJPQ = 2,

        /// <summary>
        /// 事件处理
        /// </summary>
        SJCL = 3
    }
}
