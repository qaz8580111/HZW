using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 主办队员提出结案申请
    /// </summary>
    public class Form122 : BaseForm
    {
        /// <summary>
        /// 行政处罚决定书文号
        /// </summary>
        public string XZCFJDSWH { get; set; }

        /// <summary>
        /// 行政处罚内容
        /// </summary>
        public string XZCFNR { get; set; }

        /// <summary>
        /// 处罚执行方式及罚没财物的处置
        /// </summary>
        public string CFZXFSJFMCWDCZ { get; set; }

        /// <summary>
        /// 主办队员意见
        /// </summary>
        public string ZBDYYJ { get; set; }
    }
}
