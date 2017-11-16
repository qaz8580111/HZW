using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPNewModels
{
    public class Workflow1 : BaseXZSPWorkflows
    {
        /// <summary>
        /// 派遣人
        /// </summary>
        public string PQR { get; set; }
        /// <summary>
        /// 大队审核意见
        /// </summary>
        public string DDSHYJ { get; set; }

        /// <summary>
        /// 执法大队
        /// </summary>
        public string ZSDD { get; set; }
    }
}
