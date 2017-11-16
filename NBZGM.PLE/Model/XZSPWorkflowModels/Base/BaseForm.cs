using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.Base
{
    public class BaseForm
    {
        /// <summary>
        /// 活动表单标识
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 活动定义标识
        /// </summary>
        public decimal ADID { get; set; }

        /// <summary>
        /// 活动定义名称
        /// </summary>
        public string ADName { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        public string ProcessUserID { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string ProcessUserName { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }
    }
}
