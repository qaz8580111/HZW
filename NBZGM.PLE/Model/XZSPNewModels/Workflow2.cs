using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPNewModels
{
    public class Workflow2 : BaseXZSPWorkflows
    {

        /// 执法中队
        /// </summary>
        public string ZFZD { get; set; }
        /// <summary>
        /// 中队审核意见
        /// </summary>
        public string ZDSHYJ { get; set; }
        /// <summary>
        /// 执法中队人员
        /// </summary>
        public string ZFZDRY { get; set; }
    }
}
