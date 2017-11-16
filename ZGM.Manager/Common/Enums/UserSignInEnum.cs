using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Common.Enums
{
    public enum UserSignInEnum:int
    {
         /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 迟到
        /// </summary>
        Late = 2,

        /// <summary>
        /// 早退
        /// </summary>
        Leave = 3,

        /// <summary>
        /// 缺勤
        /// </summary>
        Absence = 4
    }
}
