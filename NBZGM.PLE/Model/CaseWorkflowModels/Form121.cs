using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 送达行政处罚决定书
    /// </summary>
    public class Form121 : BaseForm
    {
        /// <summary>
        /// 当事人执行方式
        /// </summary>
        public decimal DSRZXFS { get; set; }

        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime SDSJ { get; set; }

        /// <summary>
        /// 送达备注
        /// </summary>
        public string SDBZ { get; set; }
    }
}
