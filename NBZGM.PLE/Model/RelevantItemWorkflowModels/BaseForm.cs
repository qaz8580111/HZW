using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.CustomModels;

namespace Taizhou.PLE.Model.RelevantItemWorkflowModels
{
    public abstract class BaseForm
    {
        /// <summary>
        /// 表单标识
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
        /// 处理人
        /// </summary>
        public UserInfo ProcessUser { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }
    }
}
