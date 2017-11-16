using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.XZSPEnums
{
    public enum StatusEnum:int
    {

        /// <summary>
        /// 活动的
        /// </summary>
        Active = 1,

        /// <summary>
        /// 完成的
        /// </summary>
        Complete = 2,

        /// <summary>
        /// 锁住的
        /// </summary>
        Locked = 3,

        /// <summary>
        /// 删除的
        /// </summary>
        Deleted = 4,

        /// <summary>
        /// 结束的
        /// </summary>
        OVER=5
    }
}
