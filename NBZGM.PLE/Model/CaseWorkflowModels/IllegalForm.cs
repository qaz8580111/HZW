using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 违法行为事项表单
    /// </summary>
    public class IllegalForm
    {
        /// <summary>
        /// 违法行为代码标识
        /// </summary>
        public decimal? ID { get; set; }

        /// <summary>
        /// 违法行为代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 违法行为代码名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 违则
        /// </summary>
        public string WeiZe { get; set; }

        /// <summary>
        /// 罚则
        /// </summary>
        public string FaZe { get; set; }

        /// <summary>
        /// 处罚
        /// </summary>
        public string ChuFa { get; set; }

    }
}
