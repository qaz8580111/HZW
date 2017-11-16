using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums
{
    public enum UserPositionEnum : int
    {

        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 1,

        /// <summary>
        /// 局长
        /// </summary>
        JZ = 2,

        /// <summary>
        /// 副局长
        /// </summary>
        FJZ = 3,

        /// <summary>
        /// 大队长
        /// </summary>
        DDZ = 4,

        /// <summary>
        /// 副大队长
        /// </summary>
        FDDZ = 5,

        /// <summary>
        /// 处长
        /// </summary>
        CZ = 6,

        /// <summary>
        /// 副处长
        /// </summary>
        FCZ = 7,

        /// <summary>
        /// 中队长
        /// </summary>
        ZDZ = 8,

        /// <summary>
        /// 副中队长
        /// </summary>
        FZDZ = 9,

        /// <summary>
        /// 队员
        /// </summary>
        DY = 10,

        /// <summary>
        /// 综合科科长
        /// </summary>
        ZHKKZ=11,

        /// <summary>
        /// 综合科队员
        /// </summary>
        ZHKDY=12
    }
}
