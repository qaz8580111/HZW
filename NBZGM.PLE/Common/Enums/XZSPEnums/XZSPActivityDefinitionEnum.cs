using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.XZSPEnums
{
    public enum XZSPActivityDefinitionEnum : int
    {
        /// <summary>
        /// 承办人提交申请
        /// </summary>
        CBDTJSQ = 1,

        /// <summary>
        /// 执法中队派遣审核
        /// </summary>
        ZFZDPQHC = 2,

        /// <summary>
        /// 执法队员现场核查
        /// </summary>
        ZFDYXCHC = 3,

        /// <summary>
        /// 执法中队审核核查
        /// </summary>
        ZFZDSHHC = 4,

        /// <summary>
        /// 承办人审核申请
        /// </summary>
        CBRSHSQ=5,

        /// <summary>
        /// 承办机构审核申请
        /// </summary>
        CBJGSHSQ = 6,

        /// <summary>
        /// 执法大队审核申请
        /// </summary>
        ZFDDSHSQ = 7
    }
}
