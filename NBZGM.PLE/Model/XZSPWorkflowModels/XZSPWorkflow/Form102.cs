using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow
{
    /// <summary>
    /// 执法中队派遣核查
    /// </summary>
    public class Form102 : BaseForm
    {
        /// <summary>
        /// 派遣队员
        /// </summary>
        public decimal PQDYID { get; set; }

        /// <summary>
        /// 派遣队员2
        /// </summary>
        public decimal PQDYID2 { get; set; }

        /// <summary>
        /// 派遣意见
        /// </summary>
        public string description { get; set; }
    }
}
