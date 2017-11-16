using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common.Enums.XZSPEnums
{
    /// <summary>
    /// 行政审批任务类型
    /// </summary>
    public enum WorkflowDefinitionEnum:int
    {
        /// <summary>
        /// 运输服务
        /// </summary>
        YSFW=1,

        /// <summary>
        /// 消纳服务
        /// </summary>
        XNFW=2,

        /// <summary>
        /// 准运证
        /// </summary>
        ZYZ=3,

        /// <summary>
        /// 广告审批
        /// </summary>
        GGSP=4
    }
}
