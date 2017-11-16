using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 送达通知(不予行政处罚的情况)
    /// </summary>
    public class Form113:BaseForm
    {
        /// <summary>
        /// 送达方式
        /// </summary>
        public string SDFS { get; set; }

        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime SDSJ { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string BZSM { get; set; }
    }
}